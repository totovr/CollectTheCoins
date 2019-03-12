using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNotStaticController : MonoBehaviour
{
    //　Bullet object
    public GameObject bulletPrefab;
    public Transform muzzle;
    //　Speed of bullet
    public float bulletPower = 2000f;
    //  Animation
    private EnemyNotStaticAnimations enemyAnimatorPositions;

    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    Transform player;
    public float rotate_speed = 0.5f;
    private float timeBtwShots;
    public float startTimeBtwShots;

    //  Shooting sound
    private AudioSource audioSource;
    public AudioClip shootSE;
    public AudioClip deadSE;

    // If the enemy is shoot the enemy will not be able to shoot
    private bool notShooted = true;
    private bool disableTheShootedSound = true;

    void Shot()
    {
        var bulletInstance = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation) as GameObject;
        bulletInstance.GetComponent<Rigidbody>().AddForce(bulletInstance.transform.forward * bulletPower);
        Destroy(bulletInstance, 3f);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("PlayerBullet") || GameManager.sharedInstance.currentGameState == GameState.inTheGame)
        {
            Debug.Log("Enemy shooted");
            enemyAnimatorPositions.EnemyIsDeath();
            if (disableTheShootedSound)
            {
                audioSource.PlayOneShot(deadSE);
            }
            disableTheShootedSound = false;
            notShooted = false;
            // Destroy the enemy after n time
            Invoke("destroyEnemy", 1.5f);
        }
    }

    void destroyEnemy()
    {
        Destroy(gameObject);
    }

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        enemyAnimatorPositions = GetComponent<EnemyNotStaticAnimations>();
        player = GameObject.FindWithTag("PlayerFPS").transform;
        timeBtwShots = startTimeBtwShots;

    }

    // Update is called once per frame
    void Update()
    {
        enemyAnimatorPositions.EnemyIsMoving();
        var relativePos = player.position - transform.position;
        var rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotate_speed);

        if (notShooted)
        {
            if (Vector3.Distance(transform.position, player.position) > stoppingDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else if (Vector3.Distance(transform.position, player.position) < stoppingDistance && Vector3.Distance(transform.position, player.position) > retreatDistance)
            {
                transform.position = transform.position;
            }
            else if (Vector3.Distance(transform.position, player.position) < retreatDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            }

            if (timeBtwShots <= 0)
            {
                enemyAnimatorPositions.EnemyIsAttacking();
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
}
