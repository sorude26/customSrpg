using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StageLevelData : ScriptableObject
{
    [SerializeField] int[] m_maxSortieUnit;
    [SerializeField] int[] m_stageSizeID;
    [SerializeField] string[] m_stageGuide;
    public int[] MaxSortieUnit { get => m_maxSortieUnit; }
    public int[] StageSizeID { get => m_stageSizeID; }
    public string[] StageGuide { get => m_stageGuide; }
}
