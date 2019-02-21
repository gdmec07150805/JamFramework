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
    /// 有限状态机状态基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class FsmState<T> where T : class
    {
        protected internal virtual void OnInit(IFsm<T> fsm) { }
        protected internal virtual void OnEnter(IFsm<T> fsm) { }
        protected internal virtual void OnUpdate(IFsm<T> fsm,float elapseSeconds,float realElapseSeconds) { }
        protected internal virtual void OnLeave(IFsm<T> fsm,bool isShutdown) { }
        protected internal virtual void OnDestory(IFsm<T> fsm) { }
    }
}
