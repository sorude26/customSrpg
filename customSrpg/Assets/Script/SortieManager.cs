using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortieManager
{
    public static int[] SoriteUnit { get; private set; }
    public static int SoriteNumber { get; private set; }
    public static int StageLevel { get; private set; }
    public static int AlliesNumber { get; private set; }
    public static int StageSizeLevel { get; private set; }
    public static void SetStageData(int soriteNumber, int level, int alliesNumber)
    {
        SoriteNumber = soriteNumber;
        StageLevel = level;
        AlliesNumber = alliesNumber;
    }
    public static void SetPlayer(int[] sriteUnit)
    {
        SoriteUnit = sriteUnit;
    }
}
