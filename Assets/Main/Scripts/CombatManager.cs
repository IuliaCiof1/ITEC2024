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
            _animationManager.StartAttack();
            _playerStats.DecreaseStaminaWeakSkill();
            if (_enemyPlayer != null)
            {
                float dealtDamage = (_playerStats.baseDamage + _playerStats.damageModif) * PlayerStats.SkillDamageModifier.Weak;
                PlayerStats playerStats = _enemyPlayer.GetComponent<PlayerStats>();
                playerStats.TakeDamage(dealtDamage, 0f);
            }
        }

        return attackPerformed;
    }

    public bool PerformMediumAttack()
    {
        bool attackPerformed = false;
        if (_playerStats.CurrentStamina > _playerStats.GetMidAttackCost())
        {
            attackPerformed = true;
            _animationManager.StartAttack();
            _playerStats.DecreaseStaminaMediumSkill();
            if (_enemyPlayer != null)
            {
                float dealtDamage = (_playerStats.baseDamage + _playerStats.damageModif) * PlayerStats.SkillDamageModifier.Medium;
                PlayerStats playerStats = _enemyPlayer.GetComponent<PlayerStats>();
                playerStats.TakeDamage(dealtDamage, 2f);
            }
        }

        return attackPerformed;
    }

    public bool PerformStrongAttack()
    {
        bool attackPerformed = false;
        if (_playerStats.CurrentStamina > _playerStats.GetStrongAttackCost())
        {
            attackPerformed = true;
            _animationManager.StartAttack();
            _playerStats.DecreaseStaminaStrongSkill();
            if (_enemyPlayer != null)
            {
                float dealtDamage = (_playerStats.baseDamage + _playerStats.damageModif) * PlayerStats.SkillDamageModifier.Strong;
                PlayerStats playerStats = _enemyPlayer.GetComponent<PlayerStats>();
                playerStats.TakeDamage(dealtDamage, 4f);
            }
        }

        return attackPerformed;
    }
}