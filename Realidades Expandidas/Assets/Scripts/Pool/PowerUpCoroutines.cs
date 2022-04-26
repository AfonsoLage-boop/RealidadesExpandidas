using System.Collections;
using UnityEngine;

public class PowerUpCoroutines : MonoBehaviour
{
    // Serialized Components
    [SerializeField] private PowerUpsSO powerUps;
    [SerializeField] private SpawnableObjectStatsSO stats;

    // Components
    private TextStatistics textStatistics;

    private void Awake()
    {
        textStatistics = FindObjectOfType<TextStatistics>();
    }

    public IEnumerator SlowTimeCoroutine()
    {
        stats.WallSpeed = powerUps.SlowMotionAmount;

        float enteredTime = Time.time;
        float currentTime = Time.time;

        while (currentTime < enteredTime + powerUps.SlowMotionDuration)
        {
            currentTime = Time.time;
            yield return null;
        }
        stats.WallSpeed = stats.DefaultWallSpeed;
        textStatistics.UpdateText();
    }
}
