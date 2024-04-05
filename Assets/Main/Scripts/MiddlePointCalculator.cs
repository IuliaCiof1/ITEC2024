using UnityEngine;

namespace Main.Scripts
{
    public class MiddlePointCalculator : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private Transform player1;
        [SerializeField] private Transform player2;

        void Start()
        {
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            // Calculate the middle point between object1 and object2
            Vector3 middlePoint = (player1.position + player2.position) / 2f;

            // Output the middle point position
            // Debug.Log("Middle point: " + middlePoint);
            transform.position = middlePoint;
        }

        public float GetDistanceBetweenPlayers()
        {
            return Vector3.Distance(player1.position, player2.position);
        }
    }
}