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
        /// ��ʼ����������
        /// </summary>
        void Initialize(IFsmManager fsmManager,params ProcessBase[] proce);
        /// <summary>
        /// ��ʼ����
        /// </summary>
        /// <param name="processType"></param>
        void StartProcess(Type processType);
    } 
}

