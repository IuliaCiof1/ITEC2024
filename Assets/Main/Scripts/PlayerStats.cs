using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Main.Scripts
{
    public class PlayerStats : MonoBehaviour
    {
        public int maxHealth = 100;
        public float CurrentHealth { get; private set; }
        private int healthModif = 0;

        private int baseDamage = 20;
        private int damageModif = 0;

        public int maxStamina = 100;
        public float CurrentStamina { get; private set; }
        private int _lostStamina = 30;
        private int lostStaminaModif = 0;
        private PlayerController _playerController;
        private float timeToWaitForDeathAnimation = 3.5f;
        [SerializeField] private GameObject player1StatsCanvas;
        [SerializeField] private GameObject player2StatsCanvas;
        public CameraFollow camFollow;

        private struct SkillCostModifier
        {
            public const float Weak = 0.7f;
            public const float Medium = 1f;
            public const float Strong = 1.3f;
        }

        private struct SkillDamageModifier
        {
            public const float Weak = 0.4f;
            public const float Medium = 0.7f;
            public const float Strong = 1f;
        }

        public AudioSource deathSource;
        public AudioClip clip;
        public AudioClip lightHitClip;
        public AudioClip highHitClip;

        // public List<Weapon> Weapons = new List<Weapon>();

        private void Start()
        {
            // setCurrentHealth();
            // setDamage();
            //InitializePlayerStats();
            _playerController = gameObject.GetComponent<PlayerController>();
        }

        private void Awake()
        {
            // setCurrentHealth();
            // setDamage();
            // setLostStamina();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                TakeDamage(30);
                camFollow.ShakeCamera(4f);
            }

            Debug.Log("CurrentStamina = " + CurrentStamina + " CurrentHealth = " + CurrentHealth);
        }

        public void InitializePlayerStats()
        {
            CurrentStamina = maxStamina;
            CurrentHealth = maxHealth + healthModif;
            Debug.Log("CurrentStamina = " + CurrentStamina + " CurrentHealth = " + CurrentHealth);
            UpdateStaminaHealthCanvas();
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            Debug.Log(transform.name + " takes " + damage + " damage. ");

            if (CurrentHealth <= 0)
            {
                //sound
                Die();
            }
        }

        public void decreaseStamina()
        {
            CurrentStamina -= _lostStamina;
            Debug.Log(transform.name + " Current stamina " + CurrentStamina);
        }

        public void Die()
        {
            GameManager.Instance.PlayerDied();
            GetComponent<AnimationManager>().StartDeathAnimation();
            deathSource.PlayOneShot(clip);
            StartCoroutine(IncrementWinnerCounter());
            Debug.Log(transform.name + "died.");
        }

        //
        // public void setCurrentHealth()
        // {
        //     CurrentHealth = maxHealth + healthModif;
        // }
        //
        // public void setDamage()
        // {
        //     damage = baseDamage + damageModif;
        // }
        //
        // public void setLostStamina()
        // {
        //     _lostStamina += _lostStamina + lostStaminaModif;
        // }
        //
        IEnumerator IncrementWinnerCounter()
        {
            yield return new WaitForSeconds(timeToWaitForDeathAnimation);
            switch (_playerController.GetPlayerNr())
            {
                case PlayerController.PlayerNr.Player1:
                    GameManager.Instance.setPlayer2WinCounter();
                    break;
                case PlayerController.PlayerNr.Player2:
                    GameManager.Instance.setPlayer1WinCounter();
                    break;
            }

            GameManager.Instance.ChangeState(GameManager.GameState.RoundEnded);
        }

        public void RegenStamina()
        {
            CurrentStamina = Mathf.Clamp(CurrentStamina + maxStamina * 0.3f, 0f, maxStamina);
            UpdateStaminaHealthCanvas();
        }

        public float GetWeakAttackCost()
        {
            return _lostStamina * SkillCostModifier.Weak + lostStaminaModif;
        }


        public float GetMidAttackCost()
        {
            return _lostStamina * SkillCostModifier.Medium + lostStaminaModif;
        }


        public float GetStrongAttackCost()
        {
            return _lostStamina * SkillCostModifier.Strong + lostStaminaModif;
        }

        public void DecreaseStaminaWeakSkill()
        {
            CurrentStamina -= _lostStamina * SkillCostModifier.Weak + lostStaminaModif;
            UpdateStaminaHealthCanvas();
        }

        public void DecreaseStaminaMediumSkill()
        {
            CurrentStamina -= _lostStamina * SkillCostModifier.Medium + lostStaminaModif;
            UpdateStaminaHealthCanvas();
        }


        public void DecreaseStaminaStrongSkill()
        {
            CurrentStamina -= _lostStamina * SkillCostModifier.Strong + lostStaminaModif;
            UpdateStaminaHealthCanvas();
        }

        public void setModifs(int dmgModif, int hpModif, int lStaminaModif) {
            damageModif = dmgModif;
            healthModif = hpModif;
            lostStaminaModif = lStaminaModif;
        }

        public void TakeDamageWeak()
        {
            float dealtDamage = (baseDamage + damageModif) * SkillDamageModifier.Weak;
            CurrentHealth = Mathf.Clamp(CurrentHealth - dealtDamage, 0f, maxHealth + healthModif);
            Debug.Log(transform.name + " takes " + dealtDamage + " damage. ");
            deathSource.PlayOneShot(lightHitClip);

            UpdateStaminaHealthCanvas();
            if (CurrentHealth <= 0)
            {
                //sound
                Die();
            }
        }

        public void TakeDamageMedium()
        {
            float dealtDamage = (baseDamage + damageModif) * SkillDamageModifier.Medium;
            CurrentHealth = Mathf.Clamp(CurrentHealth - dealtDamage, 0f, maxHealth + healthModif);
            Debug.Log(transform.name + " takes " + dealtDamage + " damage. ");
            camFollow.ShakeCamera(2f);
            deathSource.PlayOneShot(lightHitClip);
            UpdateStaminaHealthCanvas();

            if (CurrentHealth <= 0)
            {
                //sound
                Die();
            }
        }


        public void TakeDamageStrong()
        {
            float dealtDamage = (baseDamage + damageModif) * SkillDamageModifier.Strong;
            CurrentHealth = Mathf.Clamp(CurrentHealth - dealtDamage, 0f, maxHealth + healthModif);
            Debug.Log(transform.name + " takes " + dealtDamage + " damage. ");
            camFollow.ShakeCamera(4f);
            deathSource.PlayOneShot(highHitClip);

            UpdateStaminaHealthCanvas();
            if (CurrentHealth <= 0)
            {
                //sound
                Die();
            }
        }

        private void UpdateStaminaHealthCanvas()
        {
            string player1HealthStr = "Player1Health";
            string player2HealthStr = "Player2Health";
            string player1StaminaStr = "Player1Stamina";
            string player2StaminaStr = "Player2Stamina";


            string textHealthStr = "TextHealth";
            string textStaminaStr = "TextStamina";

            if (player1StatsCanvas.activeSelf && player2StatsCanvas.activeSelf)
            {
                // Sliders
                switch (_playerController.GetPlayerNr())
                {
                    case PlayerController.PlayerNr.Player1:
                        foreach (Transform child in player1StatsCanvas.transform)
                        {
                            Slider slider = child.GetComponent<Slider>();
                            if (slider != null && child.gameObject.name == player1HealthStr)
                            {
                                slider.value = CurrentHealth / (maxHealth + healthModif);
                                break;
                            }
                        }

                        foreach (Transform child in player1StatsCanvas.transform)
                        {
                            Slider slider = child.GetComponent<Slider>();
                            if (slider != null && child.gameObject.name == player1StaminaStr)
                            {
                                slider.value = CurrentStamina / maxStamina;
                                break;
                            }
                        }

                        break;
                    case PlayerController.PlayerNr.Player2:

                        foreach (Transform child in player2StatsCanvas.transform)
                        {
                            Slider slider = child.GetComponent<Slider>();
                            if (slider != null && child.gameObject.name == player2HealthStr)
                            {
                                slider.value = CurrentHealth / (maxHealth + healthModif);
                                break;
                            }
                        }

                        foreach (Transform child in player2StatsCanvas.transform)
                        {
                            Slider slider = child.GetComponent<Slider>();
                            if (slider != null && child.gameObject.name == player2StaminaStr)
                            {
                                slider.value = CurrentStamina / maxStamina;
                                break;
                            }
                        }

                        break;
                }

                // Texts
                switch (_playerController.GetPlayerNr())
                {
                    case PlayerController.PlayerNr.Player1:
                    {
                        TextMeshProUGUI[] textMeshPros1 = player1StatsCanvas.GetComponentsInChildren<TextMeshProUGUI>();
                        foreach (TextMeshProUGUI textMeshPro in textMeshPros1)
                        {
                            if (textMeshPro.gameObject.name == textHealthStr)
                            {
                                textMeshPro.text = string.Format("{0:0}/{1:0}", CurrentHealth,
                                    (maxHealth + healthModif));
                                break;
                            }
                        }

                        foreach (TextMeshProUGUI textMeshPro in textMeshPros1)
                        {
                            if (textMeshPro.gameObject.name == textStaminaStr)
                            {
                                textMeshPro.text = string.Format("{0:0}/{1:0}", CurrentStamina, maxStamina);
                                break;
                            }
                        }
                    }
                        break;
                    case PlayerController.PlayerNr.Player2:
                    {
                        TextMeshProUGUI[] textMeshPros2 = player2StatsCanvas.GetComponentsInChildren<TextMeshProUGUI>();
                        foreach (TextMeshProUGUI textMeshPro in textMeshPros2)
                        {
                            if (textMeshPro.gameObject.name == textHealthStr)
                            {
                                textMeshPro.text = string.Format("{0:0}/{1:0}", CurrentHealth,
                                    (maxHealth + healthModif));
                                break;
                            }
                        }

                        foreach (TextMeshProUGUI textMeshPro in textMeshPros2)
                        {
                            if (textMeshPro.gameObject.name == textStaminaStr)
                            {
                                textMeshPro.text = string.Format("{0:0}/{1:0}", CurrentStamina, maxStamina);
                                break;
                            }
                        }
                    }

                        break;
                }
            }
        }
    }
}