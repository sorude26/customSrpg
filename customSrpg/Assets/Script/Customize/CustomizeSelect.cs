using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

namespace Customize
{
    public class CustomizeSelect : MonoBehaviour
    {
        public static CustomizeSelect Instance { get; private set; } 
        [SerializeField] CustomizeUI m_ui;
        [SerializeField] CustomizeModel[] m_allModels;
        [SerializeField] GameObject m_cameraTarget;
        [SerializeField] ColorChangeControl m_colorControl;
        /// <summary>
        /// 1:基本選択、2：機体選択、3：色相選択、4：機体パーツ選択、5：武器選択、6：決定選択、
        /// </summary>
        [SerializeField] CommandControl[] m_commandControls;
        int m_commandNumber = 0;
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
            m_maxNumber = GameManager.Instanse.HaveUnitNumber;
            for (int i = 0; i < m_maxNumber; i++)
            {
                m_allModels[i].StartSet(m_colorControl.GetColor);
            }
            m_colorControl.OnColorChange += ChangeColor;
            ModelSet();
            foreach (var command in m_commandControls)
            {
                command.StartSet();
            }
        }
        void CommandChange()
        {
            foreach (var command in m_commandControls)
            {
                command.gameObject.SetActive(false);
            }
            m_commandControls[m_commandNumber].gameObject.SetActive(true);
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
        public void SelectColorChange()
        {
            m_commandNumber = 3;
            CommandChange();
        }
        public void SelectTargetUnit()
        {
            m_commandNumber = 2;
            CommandChange();
        }
        public void SelectPartsChange()
        {
            m_commandNumber = 4;
            CommandChange();
        }
        void ModelSet()
        {
            m_selectModel = m_allModels[m_number];
            m_cameraTarget.transform.position = m_selectModel.CameraPos.position;
            m_colorControl.SetColor(m_allModels[m_number].ColorNum);
        }
        public void OpenColorPanel()
        {
            m_ui.OnCursor += m_colorControl.CursorMove;
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
    }
}
