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
    internal sealed class Fsm<T> : FsmBase, IFsm<T> where T : class
    {
        private readonly T m_Owner;
        private readonly Dictionary<string, FsmState<T>> m_States = new Dictionary<string, FsmState<T>>();

        public Fsm(string name,T owner,params FsmState<T>[] states)
        {
            if (owner == null)
            {
                throw new Exception("FSM owner is invalid.");
            }

            if (states == null || states.Length < 1)
            {
                throw new Exception("FSM states is invalid.");
            }

            m_Owner = owner;

            foreach (FsmState<T> state in states)
            {
                if (state == null)
                {
                    throw new Exception("FSM states is invalid.");
                }

                string stateName = state.GetType().FullName;
                if (m_States.ContainsKey(stateName))
                {
                    throw new Exception("FSM state is already exist.");
                }

                m_States.Add(stateName, state);
                state.OnInit(this);
            }
        }

        public void Start(Type stateType)
        {
            if (stateType == null)
            {
                throw new Exception("��ʼ�����಻���ڣ�"+stateType.FullName);
            }
            if (!typeof(FsmState<T>).IsAssignableFrom(stateType))
            {
                throw new Exception("��״̬��������Fsm��" + stateType.FullName);
            }

            FsmState<T> state = GetState(stateType);
            if (state == null)
            {
                throw new Exception("��ȡ��������״̬��״̬��"+stateType.FullName);
            }
            state.OnEnter(this);
        }

        /// <summary>
        /// ��ȡ����״̬��״̬
        /// </summary>
        /// <param name="stateType"></param>
        /// <returns></returns>
        public FsmState<T> GetState(Type stateType)
        {
            if (stateType == null)
            {
                throw new NotImplementedException("��״̬�����ڣ�" + stateType.FullName);
            }

            if (!typeof(FsmState<T>).IsAssignableFrom(stateType))
            {
                throw new NotImplementedException("��״̬��������Fsm��" + stateType.FullName);
            }

            FsmState<T> state = null;
            if (m_States.TryGetValue(stateType.FullName, out state))
                return state;
            return null;
        }
    }
}