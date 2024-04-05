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
            Vector3 middlePoint = (player1.position + player2.position) / 2f;
            transform.position = middlePoint;
        }

        public float GetDistanceBetweenPlayers()
        {
            return Vector3.Distance(player1.position, player2.position);
        }
    }
}