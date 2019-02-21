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
    public interface IFsm<T> where T:class
    {
        /// <summary>
        /// 开始有限状态机
        /// </summary>
        /// <param name="stateType"></param>
        void Start(Type stateType);
    }
}
