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
        Game_Manager.note.add(this, "Show Menu");
        Game_Manager.note.add(this, "Hide Menu");
        hide();
	}

    public void hide()
    {
        visible(false);
        bshow = false;
    }

    public void show()
    {
        visible(true);
        bshow = true;
        
    }

    

    private void visible(bool bs)
    {
        Time.timeScale = (bs) ? 0 : 1;
        Game_Manager.Instance.input_allow = !bs;
        //gameObject.SetActive(bs);
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
