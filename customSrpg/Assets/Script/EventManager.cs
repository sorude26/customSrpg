using System;

/// <summary>
/// イベントを管理する
/// </summary>
public class EventManager
{
    public static event Action OnStageGuideViewEnd;

    public static void StageGuideViewEnd()
    {
        OnStageGuideViewEnd?.Invoke();
    }
}
