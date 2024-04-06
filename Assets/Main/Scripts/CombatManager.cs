using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    private bool _playerTurn = false;

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
        if (gameObject.CompareTag("Player1") &&
            other.CompareTag("Player2")) // Replace "YourTag" with the tag of the object you want to detect
        {
            Debug.Log("Collider is staying within the trigger.");
        }
    }

    public void SetPlayerTurn(bool playerTurn)
    {
        _playerTurn = playerTurn;
    }

    public bool PerformWeakAttack()
    {
        return false;
    }

    public bool PerformMediumAttack()
    {
        return false;
    }

    public bool PerformStrongAttack()
    {
        return false;
    }
}