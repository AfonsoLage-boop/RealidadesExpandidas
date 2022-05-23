using UnityEngine;

public class SpeedIncrementVFXAndSound : MonoBehaviour
{
    [SerializeField] private GameObject vfx;
    private AudioSource audioS;

    private void Awake()
    {
        audioS = GetComponent<AudioSource>();
    }

    public void ExecuteVFXAndSound()
    {
        Instantiate(vfx, transform.position, Quaternion.identity);
        audioS.Play();
    }
}
