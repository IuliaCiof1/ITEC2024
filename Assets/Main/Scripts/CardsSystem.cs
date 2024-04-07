using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts
{
    public class CardsSystem : MonoBehaviour
    {
        //[SerializeField] GameObject cardsCanvas;
        List<Card> cards;

        [SerializeField] List<CardsSO> badCard;
        [SerializeField] List<CardsSO> weakCard;
        [SerializeField] List<CardsSO> powerCard;

        public bool cardTurned = false;

        private void Start()
        {
            cards = new List<Card>();

            foreach (Transform card in transform)
            {
                if (card.GetComponent<Card>())
                    cards.Add(card.GetComponent<Card>());
            }

            GenerateCardSet();
        }

        void GenerateCardSet()
        {
            //Winner set
            List<int> cardIndex = new List<int> { 0, 1, 2 };
            List<int> prizeIndex = new List<int>();

            for (int i = 0; i < badCard.Count; i++)
            {
                prizeIndex.Add(i);
            }

            int randomCard = Random.Range(0, cardIndex.Count);
            int randomPrize = Random.Range(0, prizeIndex.Count);
            print(randomCard + " " + randomPrize + " " + cards[randomCard] + " " + badCard[randomPrize]);
            cards[cardIndex.IndexOf(randomCard)].CreateCard(badCard[prizeIndex.IndexOf(randomPrize)]);
            cardIndex.RemoveAt(randomCard);


            prizeIndex.Clear();
            for (int i = 0; i < powerCard.Count; i++)
            {
                prizeIndex.Add(i);
            }

            randomCard = Random.Range(0, cardIndex.Count);
            print(cardIndex.Count);

            randomPrize = Random.Range(0, prizeIndex.Count);
            print(randomCard + " " + randomPrize + " " + cardIndex.IndexOf(randomCard) + " " +
                  powerCard[prizeIndex.IndexOf(randomPrize)]);

            print(randomCard + " " + randomPrize);
            cards[cardIndex.IndexOf(randomCard)].CreateCard(powerCard[prizeIndex.IndexOf(randomPrize)]);

            cardIndex.RemoveAt(randomCard);
            prizeIndex.RemoveAt(randomPrize);


            print(cardIndex);


            randomCard = Random.Range(0, cardIndex.Count);
            print(cardIndex.Count);

            randomPrize = Random.Range(0, prizeIndex.Count);
            print(randomCard + " " + randomPrize + " " + cards[randomCard] + " " + powerCard[randomPrize]);

            print(randomCard + " " + randomPrize);
            cards[cardIndex.IndexOf(randomCard)].CreateCard(powerCard[prizeIndex.IndexOf(randomPrize)]);
            cardIndex.RemoveAt(randomCard);
            prizeIndex.RemoveAt(randomPrize);

            //Loser set
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
            // print("before 10 seconds");
            yield return new WaitForSeconds(2f);
            card.TurnCard();
            // print("after 10 seconds");
        }
    }
}