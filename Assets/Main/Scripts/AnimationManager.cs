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
                        if (_rigidbody.velocity.x < -0.2f)
                        {
                            animator.SetBool(MovingForward, true);
                            animator.SetBool(MovingBackward, false);
                        }
                        else if (_rigidbody.velocity.x > 0.2f)
                        {
                            animator.SetBool(MovingForward, false);
                            animator.SetBool(MovingBackward, true);
                        }
                        else
                        {
                            animator.SetBool(MovingForward, false);
                            animator.SetBool(MovingBackward, false);
                        }
                        break;
                    case PlayerController.PlayerNr.Player2:
                        if (_rigidbody.velocity.x > 0.2f)
                        {
                            animator.SetBool(MovingForward, true);
                            animator.SetBool(MovingBackward, false);
                        }
                        else if (_rigidbody.velocity.x < -0.2f)
                        {
                            animator.SetBool(MovingForward, false);
                            animator.SetBool(MovingBackward, true);
                        }
                        else
                        {
                            animator.SetBool(MovingForward, false);
                            animator.SetBool(MovingBackward, false);
                        }
                        break;
                }
            }
        }
    }
}
