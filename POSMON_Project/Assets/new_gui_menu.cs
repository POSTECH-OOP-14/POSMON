using UnityEngine;
using System.Collections;

public class new_gui_menu : MonoBehaviour {
    public Camera cam;

    public Canvas can = null;
    
    private bool bshow;
    
	void OnGUI(){
        if (bshow)
        {
            GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 100, 90), "Main Menu");
            if (GUI.Button(new Rect(Screen.width / 2 + 10, Screen.height / 2 + 30, 80, 20), "Exit"))
            {
                Application.Quit();

            }

            if (GUI.Button(new Rect(Screen.width / 2 + 10, Screen.height / 2 + 60, 80, 20), "Cancel"))
            {
                hide();
            }
        }
	}
	
	// Use this for initialization
	void Start () {
        can = GetComponent<Canvas>();
        bshow = true;
        hide();
	}

    public void hide()
    {
        bshow = false;
    }

    public void show()
    {
        bshow = true;
        
    }

    

  

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!bshow)
            {
                show();
            }
            else
            {
                hide();
            }
        }
	
	}
}
