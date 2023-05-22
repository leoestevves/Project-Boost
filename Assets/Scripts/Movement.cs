using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotating();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Pressed SPACE - Thrusting");
        }        
    }

    void ProcessRotating()
    {
        /*
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Rotating left");
        }

        else if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Rotating right");
        }
        */

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            Debug.Log("Rotating left");
        }

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            Debug.Log("Rotating right");
        }

    }

}