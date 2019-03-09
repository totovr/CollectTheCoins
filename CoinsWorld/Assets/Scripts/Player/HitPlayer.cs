using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{
    // Check if the monster hit the player
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("EnemyBullet"))
        {
            GameObject.FindGameObjectWithTag("PlayerScripts").SendMessage("PlayerReceivedDamaged");
            Debug.Log("The monster hit you");
        }

    }
}
