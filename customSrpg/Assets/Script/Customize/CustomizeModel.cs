using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カスタマイズシーンでの機体生成、変更を行う
/// </summary>
public class CustomizeModel : MonoBehaviour
{
    [SerializeField] int m_modelNum;
    UnitMaster m_master;
    UnitBuilder m_builder;
    UnitBuildData m_buildData;
    Color m_color;
    private void Start()
    {
        m_buildData = GameManager.Instanse.PlayerUnits[m_modelNum];
        m_color = GameManager.Instanse.PlayerColor[m_modelNum];
        m_builder = GetComponent<UnitBuilder>();
        m_master = GetComponent<UnitMaster>();
        m_builder.SetData(m_buildData, m_master);
        m_master.UnitColorChange(m_color);
    }
    public void ChangeColor(Color color)
    {
        m_color = color;
        m_master.UnitColorChange(m_color);
    }
    public void ChangeParts(UnitBuildData buildData)
    {
        m_buildData = buildData;
        m_builder.ResetBuild(m_buildData, m_master);
        m_master.UnitColorChange(m_color);
    }
    public void SetData()
    {
        //GameMnagerに保存を行う
    }
}
