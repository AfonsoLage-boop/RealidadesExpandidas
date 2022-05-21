using UnityEngine;

/// <summary>
/// Scriptable object with gameplay statistics.
/// </summary>
[CreateAssetMenu(fileName = "Gameplay Statistics", menuName = "Gameplay Statistics")]
public class GameplayStatisticsSO : ScriptableObject
{
    [Range(1, 10)] [SerializeField] private int defaultLives;
    [SerializeField] private SpawnableObjectStatsSO spawnableObjectsStats;

    private ObjectSpawner spawner;

    private int lives;
    public int Lives
    {
        get => lives;
        set
        {
            lives = value;

            // Gameover logic
            if (Lives <= 0)
            {
                Debug.Log("Gameover");
            }
        }
    }

    private int attemptsSucceeded;
    public int AttemptsSucceeded
    {
        get => attemptsSucceeded;
        set
        {
            if (spawner.IsPaused) return;

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
            if (spawner.IsPaused) return;

            attemptsFailed = value;

            if (value != 0)
            {
                Lives--;
            }
        }
    }

    private void OnEnable()
    {
        spawner = FindObjectOfType<ObjectSpawner>();
        if (spawner!=null)
        {
            Lives = defaultLives;
            AttemptsSucceeded = 0;
            AttemptsFailed = 0;
        }
    }
}
