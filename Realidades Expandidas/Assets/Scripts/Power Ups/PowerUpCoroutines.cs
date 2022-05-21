using System.Collections;
using UnityEngine;

public class PowerUpCoroutines : MonoBehaviour
{
    // Serialized Components
    [SerializeField] private PowerUpsSO powerUps;
    [SerializeField] private SpawnableObjectStatsSO stats;
    private ObjectSpawner spawner;

    // Components
    private TextStatistics textStatistics;

    private void Awake()
    {
        spawner = FindObjectOfType<ObjectSpawner>();
        textStatistics = FindObjectOfType<TextStatistics>();
    }

    public IEnumerator SlowTimeCoroutine()
    {
        stats.WallSpeed = powerUps.SlowMotionAmount;

        float currentTime = 0;

        while (currentTime < powerUps.SlowMotionDuration)
        {
            if (spawner.IsPaused == false)
                currentTime += Time.deltaTime;
            yield return null;
        }
        stats.WallSpeed = stats.DefaultWallSpeed;
        textStatistics.UpdateText();
    }
}
