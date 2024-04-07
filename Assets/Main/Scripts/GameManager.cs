using System;
using System.Collections;
using TMPro;
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
        [SerializeField] private GameObject cardsCanvas;
        [SerializeField] private GameObject playerStatsCanvas;
        [SerializeField] private GameObject backgroundMusic;
        [SerializeField] private TextMeshProUGUI player1WinText;
        [SerializeField] private TextMeshProUGUI player2WinText;
        private PlayerStats _player1Stats;
        private PlayerStats _player2Stats;
        private CardsSystem _cardsSystem;
        private int player1WinCounter = 0;
        private int player2WinCounter = 0;
        private int maxNumberOfRounds = 5;
        private Vector3 _player1InitialPosition;
        private Vector3 _player2InitialPosition;
        private bool _onePlayerDied;

        public enum GameState
        {
            StartMenu,
            RoundStart,
            Player1Pending,
            Player1ExecutingAction,
            Player2Pending,
            Player2ExecutingAction,
            RoundEnded,
            SelectWeaponPlayer1,
            SelectWeaponPlayer2,
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
            cardsCanvas.SetActive(false);
            _player1Stats = player1.GetComponent<PlayerStats>();
            _player2Stats = player2.GetComponent<PlayerStats>();
            playerStatsCanvas.SetActive(false);
            _player1InitialPosition = _player1Stats.transform.position;
            _player2InitialPosition = _player2Stats.transform.position;
            _onePlayerDied = false;
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

                    if (cardsCanvas.activeSelf)
                    {
                        cardsCanvas.SetActive(false);
                    }

                    _player1Stats.InitializePlayerStats();
                    _player2Stats.InitializePlayerStats();
                    _player1Stats.transform.position = _player1InitialPosition;
                    _player2Stats.transform.position = _player2InitialPosition;
                    _player1Stats.GetComponent<Animator>().Play("PlayerIdle");
                    _player2Stats.GetComponent<Animator>().Play("PlayerIdle");
                    backgroundMusic.SetActive(true);
                    ChangeState(GameState.Player1Pending);
                    _onePlayerDied = false;

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
                    if (playerStatsCanvas.activeSelf)
                    {
                        playerStatsCanvas.SetActive(false);
                    }

                    if (player1Canvas.activeSelf)
                    {
                        player1Canvas.SetActive(false);
                    }

                    if (player2Canvas.activeSelf)
                    {
                        player2Canvas.SetActive(false);
                    }

                    if (maxNumberOfRounds <= player1WinCounter + player2WinCounter)
                    {
                        ChangeState(GameState.GameFinished);
                    }
                    else
                    {
                        ChangeState(GameState.SelectWeaponPlayer1);
                    }

                    break;
                case GameState.SelectWeaponPlayer1:
                    if (!cardsCanvas.activeSelf)
                    {
                        cardsCanvas.SetActive(true);
                        TextMeshProUGUI[] textMeshProsP1 = cardsCanvas.GetComponentsInChildren<TextMeshProUGUI>();
                        foreach (TextMeshProUGUI textMeshPro in textMeshProsP1)
                        {
                            if (textMeshPro.gameObject.name == "TitleText")
                            {
                                textMeshPro.text = "<b>Player 1</b>, choose a card";
                                break;
                            }
                        }

                        _cardsSystem = cardsCanvas.GetComponent<CardsSystem>();
                        _cardsSystem.InitializeCardSystem();
                    }

                    break;
                case GameState.SelectWeaponPlayer2:
                    if (!cardsCanvas.activeSelf)
                    {
                        cardsCanvas.SetActive(true);
                        TextMeshProUGUI[] textMeshProsP2 = cardsCanvas.GetComponentsInChildren<TextMeshProUGUI>();
                        foreach (TextMeshProUGUI textMeshPro in textMeshProsP2)
                        {
                            if (textMeshPro.gameObject.name == "TitleText")
                            {
                                textMeshPro.text = "<b>Player 2</b>, choose a card";
                                break;
                            }
                        }

                        _cardsSystem = cardsCanvas.GetComponent<CardsSystem>();
                        _cardsSystem.InitializeCardSystem();
                    }

                    break;
                case GameState.GameFinished:
                    // Time.timeScale = 0f;
                    break;
            }
        }

        public void ChangeState(GameState newState)
        {
            if (_onePlayerDied && (newState != GameState.RoundEnded) && (newState != GameState.GameFinished) &&
                (newState != GameState.RoundStart) && (newState != GameState.SelectWeaponPlayer1) &&
                (newState != GameState.SelectWeaponPlayer2)) return;
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

        IEnumerator CloseSelectWeaponState()
        {
            yield return new WaitForSeconds(5);
            switch (_currentGameState)
            {
                case GameState.SelectWeaponPlayer1:
                    ChangeState(GameState.SelectWeaponPlayer2);
                    cardsCanvas.SetActive(false);
                    break;
                case GameState.SelectWeaponPlayer2:
                    ChangeState(GameState.RoundStart);
                    cardsCanvas.SetActive(false);
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

        public void setPlayer1WinCounter()
        {
            player1WinCounter++;
            if (player1Canvas.activeSelf && player1WinText)
            {
                player1WinText.text = string.Format("Wins: {0:0}", player1WinCounter);
            }
            if (player2Canvas.activeSelf && player2WinText)
            {
                player2WinText.text = string.Format("Wins: {0:0}", player2WinCounter);
            }
        }

        public void setPlayer2WinCounter()
        {
            player2WinCounter++;
            if (player1Canvas.activeSelf && player1WinText)
            {
                player1WinText.text = string.Format("Wins: {0:0}", player1WinCounter);
            }
            if (player2Canvas.activeSelf && player2WinText)
            {
                player2WinText.text = string.Format("Wins: {0:0}", player2WinCounter);
            }
        }

        public void PlayerDied()
        {
            _onePlayerDied = true;
        }

        public void UpdateWeaponOnPlayer(int weaponIndex)
        {
            switch (_currentGameState)
            {
                case GameState.SelectWeaponPlayer1:
                {
                    WeaponManager weaponManager = player1.GetComponentInChildren<WeaponManager>();
                    if (weaponManager != null)
                    {
                        weaponManager.NewWeapon(weaponIndex);
                    }

                    StartCoroutine(CloseSelectWeaponState());
                }
                    break;
                case GameState.SelectWeaponPlayer2:
                {
                    WeaponManager weaponManager = player2.GetComponentInChildren<WeaponManager>();
                    if (weaponManager != null)
                    {
                        weaponManager.NewWeapon(weaponIndex);
                    }

                    StartCoroutine(CloseSelectWeaponState());
                }
                    break;
            }
        }
    }
}