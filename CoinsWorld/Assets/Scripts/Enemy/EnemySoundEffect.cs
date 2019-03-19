using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundEffect : MonoBehaviour
{
    public List<AudioClip> ShootAudio = new List<AudioClip>();
    public List<AudioClip> KilledAudio = new List<AudioClip>();

    private AudioSource effectsAudioSource;

    int randomNumber;

    // Start is called before the first frame update
    void Start()
    {
        effectsAudioSource = gameObject.AddComponent<AudioSource>();
    }

    public void EnemyShooted()
    {
        effectsAudioSource.clip = ShootAudio[0];
        effectsAudioSource.Play();
    }

    public void EnemyKilled()
    {
        int rand = Random.Range(0, 4);
        effectsAudioSource.clip = KilledAudio[rand];
        effectsAudioSource.Play();
    }
}
