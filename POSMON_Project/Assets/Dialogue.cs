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
            GUIStyle myStyle = new GUIStyle(GUI.skin.box);
            myStyle.fontSize = 25;
            myStyle.font = (Font)Resources.Load("NANUMBARUNGOTHICBOLD", typeof(Font));
            myStyle.normal.textColor = Color.white;
            GUI.Box(new Rect(1, Screen.height - 90, Screen.width, 90), dialoge1 + "\n" + dialoge2, myStyle);
        }
    }

	// Use this for initialization
	void Start () {
        file = new System.IO.FileInfo(System.IO.Directory.GetCurrentDirectory() + "/NPCDialogue/" + gameObject.GetComponent<NPCStatus>().NPC_number.ToString() + ".txt");
        sr = file.OpenText();
        i = 1;
        dialoge1 = sr.ReadLine();
        dialoge2 = sr.ReadLine();	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && bshow == true || bshow == true && Input.GetKey(KeyCode.Z))
        {
            if (sr.Peek() >= 0)
            {
                i++;
                dialoge1 = sr.ReadLine();
                i++;
                dialoge2 = sr.ReadLine();
            }
            else
            {
                bshow = false;
                GameManager.pl_stored.GetComponent<CharacterStatus>().setBlocked(false);
                TriggerNPCEvent();
            }
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            bshow = false;
        }

    }

    private void TriggerNPCEvent()
    {
        /* Trigger Shop Interface */
        if (gameObject.GetComponent<NPCStatus>().type == NPCStatus.NPCType.SHOPPER)
        {
        }
        /* Trigger Battle Interface */
        else if (gameObject.GetComponent<NPCStatus>().type == NPCStatus.NPCType.TRAINER)
        {
        }
    }

    public bool ChangeDialogueToDefault()
    {
        if (bshow == false)
        {
            sr.Close();
            file = new System.IO.FileInfo(System.IO.Directory.GetCurrentDirectory() + "/NPCDialogue/" + gameObject.GetComponent<NPCStatus>().NPC_number.ToString() + ".txt");
            sr = file.OpenText();
            i = 1;
            dialoge1 = sr.ReadLine();
            dialoge2 = sr.ReadLine();
            return true;
        }
        return false;
    }

    public bool ChangeDialogue(string file_name)
    {
        if (bshow == false)
        {
            sr.Close();
            file = new System.IO.FileInfo(System.IO.Directory.GetCurrentDirectory() + "/NPCDialogue/" + gameObject.GetComponent<NPCStatus>().NPC_number.ToString() + "_" + file_name + ".txt");
            sr = file.OpenText();
            i = 1;
            dialoge1 = sr.ReadLine();
            dialoge2 = sr.ReadLine();
            return true;
        }
        return false;
    }

    public bool getDialogueOnOff()
    {
        return bshow;
    }

    public void TurnOnDialogue()
    {
        bshow = true;
    }
}
