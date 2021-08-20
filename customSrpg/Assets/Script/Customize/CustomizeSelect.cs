﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Customize
{
    public class CustomizeSelect : MonoBehaviour
    {
        [SerializeField] CustomizeUI m_ui;
        [SerializeField] CustomizeModel[] m_allModels;
        [SerializeField] GameObject m_cameraTarget;
        [SerializeField] ColorChangeControl m_colorControl;
        [SerializeField] CommandControl m_commandControl;
        CustomizeModel m_selectModel;
        CommandBox[] m_commands;
        int m_number = 0;
        int m_maxNumber;
        void Start()
        {
            m_commands = m_commandControl.StartSet();
            m_colorControl.StartSet();
            m_maxNumber = GameManager.Instanse.HaveUnitNumber;
            for (int i = 0; i < m_maxNumber; i++)
            {
                m_allModels[i].StartSet(m_colorControl.GetColor);
            }
            m_colorControl.OnColorChange += ChangeColor;
            ModelSet();
            m_ui.OnCursor += m_colorControl.CursorMove;
            StartCoroutine(Test());
        }
        IEnumerator Test()
        {
            yield return new WaitForEndOfFrame();
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
            m_colorControl.SetTargetColor(m_allModels[m_number].ColorNum);
        }
        public void ChangeColor(Color color,int number)
        {
            m_selectModel.ChangeColor(color,number);
        }
        public void ChangeParts(UnitBuildData buildData)
        {
            m_selectModel.ChangeParts(buildData);
        }
    }
}
