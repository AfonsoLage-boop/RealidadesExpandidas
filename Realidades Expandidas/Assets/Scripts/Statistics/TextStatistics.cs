using System.Collections;
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

    private uint score;
    private YieldInstruction wffu;

    private void Awake()
    {
        wffu = new WaitForFixedUpdate();
        score = 0;
    }

    private void OnEnable() => UpdateText();

    public void UpdateText()
    {
        bad.text = "Bad: " + statistics.AttemptsFailed;
        speed.text = "Speed: " + stats.WallSpeed;
        lives.text = statistics.Lives.ToString();

        if (statistics.AttemptsSucceeded > 0 || statistics.AttemptsFailed > 0)
            StartCoroutine(UpdateScore());
        else
            good.text = "0";
    }

    private IEnumerator UpdateScore()
    {
        while (score < statistics.Score)
        {
            score += 1;
            good.text = score.ToString();
            yield return wffu;
        }
    }
}
