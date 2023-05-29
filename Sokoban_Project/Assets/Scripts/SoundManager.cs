using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip playerFinish, playerMove, ballMove, restart;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        playerFinish = Resources.Load<AudioClip>("playerFinish");
        playerMove = Resources.Load<AudioClip>("playerMove");
        ballMove = Resources.Load<AudioClip>("ballMove");
        restart = Resources.Load<AudioClip>("restart");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "finish":
                audioSrc.volume = 0.1f;
                audioSrc.PlayOneShot(playerFinish);
                audioSrc.volume = 1;
                break;
            case "playermove":
                audioSrc.PlayOneShot(playerMove);
                break;
            case "ballmove":
                audioSrc.PlayOneShot(ballMove);
                break;
            case "restart":
                audioSrc.PlayOneShot(restart);
                break;
        }
    }
}