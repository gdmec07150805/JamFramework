/*************************************************************************************
*Author:       Jam Chen
*Version:      1.0
*UnityVersion: 2018.3.0f2
*Date:         2019-02-20
*Description:   
*History:
*************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JamFramework
{
    /// <summary>
    /// 流程管理实现类
    /// </summary>
    internal sealed class ProcessManager : JamFrameworkModule, IProcessManager
    {
        private IFsmManager m_FsmManager;
        private IFsm<IProcessManager> m_ProcessFsm;

        /// <summary>
        /// 流程优先级
        /// </summary>
        internal override int Priority
        {
            get { return -10; }
        }

        public void Initialize(IFsmManager fsmManager, params ProcessBase[] processs)
        {
            if (fsmManager==null)
            {
                throw new Exception("FSM manager无效");
            }

            m_FsmManager = fsmManager;
            m_ProcessFsm = m_FsmManager.CreateFsm(this, processs);
        }

        public void StartProcess(Type processType)
        {
            if (processType==null)
            {
                throw new Exception("开始流程类不存在：" + processType);
            }
            if (m_ProcessFsm == null)
            {
                throw new Exception("流程状态机不存在");
            }
            m_ProcessFsm.Start(processType);
        }
    }
}

