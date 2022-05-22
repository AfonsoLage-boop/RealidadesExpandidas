using UnityEngine;

public class GameOverCheck : MonoBehaviour
{
    [SerializeField] private GameplayStatisticsSO statistics;
    [SerializeField] private GameObject gameOverAnimationActivate;

    private void Update()
    {
        if (statistics.Lives <= 0)
        {
            gameOverAnimationActivate.SetActive(true);
            this.enabled = false;
        }
    }
}
