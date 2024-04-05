using System;
using UnityEngine;

namespace Main.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        // Start is called before the first frame update

        public enum GameState
        {
            StartMenu,
            PlayStarted,
            Player1Pending,
            Player1ExecutingAction,
            Player2Pending,
            Player2ExecutingAction,
            RoundEnded,
            GameFinished
        }

        private GameState _currentGameState;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            _currentGameState = GameState.StartMenu;
        }

        // Update is called once per frame
        void Update()
        {
            switch (_currentGameState)
            {
                case GameState.StartMenu:
                    break;
                case GameState.PlayStarted:
                    break;
                case GameState.Player1Pending:
                    break;
                case GameState.Player1ExecutingAction:
                    break;
                case GameState.Player2Pending:
                    break;
                case GameState.Player2ExecutingAction:
                    break;
                case GameState.RoundEnded:
                    break;
                case GameState.GameFinished:
                    break;
            }
        }

        public void ChangeState(GameState newState)
        {
            _currentGameState = newState;
        }
    }
}