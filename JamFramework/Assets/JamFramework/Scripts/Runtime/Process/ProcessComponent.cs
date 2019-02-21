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
    [DisallowMultipleComponent]//不允许同个物体挂载多个此组件
    [AddComponentMenu("JamFramework/Process")]
    public sealed class ProcessComponent : JamFrameworkComponent
    {
        private IProcessManager m_IProcessManager=null;//流程管理器

        [HideInInspector][SerializeField]
        private string[] m_ProcessTypeNames = null;//所有流程名字
        [HideInInspector][SerializeField]
        private string m_EntranceProcessTypeName = null;//入口流程名字

        private ProcessBase m_EntranceProcess = null;//入口流程实例

        protected override void Awake()
        {
            base.Awake();

            //获取流程管理器
            m_IProcessManager = JamFrameworkEntry.GetModule<IProcessManager>();
            if (m_IProcessManager==null)
            {
                Debug.LogError("流程管理器获取失败");
                return;
            }
        }

        private IEnumerator Start()
        {
            //流程初始化失败
            if (!InitializeProcedures())
                yield break;
            yield return new WaitForEndOfFrame();
            //流程开始
            m_IProcessManager.StartProcess(m_EntranceProcess.GetType());
        }
      
        /// <summary>
        /// 所有流程初始化
        /// </summary>
        /// <returns>初始化结果</returns>
        private bool InitializeProcedures()
        {
            ProcessBase[] processs = new ProcessBase[m_ProcessTypeNames.Length];
            for (int i = 0; i < m_ProcessTypeNames.Length; i++)
            {
                //获取流程的类型
                Type processType = Utility.Assembly.GetType(m_ProcessTypeNames[i]);
                if (processType == null)
                {
                    Debug.LogErrorFormat("初始化：不能获取到流程类型 {0}.",m_ProcessTypeNames[i]);
                    return false;
                }
                //创建流程实例
                processs[i] = Activator.CreateInstance(processType) as ProcessBase;
                if (processs[i] == null)
                {
                    Debug.LogErrorFormat("初始化：不能创建流程实例 {0}.", m_ProcessTypeNames[i]);
                }
                //获取入口流程实例
                if (m_EntranceProcessTypeName == m_ProcessTypeNames[i])
                    m_EntranceProcess = processs[i];
            }

            if (m_EntranceProcess == null)
            {
                Debug.LogError("流程入口无效");
                return false;
            }
            //实例化流程状态机
            m_IProcessManager.Initialize(JamFrameworkEntry.GetModule<IFsmManager>(),processs);
            return true;
        }
    }

}

