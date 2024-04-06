using UnityEngine;

namespace Main.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public enum PlayerNr
        {
            Player1,
            Player2
        }

        [SerializeField] private PlayerNr playerNr;
        private Rigidbody _rigidbody;
        private const float MoveMultiplier = 15f;
        private CombatManager _combatManager;
        private PlayerStats _playerStats;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _combatManager = GetComponent<CombatManager>();
            _playerStats = GetComponent<PlayerStats>();
        }

        void Update()
        {
            PlayerInputHandler();
        }

        private void PlayerInputHandler()
        {
            switch (playerNr)
            {
                case PlayerNr.Player1:
                    if (GameManager.Instance.GetCurrentGameState() == GameManager.GameState.Player1Pending)
                    {
                        Player1InputHandler();
                    }

                    break;
                case PlayerNr.Player2:

                    if (GameManager.Instance.GetCurrentGameState() == GameManager.GameState.Player2Pending)
                    {
                        Player2InputHandler();
                    }

                    break;
            }
        }

        private void Player1InputHandler()
        {
            bool keyWasPressed = false;
            if (Input.GetKeyDown(KeyCode.A)) // move back + regen
            {
                keyWasPressed = true;
                _playerStats.RegenStamina();
                _playerStats.RegenHealth();
                _rigidbody.AddForce(-transform.forward * MoveMultiplier, ForceMode.Impulse);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                keyWasPressed = true;
                _rigidbody.AddForce(transform.forward * MoveMultiplier, ForceMode.Impulse);
            }
            else if (Input.GetKeyDown(KeyCode.Z)) // Weak attack
            {
                keyWasPressed = _combatManager.PerformWeakAttack();
            }
            else if (Input.GetKeyDown(KeyCode.X)) // Mid attack
            {
                keyWasPressed = _combatManager.PerformMediumAttack();
            }
            else if (Input.GetKeyDown(KeyCode.C)) // Strong attack
            {
                keyWasPressed = _combatManager.PerformStrongAttack();
            }

            if (keyWasPressed)
            {
                GameManager.Instance.ChangeState(GameManager.GameState.Player1ExecutingAction);
            }
        }

        private void Player2InputHandler()
        {
            bool keyWasPressed = false;
            if (Input.GetKeyDown(KeyCode.K))
            {
                keyWasPressed = true;
                _rigidbody.AddForce(transform.forward * MoveMultiplier, ForceMode.Impulse);
            }
            else if (Input.GetKeyDown(KeyCode.L)) // move back + regen
            {
                keyWasPressed = true;
                _playerStats.RegenStamina();
                _playerStats.RegenHealth();
                _rigidbody.AddForce(-transform.forward * MoveMultiplier, ForceMode.Impulse);
            }
            else if (Input.GetKeyDown(KeyCode.I)) // Weak attack
            {
                keyWasPressed = _combatManager.PerformWeakAttack();
            }
            else if (Input.GetKeyDown(KeyCode.O)) // Mid attack
            {
                keyWasPressed = _combatManager.PerformMediumAttack();
            }
            else if (Input.GetKeyDown(KeyCode.P)) // Strong attack
            {
                keyWasPressed = _combatManager.PerformStrongAttack();
            }

            if (keyWasPressed)
            {
                GameManager.Instance.ChangeState(GameManager.GameState.Player2ExecutingAction);
            }
        }

        public PlayerNr GetPlayerNr()
        {
            return playerNr;
        }
    }
}