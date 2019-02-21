/*************************************************************************************
*Author:       Jam Chen
*Version:      1.0
*UnityVersion: 2018.3.0f2
*Date:         2019-02-17
*Description:   
*History:
*************************************************************************************/

using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using JamFramework;
using UnityEditor.Android.Il2Cpp;

namespace JamFramework.Runtime
{
    [DisallowMultipleComponent]//������ͬ��������ض�������
    [AddComponentMenu("JamFramework/Process")]
    public sealed class ProcessComponent : JamFrameworkComponent
    {
        private IProcessManager m_IProcessManager=null;//���̹�����

        [HideInInspector][SerializeField]
        private string[] m_ProcessTypeNames = null;//������������
        [HideInInspector][SerializeField]
        private string m_EntranceProcessTypeName = null;//�����������

        private ProcessBase m_EntranceProcess = null;//�������ʵ��

        protected override void Awake()
        {
            base.Awake();

            //��ȡ���̹�����
            m_IProcessManager = JamFrameworkEntry.GetModule<IProcessManager>();
            if (m_IProcessManager==null)
            {
                Debug.LogError("���̹�������ȡʧ��");
                return;
            }
        }

        private IEnumerator Start()
        {
            //���̳�ʼ��ʧ��
            if (!InitializeProcedures())
                yield break;
            yield return new WaitForEndOfFrame();
            //���̿�ʼ
            m_IProcessManager.StartProcess(m_EntranceProcess.GetType());
        }
      
        /// <summary>
        /// �������̳�ʼ��
        /// </summary>
        /// <returns>��ʼ�����</returns>
        private bool InitializeProcedures()
        {
            ProcessBase[] processs = new ProcessBase[m_ProcessTypeNames.Length];
            for (int i = 0; i < m_ProcessTypeNames.Length; i++)
            {
                //��ȡ���̵�����
                Type processType = Utility.Assembly.GetType(m_ProcessTypeNames[i]);
                if (processType == null)
                {
                    Debug.LogErrorFormat("��ʼ�������ܻ�ȡ���������� {0}.",m_ProcessTypeNames[i]);
                    return false;
                }
                //��������ʵ��
                processs[i] = Activator.CreateInstance(processType) as ProcessBase;
                if (processs[i] == null)
                {
                    Debug.LogErrorFormat("��ʼ�������ܴ�������ʵ�� {0}.", m_ProcessTypeNames[i]);
                }
                //��ȡ�������ʵ��
                if (m_EntranceProcessTypeName == m_ProcessTypeNames[i])
                    m_EntranceProcess = processs[i];
            }

            if (m_EntranceProcess == null)
            {
                Debug.LogError("���������Ч");
                return false;
            }
            //ʵ��������״̬��
            m_IProcessManager.Initialize(JamFrameworkEntry.GetModule<IFsmManager>(),processs);
            return true;
        }
    }

}

