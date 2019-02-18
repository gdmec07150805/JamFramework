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

        //private SerializedProperty sp_ProcessTypeNames;
        private SerializedProperty sp_EntranceProcessTypeName;

        public List<string> processTypeNames = new List<string>();

        private int entranceProcessIndex = -1;

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
        /// �������ݿ��ӻ�
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
        /// ��ȡ��������
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

