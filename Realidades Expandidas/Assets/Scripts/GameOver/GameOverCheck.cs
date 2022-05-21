using UnityEngine;

public class GameOverCheck : MonoBehaviour
{
    [SerializeField] private GameplayStatisticsSO statistics;
    [SerializeField] private GameObject gameOverAnimationActivate;

    private void OnEnable()
    {
        statistics.GameOver += () => gameOverAnimationActivate.SetActive(true);      
    }

    private void OnDisable()
    {
        statistics.GameOver -= () => gameOverAnimationActivate.SetActive(true);
    }
}
