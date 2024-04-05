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
                    Time.timeScale = 0f;
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        ChangeState(GameState.PlayStarted);
                    }

                    break;
                case GameState.PlayStarted:
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        ChangeState(GameState.Player1Pending);
                    }

                    Time.timeScale = 1f;
                    break;
                case GameState.Player1Pending:
                    Time.timeScale = 0f;
                    break;
                case GameState.Player1ExecutingAction:
                    Time.timeScale = 1f;
                    ChangeState(GameState.Player2Pending);
                    break;
                case GameState.Player2Pending:
                    Time.timeScale = 0f;
                    break;
                case GameState.Player2ExecutingAction:
                    Time.timeScale = 1f;
                    ChangeState(GameState.Player1Pending);
                    break;
                case GameState.RoundEnded:
                    Time.timeScale = 0f;
                    break;
                case GameState.GameFinished:
                    Time.timeScale = 0f;
                    break;
            }
        }

        public void ChangeState(GameState newState)
        {
            string currentGameStateString = Enum.GetName(typeof(GameState), _currentGameState);
            string newStateString = Enum.GetName(typeof(GameState), newState);
            Debug.Log("State changed: " + currentGameStateString + " -> " + newStateString);
            _currentGameState = newState;
        }

        public GameState GetCurrentGameState()
        {
            return _currentGameState;
        }
    }
}