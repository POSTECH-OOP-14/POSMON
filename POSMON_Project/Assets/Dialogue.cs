using UnityEngine;
using System.Collections;

public class Dialogue : MonoBehaviour {
    private bool bshow = false;
    private string dialoge1 = null;
    private string dialoge2 = null;
    private string path = System.IO.Directory.GetCurrentDirectory() ;
    System.IO.FileInfo file;
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
        file = new System.IO.FileInfo( System.IO.Directory.GetCurrentDirectory() + "/"+ this.gameObject.name + ".txt");
        sr = file.OpenText();
        i = 1;
        dialoge1 = sr.ReadLine();
        dialoge2 = sr.ReadLine();	
	}
	
	// Update is called once per frame
	void Update () {
        if (bshow == true && Input.GetKey(KeyCode.Z))
        {
            if (!bshow)
            {
                bshow = true;
            }
            else
            {
                bshow = false;
            }
            GameManager.pl_stored.GetComponent<CharacterStatus>().setBlocked(false);

        }
        else if (Input.GetKeyDown(KeyCode.Space) && bshow == true)
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

    public void ChangeDialogueToDefault()
    {
        file = new System.IO.FileInfo(System.IO.Directory.GetCurrentDirectory() + "/" + this.gameObject.name + ".txt");
        sr = file.OpenText();
        i = 1;
        dialoge1 = sr.ReadLine();
        dialoge2 = sr.ReadLine();
    }

    public void ChangeDialogue(string file_name)
    {
        file = new System.IO.FileInfo(System.IO.Directory.GetCurrentDirectory() + "/" + this.gameObject.name + "_" + file_name + ".txt");
        sr = file.OpenText();
        i = 1;
        dialoge1 = sr.ReadLine();
        dialoge2 = sr.ReadLine();
    }

    public void TurnOnDialogue()
    {
        bshow = true;
    }
}
