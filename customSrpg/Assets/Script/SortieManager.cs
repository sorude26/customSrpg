using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortieManager
{
    public static int[] SoriteUnit { get; private set; }
    public static int SoriteNumber { get; private set; }
    public static int StageLevel { get; private set; }
    public static int AlliesLevel { get; private set; }
    public static void SetStageData(int level, int alliesNumber)
    {
        StageLevel = level;
        AlliesLevel = alliesNumber;
    }
    public static void SetPlayer(int soriteNumber, int[] sriteUnit)
    {
        SoriteNumber = soriteNumber;
        SoriteUnit = sriteUnit;
    }
}
