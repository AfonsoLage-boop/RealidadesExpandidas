using UnityEngine;

/// <summary>
/// Scriptable object with power ups stats.
/// </summary>
[CreateAssetMenu(fileName = "Power Ups Stats", menuName = "Power Ups")]
public class PowerUpsSO : ScriptableObject
{
    [Header("Slow Motion")]
    [Range(0.1f, 10f)] [SerializeField] private float slowMotionDuration = 4;
    [Range(0.1f, 10f)] [SerializeField] private float slowMotionAmount = 1;

    public float SlowMotionDuration => slowMotionDuration;
    public float SlowMotionAmount => slowMotionAmount;
}
