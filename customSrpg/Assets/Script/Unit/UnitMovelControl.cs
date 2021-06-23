using System;
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
    /// <summary> 現在座標X </summary>
    public int CurrentPosX { get; private set; }
    /// <summary> 現在座標Z </summary>
    public int CurrentPosZ { get; private set; }
    /// <summary> 移動モードフラグ </summary>
    bool m_moveMode;
    /// <summary> 移動場所データ数 </summary>
    int m_moveCount;
    /// <summary> 移動前座標X </summary>
    int m_startPosX;
    /// <summary> 移動前座標Z </summary>
    int m_startPosZ;
    /// <summary> 移動前座標Y </summary>
    float m_startPosY;
    /// <summary> 移動中座標 </summary>
    Vector3 m_movePos;
    /// <summary> 移動目標座標 </summary>
    Vector3 m_targetPos;
    /// <summary> 移動速度 </summary>
    float m_moveSpeed = 20f;
    /// <summary> 上昇速度 </summary>
    float m_upSpeed = 15f;
    public float LiftingForce { get; set; }
    public event Action<int,int> PositionSet;
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

    /// <summary>
    /// 所有者を設定する
    /// </summary>
    /// <param name="owner"></param>
    public void SetOwner(Action<int,int> positionSet,int x,int z)
    {
        PositionSet = positionSet;
        m_gameMap = MapManager.Instance;
        CurrentPosX = x;
        CurrentPosZ = z;
        SetPos(x, z);
        StartWarp();
    }
    /// <summary>
    /// 位置を保存する
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    public void SetPos(int x, int z)
    {
        m_startPosX = x;
        m_startPosZ = z;
        m_startPosY = m_gameMap.GetLevel(x, z);
    }
    public void MoveEnd()
    {
        SkipMove();
        SetPos(CurrentPosX, CurrentPosZ);
    }
    /// <summary>
    /// ユニットを移動させる
    /// </summary>
    /// <returns></returns>
    IEnumerator UnitMove()
    {
        m_movePos = new Vector3(m_startPosX * m_gameMap.MapScale, m_startPosY, m_startPosZ * m_gameMap.MapScale);
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
        CurrentPosX = m_unitMoveList[0].x;
        CurrentPosZ = m_unitMoveList[0].y;
        PositionSet?.Invoke(CurrentPosX, CurrentPosZ);
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
        float targetLevel = m_gameMap.GetLevel(m_unitMoveList[m_moveCount].x, m_unitMoveList[m_moveCount].y);
        m_targetPos = new Vector3(m_unitMoveList[m_moveCount].x * m_gameMap.MapScale, targetLevel, m_unitMoveList[m_moveCount].y * m_gameMap.MapScale);
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    protected void Move()
    {
        if (m_movePos.x != m_targetPos.x) //移動・昇降、方向変更処理
        {
            MoveAround(ref m_movePos.x, m_targetPos.x, UnitAngle.Right, UnitAngle.Left);
        }
        else if (m_movePos.z != m_targetPos.z)
        {
            MoveAround(ref m_movePos.z, m_targetPos.z, UnitAngle.Up, UnitAngle.Down);            
        }
        UnitAngleControl();
    }
    /// <summary>
    /// 昇降処理
    /// </summary>
    void MoveUpDown()
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
    void MoveAround(ref float movePos,float targetPos,UnitAngle angle1,UnitAngle angle2)
    {
        if (movePos < targetPos)
        {
            if (unitAngle != angle1) { unitAngle = angle1; }

            if (targetPos - movePos <= m_gameMap.MapScale / 2 && m_movePos.y != m_targetPos.y)//昇降処理の確認
            {
                MoveUpDown();
            }
            else
            {
                movePos += m_moveSpeed * Time.deltaTime;
                if (movePos > targetPos) { movePos = targetPos; }
            }
        }
        else
        {
            if (unitAngle != angle2) { unitAngle = angle2; }

            if (movePos - targetPos <= m_gameMap.MapScale / 2 && m_movePos.y != m_targetPos.y)//昇降処理の確認
            {
                MoveUpDown();
            }
            else
            {
                movePos -= m_moveSpeed * Time.deltaTime;
                if (movePos < targetPos) { movePos = targetPos; }
            }
        }
    }
    /// <summary>
    /// 移動処理中ならば処理を停止しワープさせる。
    /// </summary>
    public void SkipMove()
    {
        if (m_moveMode)
        {
            StopAllCoroutines();
            Warp(m_unitMoveList[0].x, m_unitMoveList[0].y);
            m_moveMode = false;
        }        
    }
    /// <summary>
    /// 移動処理中ならば処理を停止しワープさせる。
    /// </summary>
    protected void SkipMove(int posX, int posZ)
    {
        if (m_moveMode)
        {
            StopAllCoroutines();
            Warp(posX, posZ);
            m_moveMode = false;
        }
    }
    protected void StartWarp()
    {
        m_startPosX = CurrentPosX;
        m_startPosZ = CurrentPosZ;
        transform.position = new Vector3(m_startPosX * m_gameMap.MapScale, m_startPosY, m_startPosZ * m_gameMap.MapScale);
    }
    /// <summary>
    /// ユニットを指定箇所に瞬間移動させる
    /// </summary>
    /// <param name="posX"></param>
    /// <param name="posZ"></param>
    protected void Warp(int posX,int posZ)
    {
        CurrentPosX = posX;
        CurrentPosZ = posZ;
        PositionSet?.Invoke(CurrentPosX, CurrentPosZ);
        transform.position = new Vector3(posX * m_gameMap.MapScale, m_gameMap.MapDatas[posX + posZ * m_gameMap.MaxX].Level, posZ * m_gameMap.MapScale);
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
    /// <param name="liftingForce">現在の昇降力</param>
    public void UnitMoveSet(in MapData[] moveList, int targetX, int targetZ ,float liftingForce)
    {
        if (m_moveMode)
        {
            if (m_unitMoveList[0].x == targetX && m_unitMoveList[0].y == targetZ)
            {
                SkipMove(targetX,targetZ);
                return;
            }
            else
            {
                SkipMove(m_startPosX, m_startPosZ);
            }
        }
        if (targetX == m_startPosX && targetZ == m_startPosZ)
        {
            Warp(targetX, targetZ);
            return;
        }
        LiftingForce = liftingForce;
        m_unitMoveList = new List<Vector2Int>();
        m_unitMoveList.Add(new Vector2Int(targetX, targetZ)); //目標データ保存
        int p = m_gameMap.GetPosition(targetX, targetZ);
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
        if (Mathf.Abs(moveList[p].Level - currentLevel) > LiftingForce) { return; }
        movePower = moveList[p].MovePoint;
        Vector2Int pos = new Vector2Int(m_gameMap.MapDatas[p].PosX, m_gameMap.MapDatas[p].PosZ);
        m_unitMoveList.Add(pos); //移動順データ保存
        if (m_startPosX == m_gameMap.MapDatas[p].PosX && m_startPosZ == m_gameMap.MapDatas[p].PosZ) //初期地点か確認
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
