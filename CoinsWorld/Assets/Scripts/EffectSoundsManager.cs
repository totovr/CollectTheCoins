using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSoundsManager : MonoBehaviour
{
    public List<AudioClip> audioToPlay = new List<AudioClip>();

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Coin"))
        {
            audioSource.clip = audioToPlay[0];
            audioSource.Play();
        }
    }
}
