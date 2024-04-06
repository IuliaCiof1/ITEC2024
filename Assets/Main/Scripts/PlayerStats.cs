using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }
    private int healthModif = 0;

    private int baseDamage = 20;
    private int damageModif = 0;
    public int damage = 0;

    public int maxStamina = 100;
    public int currentStamina { get; private set; }
    private int lostStamina = 30;
    private int lostStaminaModif = 0;

    public AudioSource deathSource;
    public AudioClip clip;

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
        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage. ");

        if (currentHealth <= 0)
        {
            //sound
            Die();

        }
    }

    public void decreaseStamina()
    {
        currentStamina -= lostStamina;
        Debug.Log(transform.name + " Current stamina " + currentStamina);
    }

    public void Die()
    {
        deathSource.PlayOneShot(clip);
        Debug.Log(transform.name + "died.");
    }

    public void setCurrentHealth()
    {
        currentHealth = maxHealth + healthModif;
    }

    public void setDamage()
    {
        damage = baseDamage + damageModif;
    }

    public void setLostStamina()
    {
        lostStamina += lostStamina + lostStaminaModif;
    }

    public void regenHealth() {

        currentHealth += currentHealth % 10;
    }

    public void regenStamina()
    {

        currentStamina += currentStamina % 30;
    }
}
