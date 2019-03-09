using UnityEngine;

public class KillPlayer : MonoBehaviour {


    public static KillPlayer sharedInstance;
    public bool touch = false;

    void Start()
    {
        sharedInstance = this;
    }

    // Check if the monster hit the player
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("EnemyOne") || collider.gameObject.CompareTag("EnemyTwo"))
        { 
            touch = true;
            Debug.Log("The monster hit you");
        }

    }

}
