using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;
using UnityEngine.SceneManagement;

namespace Customize
{
    public class CustomizeManager : MonoBehaviour
    {
        public static CustomizeManager Instance { get; private set; } 
        [SerializeField] CustomizeUI m_ui;
        [SerializeField] CustomizeModel[] m_allModels;
        [SerializeField] GameObject m_cameraTarget;
        [SerializeField] ColorChangeControl m_colorControl;
        [SerializeField] UnitParameterView m_view;
        CustomizeModel m_selectModel;
        int m_number = 0;
        int m_maxNumber;
        private void Awake()
        {
            Instance = this;
        }
        void Start()
        {
            m_colorControl.StartSet();
            m_maxNumber = UnitBuildDataMaster.MaxUintCount;
            for (int i = 0; i < m_maxNumber; i++)
            {
                m_allModels[i].StartSet(m_colorControl.GetColor);
            }
            m_colorControl.OnColorChange += ChangeColor;
            ModelSet();
            m_colorControl.ClosePanel();
            FadeController.Instance.StartFadeIn();
        }
        public void CursorMove(float x, float y)
        {
            if (x > 0)
            {
                NextModel();
            }
            else if (x < 0)
            {
                BeforeModel();
            }
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
            m_colorControl.SetColor(m_allModels[m_number].ColorNum);
            StartCoroutine(SetParameter());
        }
        IEnumerator SetParameter()
        {
            yield return new WaitForEndOfFrame();
            m_view.SetParameter(m_selectModel.Mastar);
        }
        public Action<float,float> OpenColorPanel()
        {
            m_colorControl.OpenPanel();
            return m_colorControl.CursorMove;
        }
        public void CloseColorPanel()
        {
            m_colorControl.ClosePanel();
        }
        public void ChangeColor(Color color,int number)
        {
            m_selectModel.ChangeColor(color,number);
        }
        public void ChangeParts(UnitBuildData buildData)
        {
            m_selectModel.ChangeParts(buildData);
        }
        public void ChangePartsBody(int id)
        {
            var data = m_selectModel.BuildData;
            data.BodyID = id;
            m_selectModel.ChangeParts(data);
            ModelSet();
        }
        public void ChangePartsHead(int id)
        {
            var data = m_selectModel.BuildData;
            data.HeadID = id;
            m_selectModel.ChangeParts(data);
            ModelSet();
        }
        public void ChangePartsRArm(int id)
        {
            var data = m_selectModel.BuildData;
            data.RArmID = id;
            m_selectModel.ChangeParts(data);
            ModelSet();
        }
        public void ChangePartsLArm(int id)
        {
            var data = m_selectModel.BuildData;
            data.LArmID = id;
            m_selectModel.ChangeParts(data);
            ModelSet();
        }
        public void ChangePartsLeg(int id)
        {
            var data = m_selectModel.BuildData;
            data.LegID = id;
            m_selectModel.ChangeParts(data);
            ModelSet();
        }
        public void ChangePartsRArmWeapon(int id)
        {
            var data = m_selectModel.BuildData;
            data.WeaponRArmID = id;
            m_selectModel.ChangeParts(data);
            ModelSet();
        }
        public void ChangePartsLArmWeapon(int id)
        {
            var data = m_selectModel.BuildData;
            data.WeaponLArmID = id;
            m_selectModel.ChangeParts(data);
            ModelSet();
        }
        public void DataSet()
        {
            foreach (var model in m_allModels)
            {
                model.SaveModelData();
            }
            SceneManager.LoadScene("SampleScene");
        }
    }
}
