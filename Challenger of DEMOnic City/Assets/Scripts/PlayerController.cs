using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public float playerSpeed = 50f;
    public float playerTurnSpeed = 30f;

    public float horizontalInput;
    public float forwardInput;

    //Statsistics on UI
    public enum ControlType { HumanInput}
    public ControlType controlType = ControlType.HumanInput;


    public float BestLapTime { get; private set; } = Mathf.Infinity;
    public float LastLapTime { get; private set; } =  0;
    public float CurrentLapTime { get; private set; } = 0;
    public int CurrentLap {  get; private set; } = 0;

    private float lapTimer;
    private float lastcheckpointPassed = 0;

    private Transform checkpointsParent;
    private int checkpointCount;
    private int checkpointLayer;
    //private CarController carController;

    private void Awake()
    {
        checkpointsParent = GameObject.Find("Checkpoints").transform;
        checkpointCount = checkpointsParent.childCount;
        checkpointLayer = LayerMask.NameToLayer("Checkpoint");
        //carController
    }


    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed * forwardInput);
        transform.Rotate(Vector3.up * Time.deltaTime * playerTurnSpeed * horizontalInput);
    }
}
