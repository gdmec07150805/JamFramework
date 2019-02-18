/*************************************************************************************
*Author:       Jam Chen
*Version:      1.0
*UnityVersion: 2018.3.0f2
*Date:         2019-02-17
*Description:   
*History:
*************************************************************************************/

using UnityEngine;
namespace JamFramework.Runtime
{
    [DisallowMultipleComponent]//不允许同个物体挂载多个此组件
    [AddComponentMenu("JamFramework/Process")]
    public class ProcessComponent : MonoBehaviour
    {
        [HideInInspector][SerializeField]
        private string m_EntranceProcedureTypeName = null;

        void Awake()
        {
            if (m_EntranceProcedureTypeName == null)
            {
                Debug.Log("no addition entrance process");
                return;
            }


        }

    }
}

