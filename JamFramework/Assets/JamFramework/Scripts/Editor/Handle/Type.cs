/*************************************************************************************
*Author:       Jam Chen
*Version:      1.0
*UnityVersion: 2018.3.0f2
*Date:         2019-02-17
*Description:   
*History:
*************************************************************************************/

using System.Collections.Generic;
using System.Reflection;


namespace JamFramework
{
    /// <summary>
    /// 程序集数据获取
    /// </summary>
    internal static class Type 
    {
        private static readonly string AssemblyName_Runtime = "JamFramework.Runtime";
        private static readonly string AssemblyName_Editor = "JamFramework.Editor";

        internal static string[] GetTypeNames(System.Type baseType)
        {
            return GetTypeNames(baseType, AssemblyName_Runtime);
        }

        private static string[] GetTypeNames(System.Type baseType,string assemblyName)
        {

            List<string> typeNames = new List<string>();
            Assembly assembly = null;
            try
            {
                //弱命名（只给出程序集名称）做参数的话，程序就不会去GAC(全局程序集缓存)里读取程序集
                //只会先从根目录找，再从文件所在目录找，所以要从GAC找的话，需要提供
                //版本，区域信息，公有密钥标记的字符串参数，例如:"MyAssembly,Version=1.0.0.0,culture=zh-CN,PublicKeyToken=47887f89771bc57f”
                assembly = Assembly.Load(assemblyName);
            }
            catch { }

            System.Type[] types = assembly.GetTypes();
            foreach (System.Type t in types)
            {
                //判断是否是非抽象、可外部使用的类
                if (t.IsClass && !t.IsAbstract && baseType.IsAssignableFrom(t))
                    typeNames.Add(t.FullName);
            }
            typeNames.Sort();
            return typeNames.ToArray();
        }
    }
}

