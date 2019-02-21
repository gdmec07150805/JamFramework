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
    /// 框架入口
    /// </summary>
    public class JamFrameworkEntry
    {
        private static readonly LinkedList<JamFrameworkModule> s_JamFrameworkModules = new LinkedList<JamFrameworkModule>();

        /// <summary>
        /// 获取或者创建游戏框架模块
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetModule<T>() where T : class
        {
            Type interfaceType = typeof(T);
            //必须是接口、属于JamFramework模块
            if (!interfaceType.IsInterface || !interfaceType.FullName.StartsWith("JamFramework"))
            {
                throw new Exception(string.Format("获取框架模块失败，不是接口或者不属于框架程序集{0}",interfaceType.FullName));
            }
            //获取模块类型
            string moduleName = string.Format("{0}.{1}", interfaceType.Namespace, interfaceType.Name.Substring(1));
            Type moduleType = Type.GetType(moduleName);
            if (moduleType == null)
            {
                throw new Exception(string.Format("没有发现模块类型{0}", moduleName));
            }

            return GetModule(moduleType) as T;
        }

        /// <summary>
        /// 获取框架模块
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
        /// 创建框架模块
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        private static JamFrameworkModule CreateModule(Type moduleType)
        {
            JamFrameworkModule module = Activator.CreateInstance(moduleType) as JamFrameworkModule;
            if (module == null)
            {
                throw new Exception(string.Format("框架模块创建失败：{0}",moduleType.FullName));
            }
            LinkedListNode<JamFrameworkModule> current = s_JamFrameworkModules.First;
            while (current!=null)
            {
                //如果请求创建的模块优先级比列表元素高，会放在前面
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

