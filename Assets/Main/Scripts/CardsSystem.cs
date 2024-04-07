using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts
{
    public class CardsSystem : MonoBehaviour
    {
        //[SerializeField] GameObject cardsCanvas;
        List<Card> cards;
        [SerializeField] List<CardsSO> powerCard;

        public bool cardTurned = false;

        private void Start()
        {
            // InitializeCardSystem();
        }

        public void InitializeCardSystem()
        {
            cardTurned = false;
            cards = new List<Card>();

            foreach (Transform card in transform)
            {
                Card cardComp = card.GetComponent<Card>();
                if (cardComp != null)
                {
                    cardComp.InitializeCard();
                    cards.Add(cardComp);
                }
            }

            GenerateCardSet();
        }

        void GenerateCardSet()
        {
            List<int> prizeIndex = new List<int>();
            for (int i = 0; i < powerCard.Count; i++)
            {
                prizeIndex.Add(i);
            }

            int randomPrize = Random.Range(0, prizeIndex.Count);
            cards[0].CreateCard(powerCard[prizeIndex[randomPrize]]);
            prizeIndex.RemoveAt(randomPrize);

            randomPrize = Random.Range(0, prizeIndex.Count);
            cards[1].CreateCard(powerCard[prizeIndex[randomPrize]]);
            prizeIndex.RemoveAt(randomPrize);

            randomPrize = Random.Range(0, prizeIndex.Count);
            cards[2].CreateCard(powerCard[prizeIndex[randomPrize]]);
            prizeIndex.RemoveAt(randomPrize);
        }


        void ShowCardsCanvas()
        {
            gameObject.SetActive(true);
        }


        public void ShowAllCards()
        {
            foreach (Card card in cards)
            {
                StartCoroutine(Delay(card));
            }
        }

        IEnumerator Delay(Card card)
        {
            yield return new WaitForSeconds(2f);
            card.TurnCard();
        }

        public void TurnBackAllCards()
        {
            foreach (Card card in cards)
            {
                card.InitializeCard();
                card.TurnCard();
                card.InitializeCard();
            }

            cardTurned = false;
        }
    }
}