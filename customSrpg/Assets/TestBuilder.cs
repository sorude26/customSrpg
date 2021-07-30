using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// テストシーンでのユニットの組み立てを行う
/// </summary>
public class TestBuilder : MonoBehaviour
{
    [SerializeField] UnitBuildData m_buildData;
    [SerializeField] Color m_color;
    UnitBuilder m_builder;
    MotionController m_motion;
    UnitMaster m_master;
    private void Start()
    {
        m_builder = GetComponent<UnitBuilder>();
        m_motion = GetComponent<MotionController>();
        m_master = GetComponent<UnitMaster>();
        m_builder.SetData(m_buildData, m_master);
        m_motion.StartSet();
    }
    public void ChangeColor()
    {
        m_motion.Idle();
        StartCoroutine(Change());
    }
    IEnumerator Change()
    {
        yield return new WaitForSeconds(0.01f);
        m_builder.ResetBuild(m_buildData, m_master);
        m_master.UnitColorChange(m_color);
    }
}
