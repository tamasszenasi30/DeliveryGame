using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class CountDown : MonoBehaviour
{
    public float remainingSeconds;
    //public variable for the Text UI text component, named countdownText
    public Text countdownText;

    PlayerMovement playerMovRef;

    public bool countDownIsOn;

    // Start is called before the first frame update
    void Start()
    {
        // telling Unity Editor that whenever we mention countdownText we are referring to the Text component
        countdownText = GetComponent<Text>();

        // telling the Editor that the reference refers to the PlayerMovement script
        playerMovRef = FindObjectOfType<PlayerMovement>();

        ShowIdleMessage();
 
    }
 
    public void Update()
    {
        if(playerMovRef.isRestaurantZoneTouched)
        {
            StartCountDown();
        }
    }

    public float SetRandTime()
    {
        float rand = Random.Range(1.0f, 7f);
        return rand;
    }

    public void StartCountDown()
    {

        countDownIsOn = true;

        // reducing the timer by time
         remainingSeconds -= Time.deltaTime;

        // if remaining time is more than 0
        if (remainingSeconds > 0.0f)
        {
            // change the UI text to say how many time is left
            countdownText.text = "Time Remaining: " + remainingSeconds.ToString("n1") + " seconds. ";

        }

        // if timer goes down
        if(remainingSeconds < 0.0f)
        {
            // keep the timer as 0
            remainingSeconds = 0.0f;

            // indicate to the player that the food is ready
            countdownText.text = "Food is ready to go";

            StartCoroutine(WaitBetweenInstructions());
            countDownIsOn = false;

        }

    }

    public void UnloadTextOnScreen()
    {
        countdownText.text = "Unloading the food...";
    }

    public void ShowIdleMessage()
    {
        countdownText.text = "Go to the restaurant to pick up the order!";
    }

    public void ShowInstruction()
    {
        countdownText.text = "Please deliver the food.";
    }

    IEnumerator WaitBetweenInstructions()
    {
        yield return new WaitForSeconds(0.5f);
        ShowInstruction();
    }

}