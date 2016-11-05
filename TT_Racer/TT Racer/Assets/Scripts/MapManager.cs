using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour {

	void Start ()
    {

	}

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            LoadTrack(2);
        }
    }

    public void LoadTrack(int trackNumber)
    {
        SceneManager.LoadScene(trackNumber);
    }
}
