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
    public class FsmManager {

        /// <summary>
        /// ��������״̬��
        /// </summary>
        /// <typeparam name="T">״̬������������</typeparam>
        /// <param name="owner">״̬��������</param>
        /// <param name="states">״̬��״̬����</param>
        /// <returns></returns>
        public IFsm<T> CreateFsm<T>(T owner,params FsmState<T>[] states) where T : class
        {
            return null;
        } 
		
	}
}
