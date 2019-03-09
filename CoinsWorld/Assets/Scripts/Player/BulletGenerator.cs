using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{

    float bulletSpeed = 1100;
    public GameObject bullet;
    public static BulletGenerator sharedInstance;

    void Start()
    {
        sharedInstance = this;
    }

    public void GenerateBullet()
    {
        //Shoot, the position and the rotation is of the gunbarret
        GameObject tempBullet = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
        tempRigidBodyBullet.AddForce(tempRigidBodyBullet.transform.forward * bulletSpeed);
        Destroy(tempBullet, 0.3f);
    }

}
