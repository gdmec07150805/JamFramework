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

namespace JamFramework.Runtime
{
    /// <summary>
    /// 游戏入口
    /// </summary>
    public static class GameEntry
    {
        private static readonly LinkedList<JamFrameworkComponent> s_JamFrameworkComponents = new LinkedList<JamFrameworkComponent>();

        /// <summary>
        /// 注册游戏框架组件
        /// </summary>
        /// <param name="jamFrameworkComponent"></param>
        internal static void RegisterComponent(JamFrameworkComponent jamFrameworkComponent)
        {
            if (jamFrameworkComponent == null)
            {
                Debug.LogError("JamFramework component is invalid");
                return;
            }

            Type t = typeof(JamFrameworkComponent);

            LinkedListNode<JamFrameworkComponent> current = s_JamFrameworkComponents.First;
            //检查有没有相同组件已注册
            while (current!=null)
            {
                if (t == current.Value.GetType())
                {
                    Debug.LogError("JamFramework component type "+ t.FullName + " is already exist.");
                }
                current = current.Next;
            }

            s_JamFrameworkComponents.AddLast(jamFrameworkComponent);

        }

        

    }

}

