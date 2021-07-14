using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBuilder : MonoBehaviour
{
    [SerializeField] UnitBuildData m_buildData;
    UnitBuilder m_builder;
    MotionController m_motion;
    private void Start()
    {
        m_builder = GetComponent<UnitBuilder>();
        m_motion = GetComponent<MotionController>();
        m_builder.BuildUnit(m_buildData);
        m_motion.StartSet();
    }
}
