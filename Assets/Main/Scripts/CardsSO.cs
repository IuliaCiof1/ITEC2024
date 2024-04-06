using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/Cards")]
public class CardsSO : ScriptableObject
{
    public string name;
    public Sprite contentImage;
    public string description;

    public int healthEffect;
    public bool isHealthPositive;
    public int staminaEffect;
    public bool isStaminahPositive;


    public GameObject weapon;
}
