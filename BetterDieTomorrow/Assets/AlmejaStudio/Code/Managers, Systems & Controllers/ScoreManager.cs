using UnityEngine;

public static class ScoreManager
{
    private static int score = 0;

    public static int Score
    {
        get { return score; }
    }

    public static void IncrementScore(int points)
    {
        score += points;
    }

    public static void ResetScore()
    {
        score = 0;
    }
}