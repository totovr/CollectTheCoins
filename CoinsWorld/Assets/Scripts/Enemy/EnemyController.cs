using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyAnimations enemyAnimator;

    //　Bullet object
    public GameObject bulletPrefab;
    public Transform muzzle;
    //　Speed of bullet
    [HideInInspector]
    public float bulletSpeed = 1200;

    // public Transform player;
    Transform player;
    [HideInInspector]
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

    EnemyTypes enemyTypeDamage;

    void Shot()
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        bulletInstance.GetComponent<Rigidbody>().AddForce(bulletInstance.transform.forward * bulletSpeed);
        Destroy(bulletInstance, 3f);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("PlayerBullet") || GameManager.sharedInstance.currentGameState == GameState.inTheGame)
        {
            // Debug.Log("Enemy shooted");
            enemyAnimator.EnemyIsDeath();
            if (disableTheShootedSound)
            {
                audioSource.PlayOneShot(deadSE);
            }
            disableTheShootedSound = false;
            notShooted = false;

            // Bonus time
            if (GameManager.sharedInstance.currentGameState != GameState.gameOver)
            {
                // UICountDown.TimerBonus = monsterValue;
                UICountDown.TimerBonus = enemyTypeDamage.EnemyValueCalculator(EnemyTypesEnum.STONE);
            }

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
        enemyAnimator = GetComponent<EnemyAnimations>();
        player = GameObject.FindWithTag("PlayerFPS").transform;
        timeBtwShots = startTimeBtwShots;

        // enemyOne = EnemyTypes.STONE;
        enemyTypeDamage = GetComponent<EnemyTypes>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyAnimator.EnemyIsPossing();
        var relativePos = player.position - transform.position;
        var rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotate_speed);

        if (notShooted)
        {
            if (timeBtwShots <= 0)
            { 
                enemyAnimator.EnemyIsAttacking();
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
