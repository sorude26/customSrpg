using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CursorState
{
    NormalMode,
    MoveMode,
}
public class CursorControl : MonoBehaviour
{
    int m_stageScale;
    Vector2Int m_stageSize;
    int m_currentPosX;
    int m_currentPosZ;
    bool m_notCursor;
    bool m_move;
    float m_moveTimer = 0;
    float m_moveTime = 0.03f;
    float m_timer = 0;
    bool m_test;
    void Start()
    {
        m_stageScale = MapManager.Instance.MapScale;
        m_stageSize.x = MapManager.Instance.MaxX;
        m_stageSize.y = MapManager.Instance.MaxZ;
    }

    void Update()
    {
        if (m_notCursor)
        {
            return;
        }
        if (m_moveTimer > 0)
        {
            m_moveTimer -= Time.deltaTime;
            return;
        }
        if (!m_move)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("cursor");
                StageManager.Instance.PointMoveTest(m_currentPosX, m_currentPosZ);
                return;
            }
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            if (Input.GetButtonDown("Horizontal"))
            {
                CursorMove(x, z);
                m_timer = 0.3f;
            }
            else if (Input.GetButtonDown("Vertical"))
            {
                CursorMove(x, z);
                m_timer = 0.3f;
            }
            else if (x != 0 || z != 0)
            {
                if (m_timer > 0)
                {
                    m_timer -= Time.deltaTime;
                    if (m_timer < 0.15f && !m_test)
                    {
                        CursorMove(x, z);
                        m_test = true;
                    }
                }
                else
                {
                    CursorMove(x, z);
                }
            }
            else
            {
                m_timer = 0f;
                m_test = false;
            }
        }
        if (m_move && m_moveTimer <= 0)
        {
            this.transform.position = new Vector3(m_currentPosX * m_stageScale, 0, m_currentPosZ * m_stageScale);
            m_move = false;
            m_moveTimer = m_moveTime;
            return;
        }
    }
    public void CursorStop()
    {
        m_notCursor = true;
    }
    void CursorMove(float x,float z)
    {
        if (x == 0 && z == 0)
        {
            return;
        }
        if (x != 0)
        {
            if (x > 0 && m_stageSize.x - 1 > m_currentPosX)
            {
                m_currentPosX++;
                m_move = true;
                m_moveTimer = m_moveTime;
            }
            else if (x < 0 && 0 < m_currentPosX)
            {
                m_currentPosX--;
                m_move = true;
                m_moveTimer = m_moveTime;
            }
        }
        else
        {
            if (z > 0 && m_stageSize.y - 1 > m_currentPosZ)
            {
                m_currentPosZ++;
                m_move = true;
                m_moveTimer = m_moveTime;
            }
            else if (z < 0 && 0 < m_currentPosZ)
            {
                m_currentPosZ--;
                m_move = true;
                m_moveTimer = m_moveTime;
            }
        }
    }
}
