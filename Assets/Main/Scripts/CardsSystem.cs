using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardsSystem : MonoBehaviour
{
    [SerializeField] GameObject cardsCanvas;
    List <GameObject> cards;

    List<CardsSO> badCard;
    List<CardsSO> weakCard;
    List<CardsSO> powerCard;



    private void Start()
    {
        int i = 0;
        foreach(Transform card in cardsCanvas.transform)
        {
            cards[i] = card.gameObject;
        }

        

    }

    void ShowCards()
    {
        cardsCanvas.SetActive(true);


    }
}

