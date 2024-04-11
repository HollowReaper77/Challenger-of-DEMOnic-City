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

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed * forwardInput);
        transform.Rotate(Vector3.up * Time.deltaTime * playerTurnSpeed * horizontalInput);
    }
}
