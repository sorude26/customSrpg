using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectControl : MonoBehaviour
{
    /// <summary> 自身のParticleの入れ物 </summary>
    ParticleSystem[] m_particles = default;
    private void Awake()
    {
        m_particles = GetComponentsInChildren<ParticleSystem>();
        gameObject.SetActive(false);
    }

    
    void Update()
    {
        
    }

    public void Stop()
    {
        foreach (var particle in m_particles)
        {
            particle.Stop();
        }
    }
}
