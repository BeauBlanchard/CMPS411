using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CarWaypoints : MonoBehaviour
{
    public Transform[] WaypointArray = new Transform[6];
    public static int CurrentWaypoint = 0;
    public static int CurrentLap = 0;
    private static Vector3 _startPosition;

    void Start()
    {
        for(var i = 0; i < WaypointArray.Length; i++)
        { 
            WaypointArray[i].GetChild(0).GetComponent<Renderer>().material.color = Color.green;
            WaypointArray[i].GetChild(0).gameObject.SetActive(false);
        }
        WaypointArray[5].GetComponent<BoxCollider>().enabled = false;
        WaypointArray[0].GetChild(0).GetComponent<Renderer>().material.color = Color.white;
        WaypointArray[0].GetChild(0).gameObject.SetActive(true);
        _startPosition = transform.position;
    }
}


