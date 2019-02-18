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

        //private SerializedProperty sp_ProcessTypeNames;
        private SerializedProperty sp_EntranceProcessTypeName;

        public List<string> processTypeNames = new List<string>();

        private int entranceProcessIndex = -1;

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
            //Repaint();
        }

        protected override void OnCompileComplete()
        {
            base.OnCompileComplete();

            RefreshProcessTypeName();
        }

        private void OnEnable()
        {
            sp_EntranceProcessTypeName = serializedObject.FindProperty("m_EntranceProcedureTypeName");

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
                    int selectIndex = EditorGUILayout.Popup("Entrance Process", entranceProcessIndex, processTypeNames.ToArray());
                    
                    if (selectIndex != entranceProcessIndex)
                    {
                        entranceProcessIndex = selectIndex;
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
            processTypeNames.Clear();
            processTypeNames.AddRange( Type.GetTypeNames(typeof(ProcessBase)));

            if (!string.IsNullOrEmpty(sp_EntranceProcessTypeName.stringValue))
            {
                entranceProcessIndex = processTypeNames.IndexOf(sp_EntranceProcessTypeName.stringValue);
            }

        }

    }
}

