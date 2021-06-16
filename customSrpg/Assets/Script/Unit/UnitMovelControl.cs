using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユニットの移動関係の処理を行うクラス
/// </summary>
public class UnitMovelControl : MonoBehaviour
{
    /// <summary> 移動座標の保存場所 </summary>
    List<Vector2Int> m_unitMoveList;
    /// <summary> マップデータの保存場所 </summary>
    MapManager m_gameMap;
    /// <summary> このスクリプトを持つユニット </summary>
    Unit m_owner;
    /// <summary> 移動モードフラグ </summary>
    bool m_moveMode;
    /// <summary> 移動場所データ数 </summary>
    int m_moveCount;
    /// <summary> 現在座標X </summary>
    int m_currentPosX;
    /// <summary> 現在座標X </summary>
    int m_currentPosZ;
    /// <summary> 現在座標Y </summary>
    float m_currentPosY;
    /// <summary> 移動中座標 </summary>
    Vector3 m_movePos;
    /// <summary> 移動目標座標 </summary>
    Vector3 m_targetPos;
    /// <summary> 移動速度 </summary>
    float m_moveSpeed = 10f;
    /// <summary> 上昇速度 </summary>
    float m_upSpeed = 5f;

    /// <summary>
    /// ユニット向き4方向
    /// </summary>
    public enum UnitAngle
    {
        Up,
        Down,
        Left,
        Right,
    }
    [SerializeField] public UnitAngle unitAngle = UnitAngle.Down;
    protected UnitAngle currentAngle;
    private void Start()
    {
        m_gameMap = MapManager.Instance;
    }

    /// <summary>
    /// 所有者を設定する
    /// </summary>
    /// <param name="owner"></param>
    public void SetOwner(Unit owner)
    {
        m_owner = owner;
    }
    /// <summary>
    /// 位置を保存する
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public void SetPos(int x, float y, int z)
    {
        m_currentPosX = x;
        m_currentPosY = y;
        m_currentPosZ = z;
    }
    /// <summary>
    /// ユニットを移動させる
    /// </summary>
    /// <returns></returns>
    IEnumerator UnitMove()
    {
        m_movePos = new Vector3(m_currentPosX * m_gameMap.MapScale, m_currentPosY, m_currentPosZ * m_gameMap.MapScale);
        TargetSet();
        while (m_moveCount >= 0)
        {
            if (m_targetPos != m_movePos)
            {
                Move();
            }
            else
            {
                m_moveCount--;
                TargetSet();
            }
            this.transform.position = m_movePos;
            yield return new WaitForEndOfFrame();
        }
        m_moveMode = false;
    }

    /// <summary>
    /// 目標地点を入力する
    /// </summary>
    void TargetSet()
    {
        if (m_moveCount < 0)
        {
            return;
        }
        float targetLevel = m_gameMap.MapDatas[m_unitMoveList[m_moveCount].x + (m_gameMap.MaxX * m_unitMoveList[m_moveCount].y)].Level;
        m_targetPos = new Vector3(m_unitMoveList[m_moveCount].x * m_gameMap.MapScale, targetLevel, m_unitMoveList[m_moveCount].y * m_gameMap.MapScale);
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    protected void Move()
    {
        if (m_movePos.x != m_targetPos.x) //移動・昇降、方向変更処理
        {
            if (m_movePos.x < m_targetPos.x)
            {
                if (unitAngle != UnitAngle.Right) { unitAngle = UnitAngle.Right; }

                if (m_targetPos.x - m_movePos.x <= m_gameMap.MapScale / 2 && m_movePos.y != m_targetPos.y)//昇降処理の確認
                {
                    if (m_movePos.y > m_targetPos.y)
                    {
                        m_movePos.y -= m_upSpeed * Time.deltaTime;
                        if (m_movePos.y < m_targetPos.y) { m_movePos.y = m_targetPos.y; }
                    }
                    else
                    {
                        m_movePos.y += m_upSpeed * Time.deltaTime;
                        if (m_movePos.y > m_targetPos.y) { m_movePos.y = m_targetPos.y; }
                    }
                }
                else
                {
                    m_movePos.x += m_moveSpeed * Time.deltaTime;
                    if (m_movePos.x > m_targetPos.x) { m_movePos.x = m_targetPos.x; }
                }
            }
            else
            {
                if (unitAngle != UnitAngle.Left) { unitAngle = UnitAngle.Left; }

                if (m_movePos.x - m_targetPos.x <= m_gameMap.MapScale / 2 && m_movePos.y != m_targetPos.y)//昇降処理の確認
                {
                    if (m_movePos.y > m_targetPos.y)
                    {
                        m_movePos.y -= m_upSpeed * Time.deltaTime;
                        if (m_movePos.y < m_targetPos.y) { m_movePos.y = m_targetPos.y; }
                    }
                    else
                    {
                        m_movePos.y += m_upSpeed * Time.deltaTime;
                        if (m_movePos.y > m_targetPos.y) { m_movePos.y = m_targetPos.y; }
                    }
                }
                else
                {
                    m_movePos.x -= m_moveSpeed * Time.deltaTime;
                    if (m_movePos.x < m_targetPos.x) { m_movePos.x = m_targetPos.x; }
                }
            }
        }
        else if (m_movePos.z != m_targetPos.z)
        {
            if (m_movePos.z < m_targetPos.z)
            {
                if (unitAngle != UnitAngle.Up) { unitAngle = UnitAngle.Up; }

                if (m_targetPos.z - m_movePos.z <= m_gameMap.MapScale / 2 && m_movePos.y != m_targetPos.y)//昇降処理の確認
                {
                    if (m_movePos.y > m_targetPos.y)
                    {
                        m_movePos.y -= m_upSpeed * Time.deltaTime;
                        if (m_movePos.y < m_targetPos.y) { m_movePos.y = m_targetPos.y; }
                    }
                    else
                    {
                        m_movePos.y += m_upSpeed * Time.deltaTime;
                        if (m_movePos.y > m_targetPos.y) { m_movePos.y = m_targetPos.y; }
                    }
                }
                else
                {
                    m_movePos.z += m_moveSpeed * Time.deltaTime;
                    if (m_movePos.z > m_targetPos.z) { m_movePos.z = m_targetPos.z; }
                }
            }
            else
            {
                if (unitAngle != UnitAngle.Down) { unitAngle = UnitAngle.Down; }

                if (m_movePos.z - m_targetPos.z <= m_gameMap.MapScale / 2 && m_movePos.y != m_targetPos.y)//昇降処理の確認
                {
                    if (m_movePos.y > m_targetPos.y)
                    {
                        m_movePos.y -= m_upSpeed * Time.deltaTime;
                        if (m_movePos.y < m_targetPos.y) { m_movePos.y = m_targetPos.y; }
                    }
                    else
                    {
                        m_movePos.y += m_upSpeed * Time.deltaTime;
                        if (m_movePos.y > m_targetPos.y) { m_movePos.y = m_targetPos.y; }
                    }
                }
                else
                {
                    m_movePos.z -= m_moveSpeed * Time.deltaTime;
                    if (m_movePos.z < m_targetPos.z) { m_movePos.z = m_targetPos.z; }
                }
            }
        }
        UnitAngleControl();
    }
    /// <summary>
    /// 向き変更されていたらモデルをその方向へ向ける
    /// </summary>
    protected void UnitAngleControl()
    {
        if (currentAngle == unitAngle)
        {
            return;
        }
        currentAngle = unitAngle;
        switch (currentAngle)
        {
            case UnitAngle.Up:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case UnitAngle.Down:
                transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
            case UnitAngle.Left:
                transform.rotation = Quaternion.Euler(0, 270, 0);
                break;
            case UnitAngle.Right:
                transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 検索範囲の移動経路検索し移動開始指示を出す
    /// </summary>
    /// <param name="moveList">検索範囲</param>
    /// <param name="targetX">開始地点X軸</param>
    /// <param name="targetZ">開始地点Z軸</param>
    public void UnitMoveSet(in MapData[] moveList, int targetX, int targetZ)
    {
        if (m_moveMode)
        {
            return;
        }
        m_unitMoveList = new List<Vector2Int>();
        Vector2Int pos = new Vector2Int(targetX, targetZ);
        m_unitMoveList.Add(pos); //目標データ保存
        int p = targetX + (targetZ * m_gameMap.MaxX);
        SearchCross(p, moveList[p].MovePoint, moveList);
    }

    /// <summary>
    /// 十字方向の移動確認
    /// </summary>
    /// <param name="p">現在座標</param>
    /// <param name="movePower">移動力</param>
    /// <param name="moveList">移動範囲リスト</param>
    protected void SearchCross(int p, int movePower, in MapData[] moveList)
    {
        if (0 <= p && p < m_gameMap.MaxX * m_gameMap.MaxZ)
        {
            if (m_gameMap.MapDatas[p].PosZ > 0 && m_gameMap.MapDatas[p].PosZ < m_gameMap.MaxZ)
            {
                MoveSearchPos(p - m_gameMap.MaxX, movePower, moveList[p].Level, moveList, m_gameMap.GetMoveCost(m_gameMap.MapDatas[p].MapType));
            }
            if (m_gameMap.MapDatas[p].PosZ >= 0 && m_gameMap.MapDatas[p].PosZ < m_gameMap.MaxZ - 1)
            {
                MoveSearchPos(p + m_gameMap.MaxX, movePower, moveList[p].Level, moveList, m_gameMap.GetMoveCost(m_gameMap.MapDatas[p].MapType));
            }
            if (m_gameMap.MapDatas[p].PosX > 0 && m_gameMap.MapDatas[p].PosX < m_gameMap.MaxX)
            {
                MoveSearchPos(p - 1, movePower, moveList[p].Level, moveList, m_gameMap.GetMoveCost(m_gameMap.MapDatas[p].MapType));
            }
            if (m_gameMap.MapDatas[p].PosX >= 0 && m_gameMap.MapDatas[p].PosX < m_gameMap.MaxX - 1)
            {
                MoveSearchPos(p + 1, movePower, moveList[p].Level, moveList, m_gameMap.GetMoveCost(m_gameMap.MapDatas[p].MapType));
            }
        }
    }
    /// <summary>
    /// 対象座標の確認
    /// </summary>
    /// <param name="p">対象座標</param>
    /// <param name="movePower">移動力</param>
    /// <param name="currentLevel">現在高度</param>
    /// <param name="moveList">移動範囲リスト</param>
    /// <param name="moveCost">移動前座標の移動コスト</param>
    protected void MoveSearchPos(int p, int movePower, float currentLevel, in MapData[] moveList, int moveCost)
    {
        if (m_moveMode) { return; }//検索終了か確認
        if (p < 0 || p >= m_gameMap.MaxX * m_gameMap.MaxZ) { return; }//マップ範囲内か確認
        if (movePower + moveCost != moveList[p].MovePoint) { return; } //一つ前の座標か確認
        if (Mathf.Abs(moveList[p].Level - currentLevel) > m_owner.GetUnitData().GetLiftingForce()) { return; }
        movePower = moveList[p].MovePoint;
        Vector2Int pos = new Vector2Int(m_gameMap.MapDatas[p].PosX, m_gameMap.MapDatas[p].PosZ);
        m_unitMoveList.Add(pos); //移動順データ保存
        if (m_currentPosX == m_gameMap.MapDatas[p].PosX && m_currentPosZ == m_gameMap.MapDatas[p].PosZ) //初期地点か確認
        {
            m_moveMode = true; //移動モード移行
            m_moveCount = m_unitMoveList.Count - 1;//移動経路数を入力
            //StartUnitAngle();
            StartCoroutine(UnitMove());//移動開始
            return;
        }
        else
        {
            SearchCross(p, movePower, moveList);
        }
    }
}
