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
        private const float MoveMultiplier = 8f;
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
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
            if (Input.GetKeyDown(KeyCode.A))
            {
                keyWasPressed = true;
                Debug.Log("Key A was pressed for player 1");
                _rigidbody.AddForce(transform.forward * MoveMultiplier, ForceMode.Impulse);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                keyWasPressed = true;
                Debug.Log("Key S was pressed for player 1");
                _rigidbody.AddForce(-transform.forward * MoveMultiplier, ForceMode.Impulse);
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
                Debug.Log("Key K was pressed for player 2");
                _rigidbody.AddForce(transform.forward * MoveMultiplier, ForceMode.Impulse);
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                keyWasPressed = true;
                Debug.Log("Key L was pressed for player 2");
                _rigidbody.AddForce(-transform.forward * MoveMultiplier, ForceMode.Impulse);
            }

            if (keyWasPressed)
            {
                GameManager.Instance.ChangeState(GameManager.GameState.Player2ExecutingAction);
            }
        }
    }
}