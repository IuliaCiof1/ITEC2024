using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        // Check if the collider that stays within the trigger is the one you're interested in
        if (gameObject.CompareTag("Player1") && other.CompareTag("Player2")) // Replace "YourTag" with the tag of the object you want to detect
        {
            Debug.Log("Collider is staying within the trigger.");
        }
    }
}
