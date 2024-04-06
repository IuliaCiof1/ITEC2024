using UnityEngine;

namespace Main.Scripts
{
    public class AnimationManager : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private Rigidbody _rigidbody;
        private static readonly int Hitting = Animator.StringToHash("Hitting");
        private static readonly int MovingForward = Animator.StringToHash("MovingForward");
        private static readonly int MovingBackward = Animator.StringToHash("MovingBackward");
        private PlayerController _playerController;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>(); // Assign the Rigidbody component
            _playerController = GetComponent<PlayerController>();
            // animator.Play("Punch");
        }

        void FixedUpdate()
        {
            CheckMovementDirection();
        }

        void CheckMovementDirection()
        {
            if (_rigidbody != null)
            {
                switch (_playerController.GetPlayerNr())
                {
                    case PlayerController.PlayerNr.Player1:
                        Debug.Log("Velocity " + _rigidbody.velocity);
                        if (_rigidbody.velocity.x > 0f)
                        {
                            Debug.Log("Player is moving FORWARD.");
                            animator.SetBool(MovingForward, true);
                            animator.SetBool(MovingBackward, false);
                        }
                        else if (_rigidbody.velocity.x < 0f)
                        {
                            Debug.Log("Player is moving BACKWARD.");
                            animator.SetBool(MovingForward, false);
                            animator.SetBool(MovingBackward, true);
                        }
                        else
                        {
                            Debug.Log("Player is NOT MOVING.");
                            animator.SetBool(MovingForward, false);
                            animator.SetBool(MovingBackward, false);
                        }
                        break;
                }
            }
        }
    }
}
