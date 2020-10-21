using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Manager : MonoBehaviour
{
    #region Singleton
    private static Manager _instance;
    public static Manager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        Time.timeScale = 1;
    }
    #endregion

    //Win & Defeat Screen Objects
    public GameObject winScreen;
    public GameObject defeatScreen;

    //Card Variables
    GameObject choiceCard;

    //Audio Variables
    AudioSource audioSource;
    public Slider audioSlider;

    //Timer Variables
    float countdownTimer = 0f;
    [SerializeField] float timer = 30f;
    bool quarter = true;
    bool half = false;
    bool threequarter = false;

    public Image clock_bg;
    public Image clock;

    //Control Variables
    bool choiceInProgress = false;
    System.Random r = new System.Random();
    private float cardSpeed = 10f;
    public GameObject cardWaypoint;
    public GameObject deckWaypoint;

    //Stat variables
    public float populationHappiness;
    public float plague;
    public float money;
    public float cure;

    public float cureSpeed;
    public float plagueSpeed;

    public float maxPopulationHappiness;
    public float maxPlague = 100;
    public float maxMoney;
    public float maxCure = 100;

    public Slider plagueBar;
    public Slider cureBar;
    public Slider moneyBar;
    public Slider populationHappinessBar;
    void Start()
    {
        defeatScreen.SetActive(false);
        winScreen.SetActive(false);

        audioSource = this.GetComponent<AudioSource>();

        countdownTimer = timer;

        plagueBar.value = plague / maxPlague;
        cureBar.value = cure / maxCure;
        moneyBar.value = money / maxMoney;
        populationHappinessBar.value = populationHappiness / maxPopulationHappiness;
    }

    void Update()
    {
        //Win Condition - Lose Condition
        if (cure == maxCure || plague == maxPlague)
        {
            Time.timeScale = 0;

            //Loss takes priority
            if (plague == maxPlague)
            {
                defeatScreen.SetActive(true);
            }
            else
            {
                winScreen.SetActive(true);
            }
        }

        if (countdownTimer > 0f && !choiceInProgress)
        {
            countdownTimer -= 1 * Time.deltaTime;

            clock.fillAmount = (timer - countdownTimer) / timer;
            clock_bg.fillAmount = (timer - countdownTimer) / timer;

            //Event checkpoint (When timer reaches these timers game updates increments stat bars and "rolls the dice" for special events)
            if ((countdownTimer <= (timer / 4 * 3) && quarter) || (countdownTimer <= (timer / 2) && half) || (countdownTimer <= (timer / 4) && threequarter))
            {
                if (quarter)
                {
                    quarter = !quarter;
                    half = !half;
                    //Debug.Log("QUARTER TIMER!");
                }
                else if (half)
                {
                    half = !half;
                    threequarter = !threequarter;
                    //Debug.Log("HALF TIMER!");
                }
                else if (threequarter)
                {
                    threequarter = !threequarter;
                    //Debug.Log("THREE QUARTER TIMER!");
                }

                //Increment Cure & Plague
                ChangeCure(cureSpeed);
                ChangePlague(plagueSpeed);

                //Call Special Events
                EventManager.Instance.AttemptEvent();
            }
        }
        else
        {
            countdownTimer = 0;
            choiceInProgress = true;

            if (choiceCard == null || !choiceCard.GetComponent<CardScript>().selected)
            {
                choiceCard = SelectCard();
                choiceCard.GetComponent<CardScript>().selected = true;
            }
        }

        if (choiceCard != null)
        {
            //Move Card into play area
            if (choiceCard.GetComponent<CardScript>().selected == true)
            {
                if (choiceCard.GetComponent<RectTransform>().position != cardWaypoint.GetComponent<RectTransform>().position)
                {
                    choiceCard.GetComponent<RectTransform>().position = Vector3.MoveTowards(choiceCard.GetComponent<RectTransform>().position, cardWaypoint.GetComponent<RectTransform>().position, cardSpeed * Time.deltaTime);
                }
            }
            else //Move Card out of play area
            {
                if (choiceCard.GetComponent<RectTransform>().position != deckWaypoint.GetComponent<RectTransform>().position)
                {
                    choiceCard.GetComponent<RectTransform>().position = Vector3.MoveTowards(choiceCard.GetComponent<RectTransform>().position, deckWaypoint.GetComponent<RectTransform>().position, cardSpeed * Time.deltaTime);
                }
            }
        }
        
        //Update Sliders (stat bars)
        plagueBar.value = plague / maxPlague;
        cureBar.value = cure / maxCure;
        moneyBar.value = money / maxMoney;
        populationHappinessBar.value = populationHappiness / maxPopulationHappiness;
    }

    public GameObject SelectCard()
    {
        GameObject[] cards = GameObject.FindGameObjectsWithTag("ChoiceCard");

        return cards[r.Next(0, cards.Length)];
    }

    public void confirmChoice()
    {
        if (choiceInProgress)
        {
            choiceCard.GetComponent<CardScript>().selected = false;
            choiceInProgress = false;
            countdownTimer = timer;

            quarter = true;
            half = false;
            threequarter = false;
        }
    }

    //Audio
    public void ChangeAudio()
    {
        audioSource.volume = audioSlider.value;
    }

    #region ManipulateStatsFunctions
    float Calculate(float value, float maxValue, float amountToIncrease)
    {
        if (value + amountToIncrease >= maxValue)
        {
            value += maxValue - value;
        }
        else if (value + amountToIncrease < 0)
        {
            value = 0;
        }
        else
        {
            value += amountToIncrease;
        }

        return value;
    }

    public void ChangeCure(float amount)
    {
        cure = Calculate(cure, maxCure, amount);
    }
    public void ChangeCureSpeed(float amount)
    {
        cureSpeed = Calculate(cureSpeed, 100f, amount);
    }
    public void ChangePlague(float amount)
    {
        plague = Calculate(plague, maxPlague, amount);
    }
    public void ChangePlagueSpeed(float amount)
    {
        plagueSpeed = Calculate(plagueSpeed, 100f, amount);
    }
    public void ChangeMoney(float amount)
    {
        money = Calculate(money, maxMoney, amount);
    }
    public void ChangePopulationHappiness(float amount)
    {
        populationHappiness = Calculate(populationHappiness, maxPopulationHappiness, amount);
    }
    private void UpdateStats()
    {
        plague += plagueSpeed;
        cure += cureSpeed;
    }
    #endregion
}
