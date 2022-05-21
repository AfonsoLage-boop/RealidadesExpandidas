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
    [SerializeField] private TextMeshProUGUI lives;

    private void OnEnable() => UpdateText();

    public void UpdateText()
    {
        good.text = statistics.AttemptsSucceeded.ToString();
        bad.text = "Bad: " + statistics.AttemptsFailed;
        speed.text = "Speed: " + stats.WallSpeed;
        lives.text = statistics.Lives.ToString();
    }
}
