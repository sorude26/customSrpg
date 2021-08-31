using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Customize
{
    /// <summary>
    /// カスタマイズシーンでの機体生成、変更を行う
    /// </summary>
    public class CustomizeModel : MonoBehaviour
    {
        [SerializeField] int m_modelNum;
        public int ModelNum { get => m_modelNum; }
        UnitMaster m_master;
        UnitBuilder m_builder;
        UnitBuildData m_buildData;
        public UnitBuildData BuildData { get => m_buildData; }
        Color m_color;
        int m_colorNum;
        public int ColorNum { get => m_colorNum; }
        public Color ModelColor { get => m_color; }
        public Transform CameraPos { get; private set; }

        public void StartSet(Func<int,Color> getColor)
        {
            m_buildData = UnitBuildDataManager.PlayerUnitBuildDatas[m_modelNum];
            m_builder = GetComponent<UnitBuilder>();
            m_master = GetComponent<UnitMaster>();
            CameraPos = m_builder.SetDataModel(m_buildData, m_master);
            m_colorNum = UnitBuildDataManager.PlayerColors[m_modelNum];
            m_color = getColor.Invoke(m_colorNum);
            m_master.UnitColorChange(m_color);
        }
        public void ChangeColor(Color color,int number)
        {
            m_color = color;
            m_colorNum = number;
            m_master.UnitColorChange(m_color);
        }
        public void ChangeParts(UnitBuildData buildData)
        {
            m_buildData = buildData;
            CameraPos = m_builder.ResetBuildModel(m_buildData, m_master);
            m_master.UnitColorChange(m_color);
        }
        public void SaveModelData()
        {
            UnitBuildDataManager.SetData(m_modelNum, m_buildData, m_colorNum);
        }
    }
}
