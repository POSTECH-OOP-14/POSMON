using UnityEngine;
using System.Collections;

public class Dialogue : MonoBehaviour {
    public AudioClip TypingSound;
    public AudioClip CureSound;
    public AudioClip BattleFailSound;

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
        if (bshow == true && (Input.GetKeyDown(KeyCode.Space) && bshow == true || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.X)))
        {
            if (sr.Peek() >= 0)
            {
                i++;
                dialoge1 = sr.ReadLine();
                i++;
                dialoge2 = sr.ReadLine();
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().PlayOneShot(TypingSound, 1);
            }
            else
            {
                bshow = false;
                GetComponent<AudioSource>().Stop();
                GameManager.pl_stored.GetComponent<CharacterStatus>().setBlocked(false);
                /* when Dialogue ends, trigger NPC's event */
                TriggerNPCEvent();
            }
        }
    }

    private void TriggerNPCEvent()
    {
        /* Trigger Shop Interface */
        if (gameObject.GetComponent<NPCStatus>().type == NPCStatus.NPCType.SHOPPER)
        {
            gameObject.GetComponent<GUIbuttonsho>().setShopActivation(true);
        }
        /* Trigger Quest Battle Interface */
        else if (gameObject.GetComponent<NPCStatus>().type == NPCStatus.NPCType.TRAINER && gameObject.GetComponent<NPCStatus>().NPC_number < 100)
        {
            Quest qst = GameManager.pl_stored.GetComponent<CharacterStatus>().isTarget(gameObject.GetComponent<NPCStatus>().NPC_number);
            /* if quest exist & has not been completed, trigger battle */
            if (qst != null && qst.isCompleted() == false)
            {
                bool playerValidity = false;
                bool trainerValidity = false;
                /* setting battle info */
                GameManager.resetBattleStudents();
                for (int i = 0; i < 6; i++)
                {
                    GameManager.setBattlePlayerStudent(i, GameManager.pl_stored.GetComponent<CharacterStatus>().getStudent(i));
                    GameManager.setBattleTrainerStudent(i, gameObject.GetComponent<NPCStatus>().getStudent(i));
                }
                GameManager.setBattleHostNum(qst.getHostNPCNumber());

                for (int i = 0; i < 6; i++)
                {
                    Student player_stu = GameManager.pl_stored.GetComponent<CharacterStatus>().getStudent(i);
                    Student trainer_stu = gameObject.GetComponent<NPCStatus>().getStudent(i);
                    if (player_stu != null && player_stu.getHP() >= 1)
                        playerValidity = true;
                    if (trainer_stu != null && trainer_stu.getHP() >= 1)
                        trainerValidity = true;
                }

                /* setting application level */
                if (playerValidity && trainerValidity)
                {
                    GameManager.pl_stored.SetActive(false);
                    GameManager.PrevSceneNumber = Application.loadedLevel;
                    Application.LoadLevel(9);
                }
                else
                    GetComponent<AudioSource>().PlayOneShot(BattleFailSound);
            }
        }
        /* Trigger Healing Students */
        else if (gameObject.GetComponent<NPCStatus>().type == NPCStatus.NPCType.DOCTOR)
        {
            for (int i = 0; i < 6; i++)
            {
                Student stu = GameManager.pl_stored.GetComponent<CharacterStatus>().getStudent(i);
                if (stu != null)
                {
                    for(int j = 0; j < 4; j++)
                    {
                        if(stu.retSkillList()[j] != null)
                            stu.retSkillList()[j].restoreNowChance();
                    }
                    stu.setHP(stu.getMAXHP());
                }
            }
            GetComponent<AudioSource>().PlayOneShot(CureSound);
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
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(TypingSound, 1);
    }
}
