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
    /// <summary>
    /// 游戏框架模块抽象类
    /// </summary>
    internal abstract class JamFrameworkModule
    {
        /// <summary>
        /// 游戏框架模块优先级
        /// </summary>
        internal virtual int Priority
        {
            get { return 0; }
        }
    }

}

