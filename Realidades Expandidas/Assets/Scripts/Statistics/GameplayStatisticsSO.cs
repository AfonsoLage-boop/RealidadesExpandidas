using UnityEngine;

/// <summary>
/// Scriptable object with gameplay statistics.
/// </summary>
[CreateAssetMenu(fileName = "Gameplay Statistics", menuName = "Gameplay Statistics")]
public class GameplayStatisticsSO : ScriptableObject
{
    [SerializeField] private SpawnableObjectStatsSO spawnableObjectsStats;

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
        }
    }

    private void OnEnable()
    {
        AttemptsSucceeded = 0;
        AttemptsFailed = 0;
    }
}
