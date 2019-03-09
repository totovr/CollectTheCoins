using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //　Bullet object
    public GameObject bulletPrefab;
    public Transform muzzle;
    //　Speed of bullet
    public float bulletPower = 2000f;
    //  Animation
    private Animator animator;

    public Transform player;
    public float rotate_speed = 0.5f;
    private float timeBtwShots;
    public float startTimeBtwShots;

    //  Shooting sound
    private AudioSource audioSource;
    public AudioClip shootSE;
    public AudioClip deadSE;

    void Shot()
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        bulletInstance.GetComponent<Rigidbody>().AddForce(bulletInstance.transform.forward * bulletPower);
        Destroy(bulletInstance, 3f);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player_bullet")
        {
            animator.SetBool("Dead_flag", true);
            audioSource.PlayOneShot(deadSE);
            Invoke("destroyEnemy", 1.0f);
        }
    }

    void destroyEnemy()
    {
        Destroy(gameObject);
    }

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("PlayerFPS").transform;
        timeBtwShots = startTimeBtwShots;

    }

    // Update is called once per frame
    void Update()
    {
        // animator.SetBool("Idle_flag", true);
        var relativePos = player.position - transform.position;
        var rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotate_speed);

        if (timeBtwShots <= 0)
        {
            animator.SetBool("Attack_flag", true);
            audioSource.PlayOneShot(shootSE);
            Shot();
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
