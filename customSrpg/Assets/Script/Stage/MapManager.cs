using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }
    /// <summary>  </summary>
    [SerializeField] MapCreater m_mapCreater;
    /// <summary> マップの全情報 </summary>
    public MapData[] MapDatas { get; private set; }
    /// <summary> 移動範囲 </summary>
    public List<MapData> MoveList { get; private set; }
    /// <summary> ステージの最大X座標 </summary>
    [SerializeField] int m_maxX = 15;
    /// <summary> ステージの最大Z座標 </summary>
    [SerializeField] int m_maxZ = 15;
    /// <summary> ステージの最大X座標 </summary>
    public int MaxX { get => m_maxX; }
    /// <summary> ステージの最大Z座標 </summary>
    public int MaxZ { get => m_maxZ; }
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        MapDatas = m_mapCreater.MapCreate(m_maxX, m_maxZ,this.transform);
        MoveList = new List<MapData>();
    }
}
