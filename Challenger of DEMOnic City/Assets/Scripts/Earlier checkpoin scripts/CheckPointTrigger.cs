using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointTrigger : MonoBehaviour
{
    public GameObject LapCompleteTrig;

    public GameObject CheckpointTrig;

    void OnTriggerEnter()
    {
        LapCompleteTrig.SetActive(true);

        CheckpointTrig.SetActive(false);
    }
}
