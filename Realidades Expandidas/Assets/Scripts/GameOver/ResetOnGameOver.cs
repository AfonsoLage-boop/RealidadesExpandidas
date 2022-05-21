using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetOnGameOver : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
