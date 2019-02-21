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
    public interface IProcessManager
    {
        /// <summary>
        /// 初始化所有流程
        /// </summary>
        void Initialize(IFsmManager fsmManager,params ProcessBase[] proce);
        /// <summary>
        /// 开始流程
        /// </summary>
        /// <param name="processType"></param>
        void StartProcess(Type processType);
    } 
}

