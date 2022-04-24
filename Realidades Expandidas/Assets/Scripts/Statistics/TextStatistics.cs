using UnityEngine;
using TMPro;

/// <summary>
/// Class responsible for updating text with statistics.
/// </summary>
public class TextStatistics : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] private GameplayStatisticsSO statistics;
    [SerializeField] private SpawnableObjectStatsSO stats;

    [Header("Text components")]
    [SerializeField] private TextMeshProUGUI good;
    [SerializeField] private TextMeshProUGUI bad;
    [SerializeField] private TextMeshProUGUI speed;

    private void OnEnable() => UpdateText();

    public void UpdateText()
    {
        good.text = "Good: " + statistics.AttemptsSucceeded;
        bad.text = "Bad: " + statistics.AttemptsFailed;
        speed.text = "Speed: " + stats.WallSpeed;
    }
}
