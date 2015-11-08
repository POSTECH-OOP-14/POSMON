using UnityEngine;
using System.Collections;

public class GameLoad : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void ChangeScene()
    {
        Application.LoadLevel(1);
    }
}
