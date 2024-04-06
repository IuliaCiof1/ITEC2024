using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] TMP_Text descriptionText;
    [SerializeField] Image contentImage;


    CardsSystem cardSystem;

    GameObject front, back;

    bool cardTurned = false;
    bool thisCardTurned = false;

    private void Start()
    {
        front = transform.GetChild(0).gameObject;
        back = transform.GetChild(1).gameObject;

        back.SetActive(true);
        front.SetActive(false);

        cardSystem = transform.root.GetComponent<CardsSystem>();
        print(cardSystem.name);
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
        //thisCardTurned = true;

        if (!cardSystem.cardTurned)
        {
            StartCoroutine(RotateCardSmooth());
            cardSystem.cardTurned = true;
            thisCardTurned = true;
        }
        else if (!thisCardTurned)
        {
            //StartCoroutine(Delay());
            StartCoroutine(RotateCardSmooth());
        }

        //Vector3 rotateTo = new Vector3(transform.rotation.x, 90, transform.rotation.z);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.EulerAngles(new Vector3()))
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
}
