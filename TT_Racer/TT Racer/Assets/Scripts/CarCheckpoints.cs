using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class CarCheckpoints : MonoBehaviour
{
    public List<Transform> CheckpointList = new List<Transform>();
    public static int NextCheckpoint = 0;
    public static int CurrentCheckpoint = 0;
    public static int LastCheckpoint = 0;
    public static int NumCheckpoints = 0;
    //private static Vector3 _startPosition;

    void Start()
    {
        var checkpoints = GameObject.Find("Checkpoints").GetComponentInChildren<Transform>();
        NumCheckpoints = checkpoints.childCount;
        for (var i = 0; i < checkpoints.childCount; i++)
        {
            CheckpointList.Add(checkpoints.GetChild(i));
            CheckpointList[i].gameObject.SetActive(false);
        }
        CheckpointList[0].gameObject.SetActive(true);
        // _checkpointArray[5].GetComponent<BoxCollider>().enabled = false;
        // _checkpointArray[0].GetChild(0).GetComponent<Renderer>().material.color = Color.white;
        // _checkpointArray[0].GetChild(0).gameObject.SetActive(true);
    }

    void Update()
    {
        if (CarWaypoints.CurrentWaypoint == 1)
        {
            for (var i = 0; i < NumCheckpoints; i++)
            {
                CheckpointList[i].gameObject.SetActive(true);
            }
        }
    }
}


