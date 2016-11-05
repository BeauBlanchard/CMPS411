using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine.UI;

public class Checkpoints : MonoBehaviour
{
    public static Transform PlayerTransform;

    void Start()
    {
        PlayerTransform = GameObject.Find("Player").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player Collider"))
        {
            if (transform ==
                PlayerTransform.GetComponent<CarCheckpoints>().CheckpointList[CarCheckpoints.CurrentCheckpoint]
                    .transform)
            {
                if (CarCheckpoints.CurrentCheckpoint + 1 < CarCheckpoints.NumCheckpoints)
                {
                    if(CarCheckpoints.CurrentCheckpoint + 2 < CarCheckpoints.NumCheckpoints)
                    {
                        CarCheckpoints.NextCheckpoint = CarCheckpoints.CurrentCheckpoint + 2;
                    }
                    else
                    {
                        if(CarCheckpoints.CurrentCheckpoint + 2 == 0)
                        {
                            CarCheckpoints.NextCheckpoint = 1;
                        }
                        else
                        {
                            CarCheckpoints.NextCheckpoint = 0;
                        }          
                    }
                    if(CarCheckpoints.CurrentCheckpoint < CarCheckpoints.NumCheckpoints)
                    {
                        CarCheckpoints.LastCheckpoint = CarCheckpoints.CurrentCheckpoint;            
                    }
                    else
                    {
                        CarCheckpoints.LastCheckpoint = 0;
                    }
                    CarCheckpoints.CurrentCheckpoint++;
                }
                else
                {
                    CarCheckpoints.CurrentCheckpoint = 0;
                }
                //Debug.Log("N: " + CarCheckpoints.NextCheckpoint + "  C: " + CarCheckpoints.CurrentCheckpoint + "  L: " + CarCheckpoints.LastCheckpoint);
                //print("asdf: " + CarCheckpoints.NumCheckpoints);
            }                
            }
        else
        {
            return;
        }
    }

    /*
    private void IncrementCheckpoint()
    {
        for(var i = 0; i < PlayerTransform.GetComponent<CarWaypoints>().WaypointArray.Length; i++)
        {
            PlayerTransform.GetComponent<CarWaypoints>().WaypointArray[i].GetChild(0).gameObject.SetActive(false);
        }
        PlayerTransform.GetComponent<CarWaypoints>().WaypointArray[CarWaypoints.CurrentWaypoint].GetChild(0).gameObject.SetActive(true);
    }
    */
}