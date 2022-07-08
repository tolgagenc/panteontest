using System;

public static class EventManager
{
    public static event Action start;
    public static event Action finish;
    public static event Action stumble;

    public static void StartEvent()
    {
        start?.Invoke();
    }
    public static void FinishEvent()
    {
        finish?.Invoke();
    }
    public static void StumbleEvent()
    {
        stumble?.Invoke();
    }
}