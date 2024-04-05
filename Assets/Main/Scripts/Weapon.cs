using UnityEngine;

namespace Main.Scripts
{
    public class Weapon : MonoBehaviour
    {

        public virtual void Attack()
        {
            Debug.Log("Weapon attack");
        }

        protected GameObject WeaponOwner;
        protected float Damage;
        protected float DamageModifier;
        protected float CritChance;
        protected float CritDamageMultiplier;

        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void SetOwner(GameObject owner)
        {
            WeaponOwner = owner;
        }
    }
}