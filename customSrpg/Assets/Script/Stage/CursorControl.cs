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
    /// <summary> ステージの拡大率記憶用数 </summary>
    int m_stageScale;
    /// <summary> ステージのサイズ記憶用数 </summary>
    Vector2Int m_stageSize;
    /// <summary> 現在のカーソル座標X </summary>
    int m_currentPosX;
    /// <summary> 現在のカーソル座標Z </summary>
    int m_currentPosZ;
    /// <summary> カーソル操作停止フラグ </summary>
    bool m_notCursor;
    /// <summary> 描画されるカーソル </summary>
    [SerializeField] GameObject m_cursor;
    /// <summary> 移動処理フラグ </summary>
    bool m_move;    
    /// <summary> 移動入力待ち時間 </summary>
    float m_moveTime = 0.03f;
    /// <summary> 移動待機タイマー </summary>
    float m_moveTimer = 0;
    /// <summary> 連続入力タイマー </summary>
    float m_timer = 0;
    /// <summary> 連続移動フラグ </summary>
    bool m_second;
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
                    if (m_timer < 0.15f && !m_second)
                    {
                        CursorMove(x, z);
                        m_second = true;
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
                m_second = false;
            }
        }
        if (m_move && m_moveTimer <= 0)
        {
            this.transform.position = new Vector3(m_currentPosX * m_stageScale, MapManager.Instance.GetLevel(m_currentPosX, m_currentPosZ), m_currentPosZ * m_stageScale);
            m_move = false;
            m_moveTimer = m_moveTime;
            //Debug.Log(StageManager.Instance.GetPositionUnit(m_currentPosX, m_currentPosZ));
            return;
        }
    }
    /// <summary>
    /// カーソルを非表示にし、操作不能にする
    /// </summary>
    public void CursorStop()
    {
        m_notCursor = true;
        //m_cursor.SetActive(false);
    }
    public void CursorStart()
    {
        m_notCursor = false;
        //m_cursor.SetActive(true);
    }
    /// <summary>
    /// カーソルを指定箇所に移動する
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    public void CursorWarp(int x,int z)
    {
        m_currentPosX = x;
        m_currentPosZ = z;
        transform.position = new Vector3(m_currentPosX * m_stageScale, MapManager.Instance.GetLevel(m_currentPosX,m_currentPosZ), m_currentPosZ * m_stageScale);
    }
    /// <summary>
    /// カーソルを指定ユニットの場所に移動する
    /// </summary>
    /// <param name="unit"></param>
    public void CursorWarp(Unit unit)
    {
        if (!unit)
        {
            return;
        }
        m_currentPosX = unit.CurrentPosX;
        m_currentPosZ = unit.CurrentPosZ;
        transform.position = new Vector3(m_currentPosX * m_stageScale, MapManager.Instance.GetLevel(m_currentPosX, m_currentPosZ), m_currentPosZ * m_stageScale);
    }
    /// <summary>
    /// 入力が地形の範囲内であればカーソルの座標を変更し、移動処理フラグをTrueにする
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
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
