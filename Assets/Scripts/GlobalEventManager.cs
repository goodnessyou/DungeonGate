using System;

public class GlobalEventManager
{
    public static Action OnEnemyKilled;

    public static void SendEnemyKilled()
    {
        if(OnEnemyKilled != null) OnEnemyKilled.Invoke();
    }
}
