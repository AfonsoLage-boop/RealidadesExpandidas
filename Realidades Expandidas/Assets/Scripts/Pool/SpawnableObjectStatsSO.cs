using UnityEngine;

/// <summary>
/// Scriptable object with spawnable objects stats.
/// </summary>
[CreateAssetMenu(fileName = "Spawnable Object Stats", menuName = "Spawnable Objects")]
public class SpawnableObjectStatsSO : ScriptableObject
{
    [Header("Spawn time")]
    [Tooltip("Objects spawn every X seconds")]
    [Range(0.1f, 10f)] [SerializeField] private float defaultSpawnDelay = 2.5f;

    [Header("Default Speeds")]
    [Range(0.1f, 10f)] [SerializeField] private float defaultWallSpeed = 4;
    [Range(0.1f, 10f)] [SerializeField] private float defaultPowerUpSpeed = 4;

    [Header("Difficulty Incrementation")]
    [Tooltip("How many times must the player succeed to increment the speed")]
    [Range(1, 20)] [SerializeField] private int triesRequiredToIncrement = 4;
    [Tooltip("Speed will multiply by this value")]
    [Range(1f, 2f)] [SerializeField] private float speedIncrementMultiplier = 1f;
    [Tooltip("Maximum speed an object can have")]
    [Range(0.1f, 10f)] [SerializeField] private float maximumSpeedOfObjects = 10;

    // Default values
    public float DefaultSpawnDelay => defaultSpawnDelay;
    public float DefaultWallSpeed => defaultWallSpeed;
    public float DefaultPowerUpSpeed => defaultPowerUpSpeed;
    public float TriesRequiredToIncrement => triesRequiredToIncrement;
    public float SpeedIncrementMultiplier => speedIncrementMultiplier;
    public float MaximumSpeedOfObjects => maximumSpeedOfObjects;

    // Wall get set
    private float wallSpeed;
    public float WallSpeed
    {
        get => wallSpeed;
        set => wallSpeed = value;
    }

    // Power up get set
    private float powerUpSpeed;
    public float PowerUpSpeed
    {
        get => powerUpSpeed;
        set => powerUpSpeed = value;
    }

    public void IncrementSpeed()
    {
        WallSpeed *= speedIncrementMultiplier;
        PowerUpSpeed *= speedIncrementMultiplier;
        Debug.Log("Speed multiplied. Current wall speed = " + WallSpeed);
    }

    private void OnEnable()
    {
        WallSpeed = defaultWallSpeed;
        PowerUpSpeed = defaultPowerUpSpeed;
    }
}
