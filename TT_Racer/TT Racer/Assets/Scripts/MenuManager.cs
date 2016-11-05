using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public GameObject Singleplayer;
    public GameObject Multiplayer;

	void Start () {
	
	}
	
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Escape))
	    {
            LoadTrack(1);
        }
	}

    public void SingleplayerPress()
    {
        LoadTrack(3);
    }

    public void MultiplayerPress()
    {
        // 
    }

    public void LoadTrack(int trackNumber)
    {
        SceneManager.LoadScene(trackNumber);
    }
}
