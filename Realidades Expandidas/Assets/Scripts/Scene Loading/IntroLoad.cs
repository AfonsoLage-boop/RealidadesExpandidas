using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroLoad : MonoBehaviour
{
    public void LoadNextScene()
    {
        StartCoroutine(LoadNextSceneCoroutine());
    }

    private IEnumerator LoadNextSceneCoroutine()
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        while(loading.progress < 0.95f)
        {
            yield return null;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
