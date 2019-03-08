using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Attack_byEnemey : MonoBehaviour {
    // hitting sound
    private AudioSource audioSource;
    public AudioClip hitSE;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            audioSource.PlayOneShot(hitSE);
            // GameObject.Find("PlayerPGMActuate").GetComponent<Player_PGM_Actuate>().AddPlayerHitPoint();
            // GameObject.Find("PlayerPGMActuate").GetComponent<Player_PGM_Actuate2>().AddPlayerHitPoint();
            //Destroy(col.gameObject);
        }
        else if (col.gameObject.tag == "Barrel")
        {
            Destroy(gameObject);
        }
    }
}
