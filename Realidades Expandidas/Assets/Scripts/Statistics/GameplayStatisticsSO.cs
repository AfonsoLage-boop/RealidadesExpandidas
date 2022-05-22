using UnityEngine;
using System;

/// <summary>
/// Scriptable object with gameplay statistics.
/// </summary>
[CreateAssetMenu(fileName = "Gameplay Statistics", menuName = "Gameplay Statistics")]
public class GameplayStatisticsSO : ScriptableObject
{
    [Range(100, 1000)][SerializeField] private uint goodScore;
    [Range(100, 1000)][SerializeField] private uint perfectScore;
    [Range(1, 10)] [SerializeField] private int defaultLives;
    [SerializeField] private SpawnableObjectStatsSO spawnableObjectsStats;

    public uint GoodScore => goodScore;
    public uint PerfectScore => perfectScore;

    public uint Score { get; private set; }
    public void AddGoodScore() => Score += goodScore;
    public void AddPerfectScore() => Score += perfectScore;

    private int lives;
    public int Lives
    {
        get => lives;
        set
        {
            lives = value;
        }
    }

    private int attemptsSucceeded;
    public int AttemptsSucceeded
    {
        get => attemptsSucceeded;
        set
        {
            attemptsSucceeded = value;

            // Speed multiplier logic
            if (value != 0 && 
                attemptsSucceeded % spawnableObjectsStats.TriesRequiredToIncrement == 0)
            {
                spawnableObjectsStats.IncrementSpeed();
            }
        }
    }

    private int attemptsFailed;
    public int AttemptsFailed
    {
        get => attemptsFailed;
        set
        {
            attemptsFailed = value;

            if (value != 0)
            {
                Lives--;
            }
        }
    }

    public void ResetStatistics()
    {
        Lives = defaultLives;
        Score = 0;
        AttemptsSucceeded = 0;
        AttemptsFailed = 0;
    }
}
