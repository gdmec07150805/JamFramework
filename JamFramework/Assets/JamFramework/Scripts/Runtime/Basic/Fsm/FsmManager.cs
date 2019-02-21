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
        /// 创建有限状态机
        /// </summary>
        /// <typeparam name="T">状态机持有者类型</typeparam>
        /// <param name="owner">状态机持有者</param>
        /// <param name="states">状态机状态集合</param>
        /// <returns></returns>
        public IFsm<T> CreateFsm<T>(T owner,params FsmState<T>[] states) where T : class
        {
            return null;
        } 
		
	}
}
