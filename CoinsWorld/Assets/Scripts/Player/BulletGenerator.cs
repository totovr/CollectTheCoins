using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    // public GameObject bullet;
    public static BulletGenerator sharedInstance;

    private string bulletResource = "Bullet";

    private float bulletSpeed = 5000;

    void Start()
    {
        sharedInstance = this;
    }

    public void GenerateBullet()
    {
        //Shoot, the position and the rotation is of the gunbarret
        GameObject tempBullet = Instantiate(Resources.Load(bulletResource, typeof(GameObject)), transform.position, transform.rotation) as GameObject;
        Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
        tempRigidBodyBullet.AddForce(tempRigidBodyBullet.transform.forward * bulletSpeed);
        Destroy(tempBullet, 3f);
    }

}
