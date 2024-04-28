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

    public bool moveable;


    public bool Moveable()
    {
        if (Time.deltaTime >= 5)
        {
            moveable = true;
            playerSpeed = 50f;
        }
        else if (Time.deltaTime <= 5)
        {
            moveable = false;
            playerSpeed = 0f;
        }

        return moveable;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Moveable();
        if (moveable)
        {

        }
        */
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed * forwardInput);
        transform.Rotate(Vector3.up * Time.deltaTime * playerTurnSpeed * horizontalInput);
    }
}