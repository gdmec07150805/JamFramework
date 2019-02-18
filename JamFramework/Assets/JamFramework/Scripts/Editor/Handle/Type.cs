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
    /// �������ݻ�ȡ
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
                //��������ֻ�����������ƣ��������Ļ�������Ͳ���ȥGAC(ȫ�ֳ��򼯻���)���ȡ����
                //ֻ���ȴӸ�Ŀ¼�ң��ٴ��ļ�����Ŀ¼�ң�����Ҫ��GAC�ҵĻ�����Ҫ�ṩ
                //�汾��������Ϣ��������Կ��ǵ��ַ�������������:"MyAssembly,Version=1.0.0.0,culture=zh-CN,PublicKeyToken=47887f89771bc57f��
                assembly = Assembly.Load(assemblyName);
            }
            catch { }

            System.Type[] types = assembly.GetTypes();
            foreach (System.Type t in types)
            {
                //�ж��Ƿ��Ƿǳ��󡢿��ⲿʹ�õ���
                if (t.IsClass && !t.IsAbstract && baseType.IsAssignableFrom(t))
                    typeNames.Add(t.FullName);
            }
            typeNames.Sort();
            return typeNames.ToArray();
        }
    }
}

