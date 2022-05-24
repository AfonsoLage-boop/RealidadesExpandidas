using UnityEngine;

public class RandomPitch : MonoBehaviour
{
    private AudioSource audioS;

    private void Awake()
    {
        audioS = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        audioS.pitch = Random.Range(0.8f, 1.1f);
    }
}
