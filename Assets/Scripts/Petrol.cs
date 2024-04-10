using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Petrol : MonoBehaviour
{
    UI uiScript;
    Slider sliderScript;

    public int petrolPrice = 5;
    private bool canBuyPetrol = true;

    private void Start()
    {
        canBuyPetrol = true;
        uiScript = FindObjectOfType<UI>();
        sliderScript = FindObjectOfType<Slider>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && uiScript.score > petrolPrice && canBuyPetrol)
        {
            canBuyPetrol = false;
            Debug.Log("Hello");
            // fills up the petrol completely
            sliderScript.sliderImage.fillAmount = 1f;

            // takes 5 from the score
            uiScript.score -= petrolPrice;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        canBuyPetrol = true;
    }
}
