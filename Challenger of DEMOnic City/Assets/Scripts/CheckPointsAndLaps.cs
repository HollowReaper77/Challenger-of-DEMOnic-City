using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckPointsAndLaps : MonoBehaviour
{
    
    [Header("Checkpoints")]

    public GameObject start;
    public GameObject end;
    public GameObject[] checkpoints;

    [Header("Settings")]
    private float laps = 1;

    [Header("Information")]

    private float currentCheckpoint;
    private float currentLap;
    private bool started;
    private bool finished;

    private float currentLapTime;
    private float bestLapTime;
    private float bestLap;



    [Header("UIinformation")]
    public TextMeshProUGUI warningText;

    /*
    public TextMeshProUGUI lap;
    public TextMeshProUGUI bestlap;
    public TextMeshProUGUI time;
    public TextMeshProUGUI bestTime;
    public TextMeshProUGUI progress;
    */

    private void Start()
    {
        currentCheckpoint = 0;
        currentLap = 1;

        started = false;
        finished = false;

        currentLapTime = 0;
        bestLapTime = 0;
        bestLap = 0;

        warningText.text = " ";

    }

    private void Update()
    {
        if (started && !finished)
        {
            currentLapTime += Time.deltaTime;

            if (bestLap == 0)
            {
                bestLap = 1;
            }
        }

        if (started)
        {
            if (bestLap == currentLap)
            {
                bestLapTime = currentLapTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            GameObject thisCheckpoint = other.gameObject;
            
            // Race Start
            if (thisCheckpoint == start && !started)
            {
                Debug.Log("Started");
                warningText.text = "Start!";
                started = true;
            }
            // End of the lap/race
            else if (thisCheckpoint == end && started)
            {
                // If all checkpoints activated then the race is ended
                if (currentLap == laps)
                {
                    if (currentCheckpoint == checkpoints.Length)
                    {
                        if (currentLapTime < bestLapTime)
                        {
                            bestLap = currentLap;
                        }
                        finished = true;
                        started = false;
                        Debug.Log("Finished");
                        warningText.text = "Finished!";
                    }
                    else
                    {
                        Debug.Log("Missing checkpoints");
                        warningText.text = "Missing checkpoints";
                    }
                }
                //
                else if (currentLap < laps)
                {
                    if (currentCheckpoint == checkpoints.Length)
                    {
                        if (currentLapTime < bestLapTime)
                        {
                            bestLap = currentLap;
                            bestLapTime = currentLapTime;
                        }
                        currentLap++;
                        currentCheckpoint = 0;
                        currentLapTime = 0;
                        Debug.Log($"started current lap {currentLap} - {Mathf.FloorToInt(currentLapTime / 60)}:{currentLapTime % 60:00.00}");
                    }
                }
                else
                {
                    Debug.Log("Missing Checkpoints");
                    warningText.text = "Missing checkpoints";
                }
            }
            //
            for (int i = 0; i < checkpoints.Length; i++)
            {
                if (finished)
                //{
                    return;
                //}

                //
                if (thisCheckpoint == checkpoints[i] && i == currentCheckpoint)
                {
                    Debug.Log("Correct checkpoint");
                    warningText.text = "Correct checkpoint";
                    currentCheckpoint++;
                }
                //
                else if(thisCheckpoint == checkpoints[i] && i != currentCheckpoint)
                {
                    Debug.Log("Wrong checkpoint");
                    warningText.text = "Wrong checkpoint";
                }
            }
        }
    }

    private void OnGUI()
    {
        string formattedCurrentTime = $"Current: {Mathf.FloorToInt(currentLapTime / 60)}:{currentLapTime % 60:00.00}+ (Lap: {currentLap})";
        GUI.Label(new Rect(50, 10, 250, 100), formattedCurrentTime);

        string formattedBestTime = $"Best: {Mathf.FloorToInt(bestLapTime / 60)}:{bestLapTime % 60:00.00} + (Lap: {bestLap})";
        GUI.Label(new Rect(250, 10, 250, 100), formattedBestTime);
    }
}
