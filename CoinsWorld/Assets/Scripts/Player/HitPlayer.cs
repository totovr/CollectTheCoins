using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{
    private int playerLivesCounter = 0;
    private float damageTaken = 0.1f;

    EffectSoundsManager effectSoundsManager;

    void Start()
    {
        effectSoundsManager = GetComponent<EffectSoundsManager>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("EnemyBullet") && GameManager.sharedInstance.currentGameState == GameState.inTheGame)
        {

            if (playerLivesCounter <= 9)
            {
                GameObject.FindGameObjectWithTag("HealthBar").SendMessage("UpdateHealthBar", damageTaken);
                effectSoundsManager.PlayerReceivedDamaged();
            }

            playerLivesCounter += 1;

            if (playerLivesCounter > 9) // the player is death
            {
                playerLivesCounter = 0;
                effectSoundsManager.PlayerKilled();
                GameManager.sharedInstance.GameOver();
            }

        }

    }
}
