using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // integer for the player's money
    public float score;

    // reference for the Text component on UI
    Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        // when we mention scoreText we refer to the Text Component
        scoreText = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        // setting Text Component's text to the score converted to string 
        scoreText.text =  score.ToString("n1");
        
    }

    public float RandomMoneyGenerator()
    {
        score = Random.Range(3.8f, 8.0f);
        return score;
    }

    
}
