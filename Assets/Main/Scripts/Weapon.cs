using UnityEngine;

namespace Main.Scripts
{
    public class Weapon : MonoBehaviour
    {
        public float LostStaminaModifier;
        public float DamageModifier;
        public float HealthModifier;

        public Weapon()
        {
        }
        public Weapon(float lostStaminaModifier, float damageModifier, float healthModifier)
        {
            LostStaminaModifier = lostStaminaModifier;
            DamageModifier = damageModifier;
            HealthModifier = healthModifier;
        }
    }

}