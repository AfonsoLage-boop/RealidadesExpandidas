using UnityEngine;

public class CollisionsVFXAndSound : MonoBehaviour
{
    [SerializeField] private GameObject vfx;
    [SerializeField] private AudioSource speedIncrementAudioS;
    [SerializeField] private AudioSource badAudioS;
    private WatermelonSpawner watermelonSpawner;

    private void Awake()
    {
        watermelonSpawner = FindObjectOfType<WatermelonSpawner>();
    }

    public void ExecuteVFXAndSound()
    {
        GameObject obj = Instantiate(vfx, transform.position, Quaternion.identity);
        obj.transform.parent = transform;
        speedIncrementAudioS.Play();
    }

    public void ExecuteBadAudioSound()
    {
        watermelonSpawner.SpawnWatermelon();
        badAudioS.Play();
    }
}
