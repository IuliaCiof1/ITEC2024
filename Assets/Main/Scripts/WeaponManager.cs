using Main.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private int oldIndex = -1;
    private PlayerStats stats;
    private int damageModif;
    private int healthModif;
    private int staminaModif;

    private Weapon weapon;

    private void Start()
    {
        stats = gameObject.GetComponentInParent<PlayerStats>();
        newWeapon(0);
        if (stats != null)
        {
            Debug.Log("PlayerStats component found.");
        }
        else
        {
            Debug.LogWarning("PlayerStats component not found.");
        }
    }

    private void newWeapon(int index)
    {
        if (oldIndex != -1)
            transform.GetChild(oldIndex).gameObject.SetActive(false);

        GameObject childObject = transform.GetChild(index).gameObject;
        childObject.SetActive(true);
        oldIndex = index;

        // Attempt to get the Weapon component from the child GameObject
        weapon = childObject.GetComponent<Weapon>();

        if (weapon != null)
        {
            // Weapon component found, get modifiers
            damageModif = weapon.DamageModifier;
            healthModif = weapon.HealthModifier;
            staminaModif = weapon.LostStaminaModifier;
            stats.setModifs(damageModif, healthModif, staminaModif);
            Debug.Log("Weapon component found. Damage Modifier: " + damageModif + ", Health Modifier: " + healthModif + ", Stamina Modifier: " + staminaModif);
        }
        else
        {
            // Weapon component not found, log a warning
            Debug.LogWarning("Weapon component not found on child GameObject.");
        }
    }

}
