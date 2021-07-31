using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Camera MainCamera { get; private set; }
    public Transform MainCameraTrans { get; private set; }
    [SerializeField] GameObject m_cameraBase;
    [SerializeField] Vector3 m_cameraPos = new Vector3(-40, 40, -40);
    [SerializeField] Vector3 m_forcusPos = new Vector3(-20, 10, -20);
    [SerializeField] float m_focusSpeed = 2f;
    void Start()
    {
        MainCamera = GetComponentInChildren<Camera>();
        MainCameraTrans = MainCamera.transform;
    }
    public IEnumerator PointFocus()
    {
        transform.localRotation = Quaternion.Euler(10, 45, 0);
        StopAllCoroutines();
        yield return StartCoroutine(FocusMove(m_forcusPos));
    }
    public IEnumerator FocusEnd()
    {
        transform.localRotation = Quaternion.Euler(35, 45, 0);
        StopAllCoroutines();
        yield return StartCoroutine(FocusMove(m_cameraPos));
    }
    IEnumerator FocusMove(Vector3 targetPos)
    {
        while (transform.localPosition != targetPos)
        {
            transform.localPosition= Vector3.MoveTowards(transform.localPosition, targetPos, m_focusSpeed);
            yield return new WaitForEndOfFrame();
        }
    }
}
