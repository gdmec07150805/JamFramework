/*************************************************************************************
*Author:       Jam Chen
*Version:      1.0
*UnityVersion: 2018.3.0f2
*Date:         2019-02-17
*Description:   
*History:
*************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JamFramework.Runtime
{
    public class ProcessLogin : ProcessBase
    {
        protected internal override void OnEnter(IFsm<IProcessManager> fsmOwner)
        {
            Debug.Log("I'm coming");
        }
    }
}

