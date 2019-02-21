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
    /// ������
    /// </summary>
    public class JamFrameworkEntry
    {
        private static readonly LinkedList<JamFrameworkModule> s_JamFrameworkModules = new LinkedList<JamFrameworkModule>();

        /// <summary>
        /// ��ȡ���ߴ�����Ϸ���ģ��
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetModule<T>() where T : class
        {
            Type interfaceType = typeof(T);
            //�����ǽӿڡ�����JamFrameworkģ��
            if (!interfaceType.IsInterface || !interfaceType.FullName.StartsWith("JamFramework"))
            {
                throw new Exception(string.Format("��ȡ���ģ��ʧ�ܣ����ǽӿڻ��߲����ڿ�ܳ���{0}",interfaceType.FullName));
            }
            //��ȡģ������
            string moduleName = string.Format("{0}.{1}", interfaceType.Namespace, interfaceType.Name.Substring(1));
            Type moduleType = Type.GetType(moduleName);
            if (moduleType == null)
            {
                throw new Exception(string.Format("û�з���ģ������{0}", moduleName));
            }

            return GetModule(moduleType) as T;
        }

        /// <summary>
        /// ��ȡ���ģ��
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        private static JamFrameworkModule GetModule(Type moduleType)
        {
            foreach (JamFrameworkModule module in s_JamFrameworkModules)
            {
                if (module.GetType() == moduleType)
                    return module;
            }

            return CreateModule(moduleType);
        }

        /// <summary>
        /// �������ģ��
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        private static JamFrameworkModule CreateModule(Type moduleType)
        {
            JamFrameworkModule module = Activator.CreateInstance(moduleType) as JamFrameworkModule;
            if (module == null)
            {
                throw new Exception(string.Format("���ģ�鴴��ʧ�ܣ�{0}",moduleType.FullName));
            }
            LinkedListNode<JamFrameworkModule> current = s_JamFrameworkModules.First;
            while (current!=null)
            {
                //������󴴽���ģ�����ȼ����б�Ԫ�ظߣ������ǰ��
                if (module.Priority>current.Value.Priority)
                    break;
                current = current.Next;
            }

            if (current != null)
            {
                s_JamFrameworkModules.AddBefore(current, module);
            }
            else
            {
                s_JamFrameworkModules.AddLast(module);
            }
            return module;
        }
    }

}

