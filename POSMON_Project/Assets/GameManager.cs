using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    /* For the purpose of Global Variables */
    static public GameObject pl_stored = null;
    static public int PrevSceneNumber = 0;

    public GameObject player;

	// Use this for initialization
	void Start ()
    {
        if (!pl_stored)
        {
            pl_stored = Instantiate(player, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
            pl_stored.SetActive(false);
            DontDestroyOnLoad(pl_stored);
        }
	}

	// Update is called once per frame
	void Update ()
    {
	}

    /* Start Game */
    public void StartGame()
    {
        pl_stored.SetActive(true);
        PrevSceneNumber = Application.loadedLevel;
        Application.LoadLevel(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
