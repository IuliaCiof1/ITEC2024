using UnityEngine;

namespace Main.Scripts
{
    public class Sword : Weapon
    {
        public override void Attack()
        {
            Debug.Log("Sword attack");
        }

        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}