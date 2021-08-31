using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBuildDataManager
{
    const int AllUintCount = 8;
    public int HaveUnitNumber { get; private set; } = 8;
    public static UnitBuildData[] PlayerUnitBuildDatas { get; private set; } = new UnitBuildData[AllUintCount];
    public static int[] PlayerColors { get; private set; } = new int[AllUintCount];
    public void SetData(int number,UnitBuildData data,int color)
    {
        if (number >= AllUintCount || number < 0)
        {
            Debug.Log("指定対象は存在しません");
            return;
        }
        PlayerUnitBuildDatas[number] = data;
        PlayerColors[number] = color;
    }
}
