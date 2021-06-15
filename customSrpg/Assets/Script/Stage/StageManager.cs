﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 現在のステージの進行を管理するクラス
/// </summary>
public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }
    [SerializeField] Unit m_testUnit;
    private void Awake()
    {
        Instance = this;
    }
    public void OnClickMoveSearch()
    {
        foreach (var panel in MapManager.Instance.StartSearch(m_testUnit))
        {
            panel.StagePanel.ViewMovePanel();
        }
    }
}
