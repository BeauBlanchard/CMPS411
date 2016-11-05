using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;

public class CarTracker : MonoBehaviour
{
    private float _totalTime;
    private float _lapTime = 0;
    private float _fastestLapTime = float.PositiveInfinity;
    private bool _finished = false;
    private float[] _lapTimes = new float[5];
    private int _hitCount0 = 0;
    private int _hitCount1 = 0;
    public static int NumLaps = 3;
    private float _displayLastLap = 0.0f;
    private float _displayFastLap = 0.0f;
    private bool _wrongWay = false;
    private bool _offTrack = false;
    private float _timeLeft = 5.0f;
    private bool _isTriggered = false;
    private string _countdown = "";    
    private bool _showCountdown = false;

    void Start()
    {
        CarWaypoints.CurrentLap = 0;
        CarWaypoints.CurrentWaypoint = 0;
        NumLaps = 3;
        _finished = false;
        _hitCount0 = 0;
        _hitCount1 = 0;
        StartCoroutine("GetReady");
        GameObject.Find("Player").GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }

    void Update()
    {
        _totalTime += 1 * Time.deltaTime;
        //WrongWayDetection();
        if(_offTrack)
        {
            _timeLeft -= Time.deltaTime;
            if (_timeLeft <= 0.0f)
            {
                _timeLeft = 0.0f;
                //print(CarWaypoints.CurrentWaypoint - 1);
                if (CarWaypoints.CurrentWaypoint - 1 == -1)
                {
                    transform.position = Waypoints.PlayerTransform.GetComponent<CarWaypoints>().WaypointArray[5].transform.position;
                    transform.localRotation = Waypoints.PlayerTransform.GetComponent<CarWaypoints>().WaypointArray[5].transform.rotation;
                }
                else
                {
                    transform.position = Waypoints.PlayerTransform.GetComponent<CarWaypoints>().WaypointArray[
                        CarWaypoints.CurrentWaypoint - 1].transform.position;
                    transform.localRotation = Waypoints.PlayerTransform.GetComponent<CarWaypoints>().WaypointArray[
                        CarWaypoints.CurrentWaypoint - 1].transform.rotation;
                }

                GameObject.Find("Player").GetComponent<Rigidbody>().freezeRotation = true;
                GameObject.Find("Player").GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                
                //transform.forward = ... ;
                //CarController.CurrentSpeed = 0;
            }
            GameObject.Find("Player").GetComponent<Rigidbody>().freezeRotation = false;
        }
        if (_showCountdown)
        {
            Vector3 playerVelocity = GameObject.Find("Player").GetComponent<Rigidbody>().velocity;
            playerVelocity.x = 0.0f;
            playerVelocity.z = 0.0f;
            GameObject.Find("Player").GetComponent<Rigidbody>().velocity = playerVelocity;
        }
    }

    IEnumerator GetReady()
    {
        _showCountdown = true;
        _countdown = "3";
        yield return new WaitForSeconds(1.25f);
        _countdown = "2";
        yield return new WaitForSeconds(1.25f);
        _countdown = "1";
        yield return new WaitForSeconds(1.25f);
        _countdown = "GO!";
        yield return new WaitForSeconds(0.50f);

        _showCountdown = false;
        _countdown = "";

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Finish Line Collider") && _hitCount0 == 0)
        {
            _hitCount0++;
            if (CarWaypoints.CurrentLap == 0)
            {
                _lapTime = _totalTime - _lapTime;
            }
            else
            {
                _lapTimes[CarWaypoints.CurrentLap - 1] = _totalTime - _lapTime;
                _lapTime = _totalTime;
                _displayLastLap = _lapTimes[CarWaypoints.CurrentLap - 1];
                if (CarWaypoints.CurrentLap == 1)
                {
                    _fastestLapTime = _lapTimes[CarWaypoints.CurrentLap - 1];
                    _displayFastLap = _lapTimes[CarWaypoints.CurrentLap - 1];
                }
                else
                {
                    if (_lapTimes[CarWaypoints.CurrentLap - 1] < _fastestLapTime)
                    {
                        _fastestLapTime = _lapTimes[CarWaypoints.CurrentLap - 1];
                        _displayFastLap = _lapTimes[CarWaypoints.CurrentLap - 1];
                    }
                }
            }
            //print("LAP #: " + CarWaypoints.CurrentLap);
            if(CarWaypoints.CurrentLap > 3)
            {
                _finished = true;
                SceneManager.LoadScene(2);
            }
        }
        if(CarWaypoints.CurrentWaypoint == 2)
        {
            _hitCount0 = 0;
        }

        if(other.CompareTag("Terrain Collider"))
        {
            _isTriggered = true;
            _offTrack = true;
        }
        else
        {
            _offTrack = false;
            _isTriggered = false;
            _timeLeft = 5.0f;
        }
    }

    private void WrongWayDetection()
    {
        var trackDir = Waypoints.PlayerTransform.GetComponent<CarWaypoints>().WaypointArray[
            CarWaypoints.CurrentWaypoint + 1].transform.position -
                       Waypoints.PlayerTransform.GetComponent<CarWaypoints>().WaypointArray[
                           CarWaypoints.CurrentWaypoint].transform.position;
        trackDir.Normalize();
        var dotProduct = Vector3.Dot(transform.forward, trackDir);
        //print("DotProduct: " + dotProduct);
        if(dotProduct < 0)
        {
            _wrongWay = true;
        }
        else
        {
            _wrongWay = false;
        }
    }

    private void OnGUI()
    {
        Rect leftRect = new Rect(10, 10, 215, 185);
        Rect middleRect = new Rect(650, 10, 250, 75);
        GUI.skin.box.fontSize = 25;

        GUI.backgroundColor = Color.black;

        if (_showCountdown)
        {
            GUI.skin.box.alignment = TextAnchor.MiddleCenter;
            GUI.color = Color.white;
            GUI.Box(middleRect, "GET READY\n" + 
                                _countdown);
        }

        GUI.color = Color.white;
        GUI.skin.box.alignment = TextAnchor.UpperLeft;
        if (CarWaypoints.CurrentLap > 3)
        {
            CarWaypoints.CurrentLap = 3;
        }

        GUI.Box(leftRect,  Login.Username + "\n" +
                          "Checkpoint: " + CarWaypoints.CurrentWaypoint + "/5\n" +
                          "Lap: " + CarWaypoints.CurrentLap + "/3\n" +
                          "Position: 1/1\n" +
                          "Last Lap: " + _displayLastLap.ToString("###.###") + "\n" +
                          "Best Lap: " + _displayFastLap.ToString("###.###") + "\n");

        GUI.skin.box.alignment = TextAnchor.MiddleCenter;
        if (_offTrack)
        {
            GUI.Box(middleRect, "Off Track\n" + "Return in: " + _timeLeft.ToString("0.00"));
        }

        /*
        GUI.skin.box.alignment = TextAnchor.MiddleCenter;
        if (_wrongWay)
        {
            GUI.Box(new Rect(400, 10, 150, 50), "Wrong Way!");
        }
        */
    }
}