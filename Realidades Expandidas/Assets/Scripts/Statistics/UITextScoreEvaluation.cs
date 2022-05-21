using UnityEngine;

public class UITextScoreEvaluation : MonoBehaviour
{
    [SerializeField] private GameObject bad;
    [SerializeField] private GameObject good;
    [SerializeField] private GameObject perfect;

    public void Bad()
    {
        bad.SetActive(false);
        bad.SetActive(true);
    }

    public void Good()
    {
        good.SetActive(false);
        good.SetActive(true);
    }

    public void Perfect()
    {
        perfect.SetActive(false);
        perfect.SetActive(true);
    }
}
