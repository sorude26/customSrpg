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
        UnitMaster m_master;
        UnitBuilder m_builder;
        UnitBuildData m_buildData;
        Color m_color;
        public Transform CameraPos { get; private set; }

        public void StartSet()
        {
            m_buildData = GameManager.Instanse.PlayerUnits[m_modelNum];
            m_color = GameManager.Instanse.PlayerColor[m_modelNum];
            m_builder = GetComponent<UnitBuilder>();
            m_master = GetComponent<UnitMaster>();
            CameraPos = m_builder.SetDataModel(m_buildData, m_master);
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
            CameraPos = m_builder.ResetBuildModel(m_buildData, m_master);
            m_master.UnitColorChange(m_color);
        }
        public void SetData()
        {
            //GameMnagerに保存を行う
        }
    }
}
