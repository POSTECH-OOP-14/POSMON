using UnityEngine;
using System.Collections;

public class MoveToMain : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeScene()
    {
        GameLoad.pl_stored.SetActive(false);
        Application.LoadLevel(0);
    }
}
