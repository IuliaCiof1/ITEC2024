using UnityEngine;

namespace Main.Scripts
{
    public class Hammer : Weapon
    {
        public override void Attack()
        {
            Debug.Log("Hammer attack");
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