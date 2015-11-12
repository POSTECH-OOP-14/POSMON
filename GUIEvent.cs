using UnityEngine;
using System.Collections;

public class GUIEvent : MonoBehaviour {

    public string Note = null;

    void mouse_down()
    {
        Game_Manager.note.post(this, Note);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
