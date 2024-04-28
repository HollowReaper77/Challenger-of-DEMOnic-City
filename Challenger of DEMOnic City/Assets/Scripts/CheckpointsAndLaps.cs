using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class CheckpointsAndLaps : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject finishMenuUI;

    [Header("Checkpoints")]
    public GameObject start;
    public GameObject end;
    public GameObject[] checkpoints;

    [Header("Settings")]
    public float laps = 1;
    private string warning;

    [Header("Information")]
    public float currentCheckpoint;
    public float currentLap;
    private bool started;
    private bool finished;

    private float racetime;

    private float currentLapTime;
    private float bestLapTime;
    private float bestLap;

    [Header("UIinformation")]
    public TextMeshProUGUI warningText;

    
    public TextMeshProUGUI lapText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI bestlapText;
    public TextMeshProUGUI bestTimeText;


    private void Start()
    {
        currentCheckpoint = 0;
        currentLap = 1;

        racetime = 0;

        started = false;
        finished = false;

        currentLapTime = 0;
        bestLapTime = 0;
        bestLap = 0;
    }

    public void Update()
    {
        // ide kell tenni az összes UI-t
        if (started && !finished)
        {
            racetime += Time.deltaTime; 
            currentLapTime += Time.deltaTime;
            //timeText.text = currentLapTime.ToString();
            timeText.text = $"Time: {Mathf.FloorToInt(currentLapTime / 60)}:{currentLapTime % 60:00:000}";

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
                //bestTimeText.text = "Best: "+ bestLapTime.ToSafeString();
            }
        }
        if (bestLapTime <= currentLapTime)
        {
            bestTimeText.text = $"Latest best time: \n" + timeText.text;
        }
        /*
        if (currentLapTime < bestLapTime)
        {
            bestLap = currentLap;


            Debug.Log("BestLap:" + bestLap);
            bestlapText.text = "Best: " + bestLap; // vagy ide?
            bestLapTime = currentLapTime;
            Debug.Log("BestTime" + bestLapTime);
            bestTimeText.text = "Best Time:" + bestLapTime;
        }
        */
        if (finished)
        {
            //finishMenuUI.SetActive(true);
            ShowFinishMenu();

        }

        ShowWarningMessage(warning);

    }

    private void Respawn()
    {
        foreach (GameObject checkpoint in checkpoints)
        {
            checkpoint.SetActive(true);
        }
    }
    /*
    private void ShowFinishMenu(bool mutasd) // bool mutasd
    {
        finishMenuUI.SetActive(mutasd);
    }
    */

    public void ShowFinishMenu()
    {
        finishMenuUI.SetActive(true);
    }

    public string ShowWarningMessage(string warning)
    {
        if (Time.deltaTime <= 3f)
        {
            warningText.text = warning;
        }
        else if(Time.deltaTime >= 3f)
        {
            warningText.text = " ";
        }
        return warningText.text;
    }

    public void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Checkpoint"))
        {
            GameObject thisCheckpoint = other.gameObject;
            GameObject finishMenu = finishMenuUI.gameObject;

            // Loop through the checkpoints and compare and check which one the player passed through
            for (int i = 0; i < checkpoints.Length; i++)
            {
                if (finished)
                {
                    //ShowFinishMenu(true);
                    finishMenu.SetActive(true);
                    return;
                }
                // If the checkpoint is correct
                if (thisCheckpoint == checkpoints[i] && i == currentCheckpoint)
                {
                    print($"Correct Checkpoint: {Mathf.FloorToInt(currentLapTime / 60)}:{currentLapTime % 60:00.000}");
                    currentCheckpoint++;
                    ShowWarningMessage("Correct Checkpoint");
                    thisCheckpoint.SetActive(false);
                }

                // If the checkpoint is incorrect
                else if (thisCheckpoint == checkpoints[i] && i != currentCheckpoint)
                {
                    print("Incorrect checkpoint");
                    ShowWarningMessage("Incorrect checkpoint");
                }
            }

            // Started the race
            if (thisCheckpoint == start && !started)
            {
                print("Started");
                started = true;

            }
            // Ended the lap or race
            else if (thisCheckpoint == end && started)
            {
                // If all laps are finished, end the race
                if (currentLap == laps)
                {
                    if (currentCheckpoint == checkpoints.Length)
                    {
                        if (currentCheckpoint < bestLap)
                        {
                            bestLap = currentLap;
                            bestlapText.text = "Best: "+bestLap; // ez tényleg ide kéne?
                        }
                        finished = true;
                        ShowWarningMessage("végeeeee");
                        print("Finished");
                        ShowWarningMessage("végeeeee");
                    }
                    else
                    {
                        print("Did not go through all checkpoints");
                        ShowWarningMessage("Did not go through all checkpoints");
                    }
                }
                // If all laps are not finished, start a new lap
                else if (currentLap < laps)
                {
                    if (currentCheckpoint == checkpoints.Length)
                    {

                        if (currentLapTime < bestLapTime)
                        {
                            bestLap = currentLap;
                            Debug.Log("BestLap:"+bestLap);
                            bestlapText.text = "Best: " + bestLap; // vagy ide?
                            bestLapTime = currentLapTime;
                            Debug.Log("BestTime"+bestLapTime);
                            bestTimeText.text = "Best Time:"+bestLapTime;
                            ShowWarningMessage("beeeszt");
                        }
                        currentLap++;
                        lapText.text = "Lap: "+currentLap;
                        currentCheckpoint = 0;
                        currentLapTime = 0;
                        Debug.Log($"Stared lap {currentLap}");
                        ShowWarningMessage("beeeszt");
                        Respawn();


                    }
                    else
                    {
                        print("Wrong checkpoint!");
                        ShowWarningMessage("Wrong checkpoint!");
                    }
                }
            }
        }
    }
}