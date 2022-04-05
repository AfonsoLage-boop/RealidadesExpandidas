using System.Collections;
using UnityEngine;

public class PowerUpCoroutines : MonoBehaviour
{
    [SerializeField] private PowerUpsSO powerUps;
    [SerializeField] private SpawnableObjectStatsSO stats;

    public IEnumerator SlowTimeCoroutine()
    {
        stats.WallSpeed = powerUps.SlowMotionAmount;

        float enteredTime = Time.time;
        float currentTime = Time.time;

        while (currentTime < enteredTime + powerUps.SlowMotionDuration)
        {
            Debug.Log("TEMP");
            currentTime = Time.time;
            yield return null;
        }
        stats.WallSpeed = stats.DefaultWallSpeed;
    }
}
