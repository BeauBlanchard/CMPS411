using UnityEngine;
using System.Collections;

public class CarMovement : MonoBehaviour
{
    public float currentSpeed;
    float acceleration = 1.0f;
    float maxSpeed = 0.75f;
    float minSpeed = -0.5f;
    bool slowDown = false;
    bool speedUp = false;

	void Start () {
	
	}
	
	void FixedUpdate ()
	{
	    if (Input.GetKey(KeyCode.W))
	    {
	        if (currentSpeed <= maxSpeed)
	        {
	            currentSpeed += Time.deltaTime*acceleration;
	        }

	    }
        else if (Input.GetKey(KeyCode.W))
        {
            slowDown = true;
        }

	    CheckForwardSpeed();
	    CheckBackwardSpeed();
        UpdateSpeed();

	    if (Input.GetKey(KeyCode.S))
	    {
            if (currentSpeed >= minSpeed)
            {
                currentSpeed -= Time.deltaTime * acceleration;
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                speedUp = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            //Quaternion carRotation = gameObject.transform.rotation;
            //carRotation = new Quaternion(carRotation.x, carRotation.y, carRotation.z);
            //gameObject.transform.position = carRotation;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Rotate(Vector3.right*Time.deltaTime);
        }
	}

    void CheckBackwardSpeed()
    {
        if (speedUp == true)
        {
            if (currentSpeed <= 0)
            {
                currentSpeed += Time.deltaTime * acceleration;
            }
            else if (currentSpeed >= 0)
            {
                speedUp = false;
            }
        }
    }

    void CheckForwardSpeed()
    {
        if (slowDown == true)
        {
            if (currentSpeed >= 0)
            {
                currentSpeed -= Time.deltaTime*acceleration;
            }
            else if (currentSpeed <= 0)
            {
                slowDown = false;
            }
        }
    }
    void UpdateSpeed()
    {
        Vector3 carPosition = gameObject.transform.position;
        carPosition = new Vector3(carPosition.x, carPosition.y, carPosition.z + currentSpeed);
        gameObject.transform.position = carPosition;
    }

    /*
    void UpdateRotation()
    {
        Quaternion carRotation = gameObject.transform.rotation;
        carRotation = new Quaternion(carRotation.x, carRotation.y, carRotation.z + currentSpeed);
        gameObject.transform.rotation = carRotation;
    }
    */
}  
