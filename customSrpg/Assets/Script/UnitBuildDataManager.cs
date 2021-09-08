using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBuildDataManager
{
    public const int MaxUintCount = 8;
    public int HaveUnitNumber { get; private set; } = 8;
    public static UnitBuildData[] PlayerUnitBuildDatas { get; private set; } = new UnitBuildData[MaxUintCount];
    public static int[] PlayerColors { get; private set; } = new int[MaxUintCount];
    public static Dictionary<int, int[]> HavePartsDic = new Dictionary<int, int[]>();
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
}
