using System.Collections;
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
        private static readonly int WeakAttack = Animator.StringToHash("WeakAttack");
        private float _timeToWaitBeforeResettingAttackBools = 0.8f;
        private static readonly int MidAttack = Animator.StringToHash("MidAttack");
        private static readonly int StrongAttack = Animator.StringToHash("StrongAttack");
        private static readonly int Attacking = Animator.StringToHash("Attacking");
        private static readonly int PlayDeath = Animator.StringToHash("PlayDeath");

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

        IEnumerator ResetValuesAfterDelay()
        {
            yield return new WaitForSeconds(_timeToWaitBeforeResettingAttackBools);
            animator.SetBool(Attacking, false);
        }

        private void CheckMovementDirection()
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

        public void StartAttack()
        {
            animator.SetBool(Attacking, true);
            StartCoroutine(ResetValuesAfterDelay());
        }

        public void StartDeathAnimation()
        {
            animator.SetTrigger(PlayDeath);
        }
    }
}