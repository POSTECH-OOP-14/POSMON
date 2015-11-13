using UnityEngine;
using System.Collections;

public class ButtonManage : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnGUI()
    {
    if(GUI.Button(new Rect(10,10,150,150), "I am a Button"))
        print("you clicked BTN");
    
    }
}
