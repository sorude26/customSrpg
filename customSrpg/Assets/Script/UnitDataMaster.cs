using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDataMaster
{
    public const int MaxUintCount = 8;
    public int HaveUnitNumber { get; private set; } = 8;
    public static UnitBuildData[] PlayerUnitBuildDatas { get; private set; } = new UnitBuildData[MaxUintCount];
    public static int[] PlayerColors { get; private set; } = new int[MaxUintCount];
    public static Dictionary<PartsType, int[]> HavePartsDic = new Dictionary<PartsType, int[]>();
    public static void SetData(int number,UnitBuildData data,int color)
    {
        if (number >= MaxUintCount || number < 0)
        {
            Debug.Log("指定対象は存在しません");
            return;
        }
        PlayerUnitBuildDatas[number] = data;
        PlayerColors[number] = color;
    }
    public static void StartSet(UnitPartsList partsList)
    {
        int[] allparts = new int[partsList.GetAllBodys().Length];
        HavePartsDic.Add(PartsType.Body, allparts); 
        allparts = new int[partsList.GetAllHeads().Length];
        HavePartsDic.Add(PartsType.Head, allparts); 
        allparts = new int[partsList.GetAllRArms().Length];
        HavePartsDic.Add(PartsType.RArm, allparts);
        allparts = new int[partsList.GetAllLArms().Length];
        HavePartsDic.Add(PartsType.LArm, allparts);
        allparts = new int[partsList.GetAllLegs().Length];
        HavePartsDic.Add(PartsType.Leg, allparts);
        allparts = new int[partsList.GetAllWeapons().Length];
        HavePartsDic.Add(PartsType.Weapon, allparts);
        for (int i = 0; i < PlayerColors.Length; i++)
        {
            PlayerColors[i] = 22;
        }
    }
}
