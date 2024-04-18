using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Private Variables
    [SerializeField] private float speed = 0f;
    [SerializeField] private float horsePower = 0f;
    private const float turnSpeed = 50f;
    private float horizontalInput;
    private float forwardInput;
    private Rigidbody playerRb;
    [SerializeField] GameObject centerOfMass;
    [SerializeField] TextMeshProUGUI speedometerText;
    [SerializeField] TextMeshProUGUI rpmText;
    [SerializeField] float  rpm;
    [SerializeField] List<WheelCollider> allWheels;
    [SerializeField] int wheelsOnGround;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.position;
    }


    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // Moves the car forward based on horizontal input
        //transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);

        if (IsOnGround()) 
        {
            playerRb.AddRelativeForce(Vector3.forward * horsePower * forwardInput);

            // Rotates the car based on vertical input
            transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);

            speed = Mathf.RoundToInt(playerRb.velocity.magnitude * 2.237f); //for KM/H change 2.237f to 3.6f
            speedometerText.SetText("Speed : " + speed + "MPH");

            rpm = Mathf.Round((speed % 30) * 40);
            rpmText.SetText("RPM : " + rpm);
        }


    }

    bool IsOnGround()
    {
        wheelsOnGround = 0;

        foreach (WheelCollider wheel in allWheels) 
        {
            if (wheel.isGrounded)
            {
                wheelsOnGround++;
            }
        }
        if (wheelsOnGround == 4)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
