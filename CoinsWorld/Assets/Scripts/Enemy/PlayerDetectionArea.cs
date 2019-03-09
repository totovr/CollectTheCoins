using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectionArea : MonoBehaviour
{
    SkinnedMeshRenderer enemyMesh;
    EnemyController enemyController;


    void Start()
    {
        enemyMesh = GetComponent<SkinnedMeshRenderer>();
        enemyController = gameObject.GetComponentInParent<EnemyController>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("PlayerFPS") || collider.CompareTag("PlayerFPS"))
        {
            enemyMesh.enabled = true;
            enemyController.enabled = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("PlayerFPS") || collider.CompareTag("PlayerFPS"))
        {
            enemyMesh.enabled = false;
            enemyController.enabled = false;
        }
    }

}
