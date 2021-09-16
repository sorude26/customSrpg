using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Custom;
using System;
/// <summary>
/// セーブデータを扱う
/// </summary>
public class GameDataManager : MonoBehaviour
{
    /// <summary>
    /// セーブファイル名
    /// </summary>
    string m_dataPath;
    private void Awake()
    {
        m_dataPath = Application.dataPath + "/JsonData.json";
    }
    //void Start()
    //{
    //    GameData testData = new GameData();
    //    string test = JsonUtility.ToJson(testData);
    //    Debug.Log(test);
    //}
    public void SaveData()
    {
        SaveData(Save());
    }
    private void SaveData(GameData data)
    {
        string jsonData = JsonUtility.ToJson(data);
        StreamWriter writer = new StreamWriter(m_dataPath, false);
        writer.WriteLine(jsonData);
        writer.Flush();
        writer.Close();
    }
    public void LoadData()
    {
        Load(LoadData(m_dataPath));
    }
    public GameData LoadData(string dataPath)
    {
        StreamReader reader = new StreamReader(dataPath);
        string data = reader.ReadToEnd();
        reader.Close();
        return JsonUtility.FromJson<GameData>(data);
    }
    private GameData Save()
    {
        GameData newData = new GameData();
        for (int i = 0; i < UnitDataMaster.MaxUintCount; i++)
        {
            newData.UnitHeadID += UnitDataMaster.PlayerUnitBuildDatas[i].HeadID.ToString() + ",";
            newData.UnitBodyID += UnitDataMaster.PlayerUnitBuildDatas[i].BodyID.ToString() + ",";
            newData.UnitRArmID += UnitDataMaster.PlayerUnitBuildDatas[i].RArmID.ToString() + ",";
            newData.UnitLArmID += UnitDataMaster.PlayerUnitBuildDatas[i].LArmID.ToString() + ",";
            newData.UnitLegID += UnitDataMaster.PlayerUnitBuildDatas[i].LegID.ToString() + ",";
            newData.WeaponRArmID += UnitDataMaster.PlayerUnitBuildDatas[i].WeaponRArmID.ToString() + ",";
            newData.WeaponLArmID += UnitDataMaster.PlayerUnitBuildDatas[i].WeaponLArmID.ToString() + ",";
            newData.UnitColor += UnitDataMaster.PlayerColors[i].ToString() + ",";
        }
        for (int x = 0; x < UnitDataMaster.HavePartsDic[PartsType.Head].Length; x++)
        {
            newData.HaveHead += UnitDataMaster.HavePartsDic[PartsType.Head][x].ToString() + ",";
        }
        for (int x = 0; x < UnitDataMaster.HavePartsDic[PartsType.Body].Length; x++)
        {
            newData.HaveBody += UnitDataMaster.HavePartsDic[PartsType.Body][x].ToString() + ",";
        }
        for (int x = 0; x < UnitDataMaster.HavePartsDic[PartsType.RArm].Length; x++)
        {
            newData.HaveRArm += UnitDataMaster.HavePartsDic[PartsType.RArm][x].ToString() + ",";
        }
        for (int x = 0; x < UnitDataMaster.HavePartsDic[PartsType.LArm].Length; x++)
        {
            newData.HaveLArm += UnitDataMaster.HavePartsDic[PartsType.LArm][x].ToString() + ",";
        }
        for (int x = 0; x < UnitDataMaster.HavePartsDic[PartsType.Leg].Length; x++)
        {
            newData.HaveLeg += UnitDataMaster.HavePartsDic[PartsType.Leg][x].ToString() + ",";
        }
        for (int x = 0; x < UnitDataMaster.HavePartsDic[PartsType.Weapon].Length; x++)
        {
            newData.HaveWeapon += UnitDataMaster.HavePartsDic[PartsType.Weapon][x].ToString() + ",";
        }
        return newData;
    }
    void Load(GameData loadData)
    {
        var unitHead = loadData.UnitHeadID.Split(',');
        var unitBody = loadData.UnitBodyID.Split(',');
        var unitRArm = loadData.UnitRArmID.Split(',');
        var unitLArm = loadData.UnitLArmID.Split(',');
        var unitLeg = loadData.UnitLegID.Split(',');
        var unitRWeapon = loadData.WeaponRArmID.Split(',');
        var unitLWeapon = loadData.WeaponLArmID.Split(',');
        var unitColor = loadData.UnitColor.Split(',');
        var haveHead = loadData.HaveHead.Split(',');
        var haveBody = loadData.HaveBody.Split(',');
        var haveRArm = loadData.HaveRArm.Split(',');
        var haveLArm = loadData.HaveLArm.Split(',');
        var haveLeg = loadData.HaveLeg.Split(',');
        var haveWeapon = loadData.HaveWeapon.Split(',');
        for (int i = 0; i < UnitDataMaster.MaxUintCount; i++)
        {
            UnitDataMaster.SetData(i, new UnitBuildData(Int32.Parse(unitHead[i]), Int32.Parse(unitBody[i]),
                Int32.Parse(unitRArm[i]), Int32.Parse(unitLArm[i]), Int32.Parse(unitLeg[i]),
                Int32.Parse(unitRWeapon[i]), Int32.Parse(unitLWeapon[i])), Int32.Parse(unitColor[i]));
        }
        for (int x = 0; x < UnitDataMaster.HavePartsDic[PartsType.Head].Length; x++)
        {
            UnitDataMaster.HavePartsDic[PartsType.Head][x] = Int32.Parse(haveHead[x]);
        }
        for (int x = 0; x < UnitDataMaster.HavePartsDic[PartsType.Body].Length; x++)
        {
            UnitDataMaster.HavePartsDic[PartsType.Body][x] = Int32.Parse(haveBody[x]);
        }
        for (int x = 0; x < UnitDataMaster.HavePartsDic[PartsType.RArm].Length; x++)
        {
            UnitDataMaster.HavePartsDic[PartsType.RArm][x] = Int32.Parse(haveRArm[x]);
        }
        for (int x = 0; x < UnitDataMaster.HavePartsDic[PartsType.LArm].Length; x++)
        {
            UnitDataMaster.HavePartsDic[PartsType.LArm][x] = Int32.Parse(haveLArm[x]);
        }
        for (int x = 0; x < UnitDataMaster.HavePartsDic[PartsType.Leg].Length; x++)
        {
            UnitDataMaster.HavePartsDic[PartsType.Leg][x] = Int32.Parse(haveLeg[x]);
        }
        for (int x = 0; x < UnitDataMaster.HavePartsDic[PartsType.Weapon].Length; x++)
        {
            UnitDataMaster.HavePartsDic[PartsType.Weapon][x] = Int32.Parse(haveWeapon[x]);
        }
    }
}
