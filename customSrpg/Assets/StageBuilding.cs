using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBuilding : MonoBehaviour
{
    [SerializeField] GameObject m_symbol;
    [SerializeField] GameObject m_silhouette;
    private void Start()
    {
        m_symbol.SetActive(true);
        m_silhouette.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_silhouette.SetActive(true);
            m_symbol.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_symbol.SetActive(true);
            m_silhouette.SetActive(false);
        }
    }
}
