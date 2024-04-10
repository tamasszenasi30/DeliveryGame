using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider : MonoBehaviour
{
    // reference for the green image on slider,
    // public for being accessible for other scripts
    public Image sliderImage;

    // Start is called before the first frame update
    void Start()
    {
        // Unity editor know we mean the Image component when we mention sliderImage reference
        sliderImage = GetComponent<Image>();
        // filling up the sliderImage with 10f
        sliderImage.fillAmount = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        // constantly taking amount out from the fill amount as we are consuming petrol
        sliderImage.fillAmount -= 0.005f * Time.deltaTime;

        // if fill amount goes down under 0
        if(sliderImage.fillAmount < 0)
        {
            // keeping it as 0
            sliderImage.fillAmount = 0.0f;
        }
    }
}
