using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class OldCheckpointsAndLaps : MonoBehaviour
{
    [Header("Checkpoints")]
    public GameObject start;
    public GameObject end;
    public GameObject[] checkpoints;

    [Header("Settings")]
    public float laps = 1;

    [Header("Information")]
    public float currentCheckpoint;
    public float currentLap;
    private bool started;
    private bool finished;

    private float currentLapTime;
    private float bestLapTime;
    private float bestLap;

    private void Start()
    {
        currentCheckpoint = 0;
        currentLap = 1;


        started = false;
        finished = false;

        currentLapTime = 0;
        bestLapTime = 0;
        bestLap = 0;
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

    private void Respawn()
    {
        foreach (GameObject checkpoint in checkpoints)
        {
            checkpoint.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Checkpoint"))
        {
            GameObject thisCheckpoint = other.gameObject;

            // Loop through the checkpoints and compare and check which one the player passed through
            for (int i = 0; i < checkpoints.Length; i++)
            {
                if (finished)
                {
                    return;
                }
                // If the checkpoint is correct
                if (thisCheckpoint == checkpoints[i] && i == currentCheckpoint)
                {
                    print($"Correct Checkpoint: {Mathf.FloorToInt(currentLapTime / 60)}:{currentLapTime % 60:00.000}");
                    currentCheckpoint++;
                    thisCheckpoint.SetActive(false);
                }

                // If the checkpoint is incorrect
                else if (thisCheckpoint == checkpoints[i] && i != currentCheckpoint)
                {
                    print("Incorrect checkpoint");
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
                        }
                        finished = true;
                        print("Finished");
                    }
                    else
                    {
                        print("Did not go through all checkpoints");
                    }
                }
                // If all laps are not finished, start a new lap
                else if (currentLap <= laps)
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
                        Debug.Log($"Stared lap {currentLap}");

                        Respawn();


                    }
                    else
                    {
                        print("Old not go through all the checkpoints");
                    }
                }
            }
        }
    }
}