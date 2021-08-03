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
        public string UnitHeadID;
        public string UnitBodyID;
        public string UnitRArmID;
        public string UnitLArmID;
        public string UnitLegID;
        public string WeaponBodyID;
        public string WeaponRArmID;
        public string WeaponLArmID;
        public string UnitColor;
    }
}
