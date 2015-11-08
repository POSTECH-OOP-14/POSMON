using UnityEngine;
using System.Collections;

public class GameLoad : MonoBehaviour {

    
    static public GameObject pl_stored = null;
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

    void ChangeScene()
    {
        pl_stored.SetActive(true);
        Application.LoadLevel(1);
    }
}
