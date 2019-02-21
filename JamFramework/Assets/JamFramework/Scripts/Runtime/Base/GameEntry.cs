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
    /// ��Ϸ���
    /// </summary>
    public static class GameEntry
    {
        private static readonly LinkedList<JamFrameworkComponent> s_JamFrameworkComponents = new LinkedList<JamFrameworkComponent>();

        /// <summary>
        /// ע����Ϸ������
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
            //�����û����ͬ�����ע��
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

