using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoLSDK;

namespace RobertHeslin.RobotRepair
{
    public class LoL_DDOL : MonoBehaviour
    {
        void Awake()
        {
            LOLSDK.DontDestroyOnLoad(gameObject);
        }

    }
}

