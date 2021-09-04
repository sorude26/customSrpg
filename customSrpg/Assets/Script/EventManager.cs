using System;

/// <summary>
/// イベントを管理する
/// </summary>
public class EventManager
{
    public static event Action OnGameStart;
    public static event Action OnStageGuideViewEnd;
    public static event Action OnAttackSearchEnd;

    public static void GameStart()
    {
        OnGameStart?.Invoke();
    }
    public static void StageGuideViewEnd()
    {
        OnStageGuideViewEnd?.Invoke();
    }
    public static void AttackSearchEnd()
    {
        OnAttackSearchEnd?.Invoke();
    }
}
