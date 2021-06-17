using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Camera MainCamera { get; private set; }
    public Transform MainCameraTrans { get; private set; }
    [SerializeField] GameObject m_cameraBase;
    void Start()
    {
        MainCamera = GetComponentInChildren<Camera>();
        MainCameraTrans = MainCamera.transform;
    }
}
