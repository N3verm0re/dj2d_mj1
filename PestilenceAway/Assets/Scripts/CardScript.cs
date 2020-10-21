using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public ChoiceCard card;
    //public int cardID;
    public bool selected = false;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public Image cardImage;
    void Start()
    {
        titleText.text = card.name;
        descriptionText.text = card.description;
        cardImage.sprite = card.spriteArt;
    }
}
