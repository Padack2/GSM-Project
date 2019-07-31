using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] bubble = new AudioClip[3];
    public AudioClip[] explosion = new AudioClip[7];
    public AudioClip[] bullet = new AudioClip[6];
    public AudioClip Btn;
    public AudioClip MainBtn;
    public AudioClip PitchBtn;

    AudioSource myAudio;
    public static SoundManager instance;

    int num;

    void Awake()
    {
        if (SoundManager.instance == null)
            SoundManager.instance = this;

        myAudio = GetComponent<AudioSource>();
    }

    public void RandomSound(AudioClip[] audioclip)
    {
        int length = audioclip.Length;
        int ran = Random.Range(0, length);

        num = ran;
    }
    
    public void PlayBubble()
    {
        RandomSound(bubble);
        myAudio.PlayOneShot(bubble[num]);
    }

    public void PlayExplosion()
    {
        RandomSound(explosion);
        myAudio.PlayOneShot(explosion[num]);
    }

    public void PlayBullet()
    {
        RandomSound(bullet);
        myAudio.PlayOneShot(bullet[num]);
    }

    public void PlayBtn()
    {
        myAudio.PlayOneShot(Btn);
    }

    public void PlayMainBtn()
    {
        myAudio.PlayOneShot(MainBtn);
    }

    public void PlayPitchBtn()
    {
        myAudio.PlayOneShot(PitchBtn);
    }
}
