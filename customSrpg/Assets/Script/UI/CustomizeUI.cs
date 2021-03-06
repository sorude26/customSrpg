using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CustomizeUI : MonoBehaviour
{
    [SerializeField] float m_changeSpeed = 0.2f;
    bool m_cursorMove;
    public event Action<float, float> OnCursor;
    private void Start()
    {
        GameScene.InputManager.Instance.OnInputArrow += CursorMove;
    }
    /// <summary>
    /// カーソルを移動させる
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void CursorMove(float x, float y)
    {
        if (m_cursorMove)
        {
            return;
        }
        StartCoroutine(CursorMoveStart(x, y));
    }
    IEnumerator CursorMoveStart(float x, float y)
    {
        m_cursorMove = true;
        OnCursor?.Invoke(x, y);
        yield return new WaitForSeconds(m_changeSpeed);
        m_cursorMove = false;
    }
}
