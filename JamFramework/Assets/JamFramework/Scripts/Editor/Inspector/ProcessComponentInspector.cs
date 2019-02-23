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
    [CanEditMultipleObjects]//������ѡ��ͬһ�����͵Ķ�����ʱ�������Զ��������ǿ���֧��ͬʱ�޸�����ѡ�е������
    [CustomEditor(typeof(ProcessComponent), false)]//�ڶ�������Ĭ��Ϊfalse�������̳е�������ʾ
    public class ProcessComponentInspector : JamFrameworkComponentInspector
    {

        private SerializedProperty sp_ProcessTypeNames;
        private SerializedProperty sp_EntranceProcessTypeName;

        public List<string> processTypeNames = new List<string>();

        private int m_EntranceProcessIndex = -1;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            //���л�����ʵʱ����
            serializedObject.Update();

            //�������ݣ�Ӧ����ʾ��ʽ
            Handle();

            //���л�����Ӧ�ø���
            serializedObject.ApplyModifiedProperties();

            //���»���
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
        /// �������ݿ��ӻ�
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
        /// ��ȡ��������
        /// </summary>
        private void RefreshProcessTypeName()
        {
            #region ˢ�³������������������
            processTypeNames.Clear();
            processTypeNames.AddRange(Type.GetTypeNames(typeof(ProcessBase)));
            #endregion

            #region ˢ����������������������
            List<string> allProcess_tmp = new List<string>();
            for (int i = 0; i < sp_ProcessTypeNames.arraySize; i++)
            {
                allProcess_tmp.Add(sp_ProcessTypeNames.GetArrayElementAtIndex(i).stringValue);
            }
            if (allProcess_tmp.Count != processTypeNames.Count)//���������������������֮ǰ��õĲ�һ�£�ˢ��
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
        /// ����ѡ�����������±�
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

