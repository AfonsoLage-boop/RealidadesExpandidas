using UnityEngine;

public class ResetStatistics : MonoBehaviour
{
    [SerializeField] private GameplayStatisticsSO statistics;

    public void Start()
    {
        statistics.ResetStatistics();
    }
}
