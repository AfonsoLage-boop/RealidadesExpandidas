using UnityEngine;

public class PauseToGameplayCounter : MonoBehaviour
{
    private ObjectSpawner spawner;
    private MousePauseGame mousePauseGame;

    private void Awake()
    {
        spawner = FindObjectOfType<ObjectSpawner>();
        mousePauseGame = FindObjectOfType<MousePauseGame>();
    }

    public void UnpauseAnimationEvent()
    {
        mousePauseGame.MousePosition = Input.mousePosition;
        spawner.PauseSpawn(false);
    }
}
