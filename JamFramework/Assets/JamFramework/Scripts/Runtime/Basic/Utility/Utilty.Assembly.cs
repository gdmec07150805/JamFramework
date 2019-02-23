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
    public static partial class Utility
    {
        public static class Assembly
        {
            private static readonly System.Reflection.Assembly[] s_Assemblys = null;
            //类型的缓存
            private static readonly Dictionary<string, Type> s_CachedTypes = new Dictionary<string, Type>();

            static Assembly()
            {
                //获取程序所有的程序集
                s_Assemblys = AppDomain.CurrentDomain.GetAssemblies();
            }

            /// <summary>
            /// 获取已加载的程序集中的指定类型
            /// </summary>
            /// <param name="typeName"></param>
            /// <returns></returns>
            public static Type GetType(string typeName)
            {
                //Debug.Log(typeName);
                if (string.IsNullOrEmpty(typeName))
                {
                    throw new Exception("Type name is invalid");
                }
                //缓存提取
                Type type = null;
                if (s_CachedTypes.TryGetValue(typeName,out type))
                {
                    return type;
                }
                //从同一程序集里获取
                type = Type.GetType(typeName);
                //Debug.Log(type.FullName);
                if (type != null)
                {
                    s_CachedTypes.Add(typeName,type);
                    return type;
                }
                //从所有程序集里获取
                foreach (System.Reflection.Assembly assembly in s_Assemblys)
                {
                    type = Type.GetType(string.Format("{0}, {1}", typeName, assembly.FullName));
                    //Debug.Log(type.FullName);
                    if (type != null)
                    {
                        s_CachedTypes.Add(typeName,type);
                        return type;
                    }
                }
                return null;
            }

        }
    }

}

