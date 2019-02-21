/*************************************************************************************
*Author:       Jam Chen
*Version:      1.0
*UnityVersion: 2018.3.0f2
*Date:         2019-02-20
*Description:   
*History:
*************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JamFramework
{
    public interface IFsmManager
    {
        /// <summary>
        /// ��������״̬����
        /// </summary>
        /// <typeparam name="T">����״̬�����������͡�</typeparam>
        /// <param name="owner">����״̬�������ߡ�</param>
        /// <param name="states">����״̬��״̬���ϡ�</param>
        /// <returns>Ҫ����������״̬����</returns>
        IFsm<T> CreateFsm<T>(T owner, params FsmState<T>[] states) where T : class;
    }
}
