/*************************************************************************************
*Author:       Jam Chen
*Version:      1.0
*UnityVersion: 2018.3.0f2
*Date:         2019-02-17
*Description:   
*History:
*************************************************************************************/

using UnityEditor;
using UnityEngine;

namespace JamFramework.Editor {
    /// <summary>
    /// 框架组件扩展基本处理
    /// </summary>
    public abstract class JamFrameworkComponentInspector : UnityEditor.Editor
    {
        private bool compiling;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if (!compiling && EditorApplication.isCompiling)
            {
                compiling = true;
                OnCompileStart();
            }else if(compiling && !EditorApplication.isCompiling)
            {
                compiling = false;
                OnCompileComplete();
            }
        }

        /// <summary>
        /// 编译开始
        /// </summary>
        protected virtual void OnCompileStart()
        {

        }

        /// <summary>
        /// 编译完成
        /// </summary>
        protected virtual void OnCompileComplete()
        {

        }

    }
}


