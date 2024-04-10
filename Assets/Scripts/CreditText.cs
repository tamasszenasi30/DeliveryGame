using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class CreditText : MonoBehaviour
{
    public TMP_Text creditText;

    // Start is called before the first frame update
    void Start()
    {
        creditText = GetComponent<TMP_Text>();
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        creditText.transform.Translate(Vector3.up * 35f * Time.deltaTime);
        BackToMenu();
    }

    void BackToMenu()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            SceneManager.LoadScene(0);
        }
    }
}
