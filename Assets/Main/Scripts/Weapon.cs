using UnityEngine;

namespace Main.Scripts
{
    public class Weapon : MonoBehaviour
    {
        public int LostStaminaModifier;
        public int DamageModifier;
        public int HealthModifier;

        public Weapon()
        {
        }
        public Weapon(int lostStaminaModifier, int damageModifier, int healthModifier)
        {
            LostStaminaModifier = lostStaminaModifier;
            DamageModifier = damageModifier;
            HealthModifier = healthModifier;
        }
    }

}