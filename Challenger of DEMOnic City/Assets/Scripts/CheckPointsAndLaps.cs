using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        currentCheckpoint = 0;
        currentLap = 1;

        started = false;
        finished = false;

        currentLapTime = 0;
        bestLapTime = 0;
    }
    private void Update()
    {
        if (started && !finished)
        {
            currentLapTime += Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            GameObject thisCheckpoint = other.gameObject;

            if (thisCheckpoint == start && !started)
            {
                Debug.Log("Started");
                started = true;
            }
            else if (thisCheckpoint == end && started)
            {
                if (currentLap == laps)
                {
                    if (currentCheckpoint == checkpoints.Length)
                    {
                        finished = true;
                        started = false;
                        Debug.Log("Finished");
                    }
                    else
                    {
                        Debug.Log("Missing checkpoints");
                    }
                }
                else if (currentLap < laps)
                {
                    if (currentCheckpoint == checkpoints.Length)
                    {
                        currentLap++;
                        currentCheckpoint = 0;
                        currentLapTime = 0;
                        Debug.Log($"started current lap {currentLap} - {Mathf.FloorToInt(currentLapTime / 60)}:{currentLapTime % 60:00.00}");
                    }
                }
                else
                {
                    Debug.Log("Missing Checkpoints");
                }
            }
            for (int i = 0; i < checkpoints.Length; i++)
            {
                if (finished)
                {
                    return;
                }
                if (thisCheckpoint == checkpoints[i] && i == currentCheckpoint)
                {
                    Debug.Log("Correct checkpoint");
                    currentCheckpoint++;
                }
                else if(thisCheckpoint == checkpoints[i] && i != currentCheckpoint)
                {
                    Debug.Log("Wrong checkpoint");
                }
            }
        }
    }

    private void OnGUI()
    {
        string formattedCurrentTime = $"Current: {Mathf.FloorToInt(currentLapTime / 60)}:{currentLapTime % 60:00.00}+ (Lap: {currentLap}";
        GUI.Label(new Rect(50, 10, 250, 100), formattedCurrentTime);

        string formattedBestTime = $"Current: {Mathf.FloorToInt(bestLapTime / 60)}:{bestLapTime % 60:00.00} + (Lap: {bestLap})";
        GUI.Label(new Rect(50, 10, 250, 100), formattedBestTime);
    }
}
