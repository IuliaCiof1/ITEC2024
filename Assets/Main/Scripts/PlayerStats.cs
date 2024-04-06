using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public float CurrentHealth { get; private set; }
    private int healthModif = 0;

    private int baseDamage = 20;
    private int damageModif = 0;
    public int damage = 0;

    public int maxStamina = 100;
    public float CurrentStamina { get; private set; }
    private int _lostStamina = 30;
    private int lostStaminaModif = 0;

    public void InitializePlayerStats()
    {
        CurrentStamina = maxStamina;
        CurrentHealth = maxHealth;
    }

    private struct SkillCostModifier
    {
        public const float Weak = 0.7f;
        public const float Medium = 1f;
        public const float Strong = 1.3f;
    }

    public AudioSource deathSource;
    public AudioClip clip;

    // public List<Weapon> Weapons = new List<Weapon>();

    private void Start()
    {
        setCurrentHealth();
        setDamage();
    }

    private void Awake()
    {
        setCurrentHealth();
        setDamage();
        setLostStamina();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(30);
        }
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage. ");

        if (CurrentHealth <= 0)
        {
            //sound
            Die();
        }
    }

    public void decreaseStamina()
    {
        CurrentStamina -= _lostStamina;
        Debug.Log(transform.name + " Current stamina " + CurrentStamina);
    }

    public void Die()
    {
        deathSource.PlayOneShot(clip);
        Debug.Log(transform.name + "died.");
    }

    public void setCurrentHealth()
    {
        CurrentHealth = maxHealth + healthModif;
    }

    public void setDamage()
    {
        damage = baseDamage + damageModif;
    }

    public void setLostStamina()
    {
        _lostStamina += _lostStamina + lostStaminaModif;
    }

    public void regenHealth()
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + maxHealth % 10, 0f, maxHealth);
    }

    public void regenStamina()
    {
        CurrentStamina = Mathf.Clamp(CurrentStamina + maxStamina % 30, 0f, maxStamina);
    }


    public float GetWeakAttackCost()
    {
        return _lostStamina * SkillCostModifier.Weak + lostStaminaModif;
    }


    public float GetMidAttackCost()
    {
        return _lostStamina * SkillCostModifier.Medium + lostStaminaModif;
    }


    public float GetStrongAttackCost()
    {
        return _lostStamina * SkillCostModifier.Strong + lostStaminaModif;
    }

    public void DecreaseStaminaWeakSkill()
    {
        CurrentStamina -= _lostStamina * SkillCostModifier.Strong + lostStaminaModif;
    }

    public void TakeDamageWeak()
    {
        CurrentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage. ");

        if (CurrentHealth <= 0)
        {
            //sound
            Die();
        }
    }

    public void InitializeStats()
    {
        
    }
}