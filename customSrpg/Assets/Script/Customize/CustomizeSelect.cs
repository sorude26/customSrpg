using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Customize
{
    public class CustomizeSelect : MonoBehaviour
    {
        [SerializeField] CustomizeModel[] m_allModels;
        [SerializeField] GameObject m_cameraTarget;
        CustomizeModel m_selectModel;
        int m_number = 0;
        int m_maxNumber;
        void Start()
        {
            m_maxNumber = GameManager.Instanse.HaveUnitNumber;
            for (int i = 0; i < m_maxNumber; i++)
            {
                m_allModels[i].StartSet();
            }
            ModelSet();
        }
        public void NextModel()
        {
            m_number++;
            if (m_number >= m_maxNumber)
            {
                m_number = 0;
            }
            ModelSet();
        }
        public void BeforeModel()
        {
            m_number--;
            if (m_number < 0)
            {
                m_number = m_maxNumber - 1;
            }
            ModelSet();
        }
        void ModelSet()
        {
            m_selectModel = m_allModels[m_number];
            m_cameraTarget.transform.position = m_selectModel.CameraPos.position;
        }
        public void ChangeColor(Color color)
        {
            m_selectModel.ChangeColor(color);
        }
        public void ChangeParts(UnitBuildData buildData)
        {
            m_selectModel.ChangeParts(buildData);
        }
    }
}
