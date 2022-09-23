using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using LoLSDK;
using System;

namespace RobertHeslin.RobotRepair
{
    public static class SharedData
    {

        private static JSONNode startGameData;
        private static JSONNode languageDefs;

        public static JSONNode StartGameData
        {
            get { return startGameData; }
            set { startGameData = value; }
        }

        public static JSONNode LanguageDefs
        {
            get { return languageDefs; }
            set { languageDefs = value; }
        }

        public static void SaveData(RobotRepairData data)
        {
            LOLSDK.Instance.SaveState(data);
        }

    }
}

