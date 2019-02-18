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
    /// ��������չ��������
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
        /// ���뿪ʼ
        /// </summary>
        protected virtual void OnCompileStart()
        {

        }

        /// <summary>
        /// �������
        /// </summary>
        protected virtual void OnCompileComplete()
        {

        }

    }
}


