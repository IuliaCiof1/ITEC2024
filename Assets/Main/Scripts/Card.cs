using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] TMP_Text descriptionText;
    [SerializeField] Image contentImage;

    void CreateCard(CardsSO cardSO)
    {
        contentImage.sprite = cardSO.contentImage;
        descriptionText.text = cardSO.description;
    }
}
