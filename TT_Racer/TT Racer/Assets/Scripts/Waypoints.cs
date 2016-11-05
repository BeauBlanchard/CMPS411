using UnityEngine;
using System.Collections;
using System.Net.NetworkInformation;
using UnityEngine.UI;

public class Waypoints : MonoBehaviour
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
            if(transform == PlayerTransform.GetComponent<CarWaypoints>().WaypointArray[CarWaypoints.CurrentWaypoint].transform)
            {
                PlayerTransform.GetComponent<CarWaypoints>().WaypointArray[5].GetComponent<BoxCollider>().enabled = isActiveAndEnabled;
                if (CarWaypoints.CurrentWaypoint + 1 < PlayerTransform.GetComponent<CarWaypoints>().WaypointArray.Length)
                {
                    if (CarWaypoints.CurrentWaypoint == 0)
                    {
                        if (CarWaypoints.CurrentWaypoint == 0 && CarWaypoints.CurrentLap == 1)
                        {
                            CarWaypoints.CurrentLap = 2;
                        }
                        else
                        {
                            CarWaypoints.CurrentLap++;
                        }
                    }
                    CarWaypoints.CurrentWaypoint++;
                }
                else
                {
                    CarWaypoints.CurrentWaypoint = 0;
                }

                IncrementWaypoint();
            }

        }
        else
        {
            return;
        }
    }

    private void IncrementWaypoint()
    {
        for(var i = 0; i < PlayerTransform.GetComponent<CarWaypoints>().WaypointArray.Length; i++)
        {
            PlayerTransform.GetComponent<CarWaypoints>().WaypointArray[i].GetChild(0).gameObject.SetActive(false);
        }
        PlayerTransform.GetComponent<CarWaypoints>().WaypointArray[CarWaypoints.CurrentWaypoint].GetChild(0).gameObject.SetActive(true);
    }
}
