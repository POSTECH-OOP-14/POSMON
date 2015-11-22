using UnityEngine;
using System.Collections;

public class Dialogue : MonoBehaviour {
    private bool bshow = true;
    private string dialoge1 = null;
    private string dialoge2 = null;
    private string path = System.IO.Directory.GetCurrentDirectory() ;
    System.IO.FileInfo file = new System.IO.FileInfo( System.IO.Directory.GetCurrentDirectory() + "/"+"1"+ ".txt");
    System.IO.StreamReader sr = null;
    private int i;
    void OnGUI()
    {
        if (bshow)
        {
            GUI.Box(new Rect(1, Screen.height - 90, 500, 50), dialoge1 + "\n" + dialoge2);

        }
    }

	// Use this for initialization
	void Start () {
        sr = file.OpenText();
        i = 1;
        dialoge1 = sr.ReadLine();
        dialoge2 = sr.ReadLine();
        
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if (!bshow)
            {
                bshow = true;
            }
            else
            {
                bshow = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (sr.Peek() >= 0)
            {
                i++;
                dialoge1 = sr.ReadLine();
                i++;
                dialoge2 = sr.ReadLine();
            }
            else bshow = false;
        }

    }
}
