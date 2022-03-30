using UnityEngine;

/// <summary>
/// Scriptable object with spawnable objects stats.
/// </summary>
[CreateAssetMenu(fileName = "Spawnable Object Stats", menuName = "Spawnable Objects")]
public class SpawnableObjectStatsSO : ScriptableObject
{
    [Range(0.1f, 10f)] [SerializeField] private float defaultWallSpeed = 4;
    [Range(0.1f, 10f)] [SerializeField] private float defaultPowerUpSpeed = 4;

    private float wallSpeed;
    public float WallSpeed
    {
        get => wallSpeed;
        set => wallSpeed = value;
    }

    private float powerUpSpeed;
    public float PowerUpSpeed
    {
        get => powerUpSpeed;
        set => powerUpSpeed = value;
    }

    private void OnEnable()
    {
        WallSpeed = defaultWallSpeed;
        PowerUpSpeed = defaultPowerUpSpeed;
    }
}
