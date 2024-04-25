using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NUnit.Framework.Constraints;
using System;
public class CountdownTimer : MonoBehaviour
{
    
    float currentTime = 0f;
    float startingTime = 5f;

    [SerializeField] TextMeshProUGUI coundownText;

    // Start is called before the first frame update
    public void Start()
    {
        currentTime = startingTime;
        coundownText.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        coundownText.text = currentTime.ToString("0");
        /*
        if (currentTime >= 0)
        {
            Time.timeScale = 0f;
        }
        */
        if (currentTime <= 1 && currentTime >= 0)
        {
            coundownText.text = "GO!";
        }

        if (currentTime <= 0)
        {
            //currentTime = 0;

            coundownText.enabled = false;
        }
    }

    


    /*
    public int countdownTime;
    public TextMeshProUGUI countdownDisplay;

    private void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        countdownDisplay.text = "GO";

        yield return new WaitForSeconds(1f);

        countdownDisplay.gameObject.SetActive(false);
    }
    */
}
