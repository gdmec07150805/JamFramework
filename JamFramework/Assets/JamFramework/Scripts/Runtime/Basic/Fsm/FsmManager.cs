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
    internal sealed class FsmManager :JamFrameworkModule,IFsmManager{
        private readonly Dictionary<string, FsmBase> m_Fsms = new Dictionary<string, FsmBase>();
        /// <summary>
        /// ��������״̬��
        /// </summary>
        /// <typeparam name="T">״̬������������</typeparam>
        /// <param name="owner">״̬��������</param>
        /// <param name="states">״̬��״̬����</param>
        /// <returns></returns>
        public IFsm<T> CreateFsm<T>(T owner,params FsmState<T>[] states) where T : class
        {
            return CreateFsm(string.Empty, owner, states);
        }
        /// <summary>
        /// ��������״̬��
        /// </summary>
        /// <typeparam name="T">״̬������������</typeparam>
        /// <param name="owner">״̬��������</param>
        /// <param name="states">״̬��״̬����</param>
        /// <returns></returns>
        public IFsm<T> CreateFsm<T>(string name,T owner, params FsmState<T>[] states) where T : class
        {
            Fsm<T> fsm = new Fsm<T>(name,owner,states);
            m_Fsms.Add(GetFullName<T>(name),fsm);
            return fsm;
        }

        private string GetFullName<T>(String name)
        {
            return string.IsNullOrEmpty(name) ? typeof(T).FullName : string.Format("{0}.{1}",typeof(T).FullName,name);
        }
    }
}
