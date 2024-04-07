using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts
{
    public class Card : MonoBehaviour
    {
        [SerializeField] TMP_Text descriptionText;
        [SerializeField] Image contentImage;
        private CardsSystem _cardSystem;

        GameObject front, back;

        bool cardTurned = false;
        bool thisCardTurned = false;

        private void Start()
        {
            front = transform.GetChild(0).gameObject;
            back = transform.GetChild(1).gameObject;

            back.SetActive(true);
            front.SetActive(false);

            _cardSystem = GetComponentInParent<CardsSystem>();
            print(_cardSystem.name);
        }

        private void Update()
        {
            Debug.Log("_cardSystem.cardTurned = " + _cardSystem.cardTurned);
        }

        //public ChangeThisCardTurned()
        //{
        //    thisCardTurned = true;
        //}

        public void CreateCard(CardsSO cardSO)
        {
            contentImage.sprite = cardSO.contentImage;
            contentImage.enabled = true;
            descriptionText.text = cardSO.description;
        }

        public void TurnCard()
        {
            if (thisCardTurned) return;

            if (!_cardSystem.cardTurned)
            {
                StartCoroutine(RotateCardSmooth());
                _cardSystem.cardTurned = true;
                thisCardTurned = true;
            }
            else if (!thisCardTurned)
            {
                //StartCoroutine(Delay());
                StartCoroutine(RotateCardSmooth());
            }

            //Vector3 rotateTo = new Vector3(transform.rotation.x, 90, transform.rotation.z);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.EulerAngles(new Vector3()))
            thisCardTurned = true;
        }

        IEnumerator Delay()
        {
            yield return new WaitForSeconds(10f);
        }


        IEnumerator RotateCardSmooth()
        {
            if (!front.activeSelf)
                for (float i = 0f; i <= 100f; i += 10f)
                {
                    transform.rotation = Quaternion.Euler(0, i, 0);

                    if (i == 90f)
                    {
                        front.SetActive(true);
                        back.SetActive(false);
                    }

                    yield return new WaitForSeconds(0.01f);
                }

            for (float i = 100f; i >= 0f; i -= 10f)
            {
                transform.rotation = Quaternion.Euler(0, i, 0);
                if (i == 0f)
                {
                    yield return null;
                }

                yield return new WaitForSeconds(0.01f);
            }
        }

        public void SelectCard()
        {
            if (thisCardTurned) return;
            TurnCard();
        }
    }
}