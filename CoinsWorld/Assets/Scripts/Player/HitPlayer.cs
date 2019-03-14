using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{
    private int playerLivesCounter = 0;
    private float damageTaken = 0.1f;

    private int playerLifes = 9;

    EffectSoundsManager effectSoundsManager;

    void Start()
    {
        effectSoundsManager = GetComponent<EffectSoundsManager>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("EnemyBullet") && GameManager.sharedInstance.currentGameState == GameState.inTheGame)
        {

            if (playerLivesCounter <= playerLifes)
            {
                GameObject.FindGameObjectWithTag("HealthBar").SendMessage("UpdateHealthBar", damageTaken);
                effectSoundsManager.PlayerReceivedDamaged();
            }

            playerLivesCounter += 1;

            if (playerLivesCounter > playerLifes) // the player is death
            {
                playerLivesCounter = 0;
                effectSoundsManager.PlayerKilled();
                GameManager.sharedInstance.GameOver();
            }

        }

    }
}
