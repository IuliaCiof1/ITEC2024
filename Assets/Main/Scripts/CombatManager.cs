using System;
using System.Collections;
using System.Collections.Generic;
using Main.Scripts;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    private bool _playerTurn = false;
    private PlayerStats _playerStats;
    private GameObject _enemyPlayer;
    private AnimationManager _animationManager;


    void Start()
    {
        _playerStats = GetComponent<PlayerStats>();
        _animationManager = GetComponent<AnimationManager>();
        _enemyPlayer = null;
    }

    void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if (gameObject.CompareTag("Player1") && other.CompareTag("Player2"))
        {
            // Debug.Log("Collider is staying within the trigger.");
            _enemyPlayer = other.gameObject;
        }
        else if (gameObject.CompareTag("Player2") && other.CompareTag("Player1"))
        {
            // Debug.Log("Collider is staying within the trigger.");
            _enemyPlayer = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.CompareTag("Player1") && other.CompareTag("Player2"))
        {
            // Debug.Log("Collider is staying within the trigger.");
            _enemyPlayer = null;
        }
        else if (gameObject.CompareTag("Player2") && other.CompareTag("Player1"))
        {
            // Debug.Log("Collider is staying within the trigger.");
            _enemyPlayer = null;
        }
    }

    public void SetPlayerTurn(bool playerTurn)
    {
        _playerTurn = playerTurn;
    }

    public bool PerformWeakAttack()
    {
        bool attackPerformed = false;
        if (_playerStats.CurrentStamina > _playerStats.GetWeakAttackCost())
        {
            attackPerformed = true;
            _animationManager.StartWeakAttack();
            _playerStats.DecreaseStaminaWeakSkill();
            if (_enemyPlayer != null)
            {
                PlayerStats playerStats = _enemyPlayer.GetComponent<PlayerStats>();
                playerStats.TakeDamageWeak();
            }
        }
        else
        {
            Debug.Log("Not enough stamina: currentStamina =  " + _playerStats.CurrentStamina + "Weak attack cost =" + _playerStats.GetWeakAttackCost());
        }

        return attackPerformed;
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