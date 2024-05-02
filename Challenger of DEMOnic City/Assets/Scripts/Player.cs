using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    public enum ControlType { HumanInput, AI}
    public ControlType controlType = ControlType.HumanInput;

    public float BestLapTime {  get; private set; } = Mathf.Infinity;

    public float LastLapTime {  get; private set; } = 0;

    public float CurrentLapTime {  get; private set; } = 0;

    public int CurrentLap {  get; private set; } = 0;

    private float lapTimerTimestamp;
    private int lastCheckpointPassed = 0;

    private Transform checkpointsParent;
    private int checkpointCount;
    private int checkpointLayer;
    private OLDCarController carController;


    public TextMeshProUGUI warningText;



    [Header("Settings")]

    public bool around;
    public int maxLaps;
    private string finishCheckpoint;


    void Awake()
    {
        checkpointsParent = GameObject.Find("Checkpoints").transform;
        checkpointCount = checkpointsParent.childCount;
        checkpointLayer = LayerMask.NameToLayer("Checkpoint");
        carController = GetComponent<OLDCarController>();

        if (around == true)
        {
            finishCheckpoint = "1";
        }
        else
        {
            finishCheckpoint = checkpointCount.ToString();
        }

    }

    void StartLap()
    {
        Debug.Log("StartLap");
        CurrentLap++;
        lastCheckpointPassed = 1;
        lapTimerTimestamp = Time.time;
    }

    void EndLap()
    {
        LastLapTime = Time.time - lapTimerTimestamp;
        BestLapTime = Mathf.Min(LastLapTime, BestLapTime);
        Debug.Log("EndLap - LapTime was "+ LastLapTime + "seconds");

    }

    void FinishRace()
    {
        Debug.Log("Finish");
    }

    // kivételeket megírni, hogy például és módosítani az OldCheckpointSystem alapján

    void OnTriggerEnter(Collider collider)
    {
        // ha az objektum layer-je nem egyezik meg a checkpoint layer-ével
        if(collider.gameObject.layer != checkpointLayer)
        {
            return;
        }
        // ha az objektum neve a finish checkpoint-nak értéke ami ebben az estben string

        // körversenyre állítására szolgál
        // startvonalat ellenörzi
        if (collider.gameObject.name == finishCheckpoint)
        {
            // ha az utolsó checkpoint egyenlõ a checkpointokkal akkor új kör
            if (lastCheckpointPassed == checkpointCount)
            {

                EndLap();
                /*
                if (CurrentLap == maxLaps)
                {
                    FinishRace();
                }
                */
            }
            /*
            else
            {
                FinishRace();
            }
            */

            /*
            if (CurrentLap == 0 || lastCheckpointPassed == checkpointCount)
            {
                StartLap();
            }
            */
            // ha 
            if (CurrentLap == 0 || lastCheckpointPassed == checkpointCount)
            {
                StartLap();
            }
            if(CurrentLap == maxLaps)
            {
                FinishRace();
            }


            return;
        }

        /*
        // A-ból B-be versenyre állítja át
        else if (collider.gameObject.name == finishCheckpoint)
        {
            if (lastCheckpointPassed == checkpointCount)
            {
                EndLap();
            }
            if (CurrentLap == 0 || lastCheckpointPassed == checkpointCount)
            {
                StartLap();
            }
            if (CurrentLap == maxLaps)
            {
                FinishRace();
            }
            return;
        }
        */
        // checkpoint-okat számolja, ha az aktuálistól nagyobb egyel a következõ, külömben hibás checkpoint felírat
        if (collider.gameObject.name == (lastCheckpointPassed+1).ToString())
        {
            lastCheckpointPassed++;
            Debug.Log("Checkpoint passed"+lastCheckpointPassed);
        }
        /*
        else
        {
            Debug.Log("Missing checkpoints!");
        }
        */
    }
    // Update is called once per frame
    void Update()
    {
        CurrentLapTime = lapTimerTimestamp > 0 ? Time.time -lapTimerTimestamp : 0;
        /*
        if (controlType == ControlType.HumanInput)
        {
            carController.Steer = GameManager.Instance.InputController.SteerInput;
            carController.Throttle = GameManager.Instance.InputController.ThrottleInput;
        }
        */
    }
}
