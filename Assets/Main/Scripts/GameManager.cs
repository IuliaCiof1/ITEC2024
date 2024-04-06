using System;
using System.Collections;
using UnityEngine;

namespace Main.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        [SerializeField] private GameObject startMenu;
        [SerializeField] private GameObject player1Canvas;
        [SerializeField] private GameObject player2Canvas;
        [SerializeField] private GameObject player1;
        [SerializeField] private GameObject player2;
        [SerializeField] private GameObject playerStatsCanvas;
        private PlayerStats _player1Stats;
        private PlayerStats _player2Stats;

        public enum GameState
        {
            StartMenu,
            RoundStart,
            Player1Pending,
            Player1ExecutingAction,
            Player2Pending,
            Player2ExecutingAction,
            RoundEnded,
            GameFinished
        }

        private GameState _currentGameState;
        private bool _player1StartedPerformingAction = false;
        private bool _player2StartedPerformingAction = false;
        private const float TimeToPerformAction = 1.5f;

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
            startMenu.SetActive(true);
            player1Canvas.SetActive(false);
            player2Canvas.SetActive(false);
            _player1Stats = player1.GetComponent<PlayerStats>();
            _player2Stats = player2.GetComponent<PlayerStats>();
            playerStatsCanvas.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            switch (_currentGameState)
            {
                case GameState.StartMenu:
                    // Time.timeScale = 0f;
                    break;
                case GameState.RoundStart:
                    if (!playerStatsCanvas.activeSelf)
                    {
                        playerStatsCanvas.SetActive(true);
                    }
                    _player1Stats.InitializePlayerStats();
                    _player2Stats.InitializePlayerStats();
                    ChangeState(GameState.Player1Pending);

                    // Time.timeScale = 1f;
                    break;
                case GameState.Player1Pending:
                    // Time.timeScale = 0f;
                    if (!player1Canvas.activeSelf)
                    {
                        player1Canvas.SetActive(true);
                    }

                    break;
                case GameState.Player1ExecutingAction:
                    if (!_player1StartedPerformingAction)
                    {
                        // Time.timeScale = 1f;
                        _player1StartedPerformingAction = true;
                        StartCoroutine(ChangeToPendingPlayerState());

                        if (player1Canvas.activeSelf)
                        {
                            player1Canvas.SetActive(false);
                        }
                    }

                    break;
                case GameState.Player2Pending:
                    // Time.timeScale = 0f;
                    if (!player2Canvas.activeSelf)
                    {
                        player2Canvas.SetActive(true);
                    }

                    break;
                case GameState.Player2ExecutingAction:
                    if (!_player2StartedPerformingAction)
                    {
                        // Time.timeScale = 1f;
                        _player2StartedPerformingAction = true;
                        StartCoroutine(ChangeToPendingPlayerState());
                        if (player2Canvas.activeSelf)
                        {
                            player2Canvas.SetActive(false);
                        }
                    }

                    break;
                case GameState.RoundEnded:
                    // Time.timeScale = 0f;
                    if (playerStatsCanvas.activeSelf)
                    {
                        playerStatsCanvas.SetActive(false);
                    }
                    break;
                case GameState.GameFinished:
                    // Time.timeScale = 0f;
                    break;
            }
        }

        public void ChangeState(GameState newState)
        {
            string currentGameStateString = Enum.GetName(typeof(GameState), _currentGameState);
            string newStateString = Enum.GetName(typeof(GameState), newState);
            Debug.Log("State changed: " + currentGameStateString + " -> " + newStateString);
            _currentGameState = newState;
            switch (newState)
            {
                case GameState.Player1ExecutingAction:
                    _player1StartedPerformingAction = false;
                    break;
                case GameState.Player2ExecutingAction:
                    _player2StartedPerformingAction = false;
                    break;
            }
        }

        IEnumerator ChangeToPendingPlayerState()
        {
            yield return new WaitForSeconds(TimeToPerformAction);
            switch (_currentGameState)
            {
                case GameState.Player1ExecutingAction:
                    ChangeState(GameState.Player2Pending);
                    break;
                case GameState.Player2ExecutingAction:
                    ChangeState(GameState.Player1Pending);
                    break;
            }
        }

        public GameState GetCurrentGameState()
        {
            return _currentGameState;
        }

        public void StartButtonPressed()
        {
            startMenu.SetActive(false);
            ChangeState(GameState.RoundStart);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}