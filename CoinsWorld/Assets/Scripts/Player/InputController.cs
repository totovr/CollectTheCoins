using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    public float fireRate = 0.3f;
    float nextFire = 0.0F;

    private EffectSoundsManager shootSound;

    void Start()
    {
        shootSound = GetComponent<EffectSoundsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // change this once we use VR
        // OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger) && Time.time > nextFire
        if ((Input.GetKey(KeyCode.Mouse0)) && Time.time > nextFire && GameManager.sharedInstance.currentGameState == GameState.inTheGame)
        {
            //Debug.Log("Shoot");
            nextFire = Time.time + fireRate;
            shootSound.ShootTheGunSound();
            BulletGenerator.sharedInstance.GenerateBullet();
        }

    }

}
