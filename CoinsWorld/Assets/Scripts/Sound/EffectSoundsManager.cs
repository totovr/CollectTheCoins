using System.Collections;
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

    void PlayerReceivedDamaged()
    {
        int rand = Random.Range(2, 5);
        effectsAudioSource.clip = audioToPlay[rand];
        effectsAudioSource.Play();
    }

    void CoinCollectedSound()
    {
        effectsAudioSource.clip = audioToPlay[0];
        effectsAudioSource.Play();
    }
}
