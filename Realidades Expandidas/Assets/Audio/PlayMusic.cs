using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    private AudioSource audioS;
    private System.Random rand;
    private int randomNum;

    public static PlayMusic Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);

        audioS = GetComponent<AudioSource>();
        rand = new System.Random();
        clips.Shuffle(rand);

        randomNum = rand.Next(clips.Length);
        audioS.clip = clips[randomNum];
        audioS.Play();
    }

    private void Update()
    {
        if(audioS.isPlaying == false)
        {
            int otherRandomNum;
            do
            {
                otherRandomNum = rand.Next(clips.Length);
            }
            while (otherRandomNum == randomNum);
            randomNum = otherRandomNum;
            audioS.clip = clips[randomNum];
            audioS.Play();
        }
    }
}
