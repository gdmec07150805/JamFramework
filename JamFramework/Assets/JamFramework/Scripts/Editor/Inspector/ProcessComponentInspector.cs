/*************************************************************************************
*Author:       Jam Chen
*Version:      1.0
*UnityVersion: 2018.3.0f2
*Date:         2019-02-17
*Description:   
*History:
*************************************************************************************/

using UnityEditor;
using UnityEngine;
using JamFramework.Runtime;
using System.Collections.Generic;

namespace JamFramework.Editor
{
    [CanEditMultipleObjects]//当我们选择同一种类型的多个组件时，我们自定义的面板是可以支持同时修改所有选中的组件的
    [CustomEditor(typeof(ProcessComponent), false)]//第二个参数默认为false，不给继承的子类显示
    public class ProcessComponentInspector : JamFrameworkComponentInspector
    {

        private SerializedProperty sp_ProcessTypeNames;
        private SerializedProperty sp_EntranceProcessTypeName;

        public List<string> processTypeNames = new List<string>();

        private int m_EntranceProcessIndex = -1;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            //序列化数据实时更新
            serializedObject.Update();

            //处理数据，应用显示方式
            Handle();

            //序列化数据应用更新
            serializedObject.ApplyModifiedProperties();

            //重新绘制
            Repaint();
        }

        protected override void OnCompileComplete()
        {
            base.OnCompileComplete();
            RefreshProcessTypeName();
        }

        private void OnEnable()
        {
            sp_ProcessTypeNames = serializedObject.FindProperty("m_ProcessTypeNames");
            sp_EntranceProcessTypeName = serializedObject.FindProperty("m_EntranceProcessTypeName");
            RefreshProcessTypeName();
        }

        /// <summary>
        /// 处理数据可视化
        /// </summary>
        private void Handle()
        {
            //ProcessComponent t = target as ProcessComponent;

            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            {
                if (processTypeNames != null && processTypeNames.Count > 0)
                {
                    int selectIndex = EditorGUILayout.Popup("Entrance Process", m_EntranceProcessIndex, processTypeNames.ToArray());
                    
                    if (selectIndex != m_EntranceProcessIndex)
                    {
                        m_EntranceProcessIndex = selectIndex;
                        sp_EntranceProcessTypeName.stringValue = processTypeNames[selectIndex];
                    }
                }
            }
            EditorGUI.EndDisabledGroup();
        }

        /// <summary>
        /// 获取所有流程
        /// </summary>
        private void RefreshProcessTypeName()
        {
            #region 刷新程序集里的所有流程名字
            processTypeNames.Clear();
            processTypeNames.AddRange(Type.GetTypeNames(typeof(ProcessBase)));
            #endregion

            #region 刷新组件类里的所有流程名字
            List<string> allProcess_tmp = new List<string>();
            for (int i = 0; i < sp_ProcessTypeNames.arraySize; i++)
            {
                allProcess_tmp.Add(sp_ProcessTypeNames.GetArrayElementAtIndex(i).stringValue);
            }
            if (allProcess_tmp.Count != processTypeNames.Count)//如果程序集里的流程类个数和之前获得的不一致，刷新
            {
                sp_ProcessTypeNames.ClearArray();
                for (int i = 0; i < processTypeNames.Count; i++)
                {
                    sp_ProcessTypeNames.InsertArrayElementAtIndex(i);
                    sp_ProcessTypeNames.GetArrayElementAtIndex(i).stringValue = processTypeNames[i];
                }
            }
            #endregion

            SetEntranceProcessIndex();

            serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// 设置选择的入口流程下标
        /// </summary>
        private void SetEntranceProcessIndex()
        {
            if (!string.IsNullOrEmpty(sp_EntranceProcessTypeName.stringValue))
            {
                m_EntranceProcessIndex = processTypeNames.IndexOf(sp_EntranceProcessTypeName.stringValue);
                if (m_EntranceProcessIndex < 0)
                {
                    sp_EntranceProcessTypeName.stringValue = null;
                }
            }
        }

    }
}

