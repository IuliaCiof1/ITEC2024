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

        void Start()
        {
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
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("Key A was pressed for player 1");
            }
        }

        private void Player2InputHandler()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("Key A was pressed for player 2");
            }
        }
    }
}