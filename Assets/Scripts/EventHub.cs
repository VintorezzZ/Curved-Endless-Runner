using System;

public static class EventHub
{
    public static event Action GameOvered;
    public static event Action<bool> GamePaused;
    public static event Action ScoreChanged;
    public static event Action<int> HealthChanged;

    public static void OnGameOvered()
    { 
        GameOvered?.Invoke();
    }
        
    public static void OnGamePaused(bool paused)
    { 
        GamePaused?.Invoke(paused);
    }
        
    public static void OnScoreChanged()
    { 
        ScoreChanged?.Invoke();
    }
        
    public static void OnHealthChanged(int health)
    { 
        HealthChanged?.Invoke(health);
    }
}