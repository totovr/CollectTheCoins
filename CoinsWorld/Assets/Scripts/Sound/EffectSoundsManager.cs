﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSoundsManager : MonoBehaviour
{
    public List<AudioClip> audioToPlay = new List<AudioClip>();

    private AudioSource effectsAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        effectsAudioSource = gameObject.AddComponent<AudioSource>();
    }

    void CoinCollectedSound()
    {
        effectsAudioSource.clip = audioToPlay[0];
        effectsAudioSource.Play();
    }

    public void ShootTheGunSound()
    {
        effectsAudioSource.clip = audioToPlay[1];
        effectsAudioSource.Play();
    }

    public void PlayerReceivedDamaged()
    {
        int rand = Random.Range(2, 5);
        effectsAudioSource.clip = audioToPlay[rand];
        effectsAudioSource.Play();
    }

    public void PlayerKilled()
    {
        int rand = Random.Range(6, 9);
        effectsAudioSource.clip = audioToPlay[rand];
        effectsAudioSource.Play();
    }
}
