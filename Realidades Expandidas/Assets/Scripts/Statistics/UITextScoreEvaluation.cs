using UnityEngine;
using TMPro;

public class UITextScoreEvaluation : MonoBehaviour
{
    [SerializeField] private GameplayStatisticsSO statistics;
    [SerializeField] private GameObject bad;
    [SerializeField] private TextMeshProUGUI badText;
    [SerializeField] private GameObject good;
    [SerializeField] private TextMeshProUGUI goodText;
    [SerializeField] private GameObject perfect;
    [SerializeField] private TextMeshProUGUI perfectText;

    public void Bad()
    {
        bad.SetActive(false);
        bad.SetActive(true);
        badText.text = "Bad" + "\n" + "+ 0";
    }

    public void Good()
    {
        good.SetActive(false);
        good.SetActive(true);
        goodText.text = "Good" + "\n" + "+ " + statistics.GoodScore;
    }

    public void Perfect()
    {
        perfect.SetActive(false);
        perfect.SetActive(true);
        perfectText.text = "Perfect" + "\n" + "+ " + statistics.PerfectScore;
    }
}
