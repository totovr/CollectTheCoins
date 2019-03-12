using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("PlayerBullet") || GameManager.sharedInstance.currentGameState == GameState.inTheGame)
        {
            Debug.Log("The enemy was shooted");
            Destroy(gameObject);
        }
    }
}
