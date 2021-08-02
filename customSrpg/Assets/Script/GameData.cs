using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Custom
{
    /// <summary>
    /// JSONで扱う為のクラス
    /// </summary>
    [System.Serializable]
    public class GameData
    {
        public string unitHeadID;
        public string unitBodyID;
        public string unitRArmID;
        public string unitLArmID;
        public string unitLegID;
        public string WeaponBodyID;
        public string WeaponRArmID;
        public string WeaponLArmID;
    }
}
