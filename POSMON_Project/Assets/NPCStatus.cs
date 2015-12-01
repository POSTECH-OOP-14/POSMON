using UnityEngine;
using System.Collections;

public class NPCStatus : MonoBehaviour {
    public enum NPCType
    {
        TALKER,
        SHOPPER,
        TRAINER,
        DOCTOR,
        WARP
    };

    public enum FaceDirection
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    };

    public int NPC_number;
    public NPCType type;
    public FaceDirection Facing;
   
    /* Quest Information */
    private Quest quest = null;
    public int QuestTargetNPCNumber;
    public int QuestReward;

    /* Data for Battle */
    public Student[] student_list = new Student[6];

    /* Data for Warp */
    public int ChangeSceneTo;
    public float WarpPositionX;
    public float WarpPositionY;

    /* Battle Data */
    private bool battleEnd = false;

	// Use this for initialization
	void Start () {
        if (QuestTargetNPCNumber != 0 && type == NPCType.TALKER)
            quest = new Quest(NPC_number, QuestTargetNPCNumber, QuestReward);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /* when got collision + Activate Input(Z) from Player, it would be executed */
    public void interaction(GameObject player)
    {
        bool dialogue_occurence = false;
        GameObject pl = GameManager.pl_stored;

        if (type != NPCType.WARP && type != NPCType.DOCTOR && type != NPCType.SHOPPER && NPC_number < 100 && NPC_number > 0)
        {
            if (gameObject.GetComponent<Dialogue>().getDialogueOnOff() == false)
            {
                /* Facing Direction Update */
                if (gameObject.transform.position.x < player.transform.position.x && Mathf.Abs(gameObject.transform.position.y - player.transform.position.y) < 0.5)
                {
                    Facing = FaceDirection.RIGHT;
                }
                else if (gameObject.transform.position.x > player.transform.position.x && Mathf.Abs(gameObject.transform.position.y - player.transform.position.y) < 0.5)
                {
                    Facing = FaceDirection.LEFT;
                }
                else if (gameObject.transform.position.y < player.transform.position.y && Mathf.Abs(gameObject.transform.position.x - player.transform.position.x) < 0.5)
                {
                    Facing = FaceDirection.UP;
                }
                else if (gameObject.transform.position.y > player.transform.position.y && Mathf.Abs(gameObject.transform.position.x - player.transform.position.x) < 0.5)
                {
                    Facing = FaceDirection.DOWN;
                }

                /* if quest is not given */
                if (GameManager.QuestGiven[NPC_number] == false && quest != null)
                {
                    Debug.Log("Giving Quest...");
                    /* give new quest */
                    if (this.gameObject.GetComponent<Dialogue>().ChangeDialogue("queststart"))
                    {
                        player.GetComponent<CharacterStatus>().setBlocked(true);
                        this.gameObject.GetComponent<Dialogue>().TurnOnDialogue();
                        dialogue_occurence = true;
                        quest.setProgress(true);
                        player.GetComponent<CharacterStatus>().addQuest(quest);
                        GameManager.QuestGiven[NPC_number] = true;
                    }
                    Debug.Log("Got Quest!");
                }
                /* Dialogue in quest progressing */
                else if (GameManager.QuestGiven[NPC_number] == true && pl.GetComponent<CharacterStatus>().getQuest(NPC_number) != null)
                {
                    if (pl.GetComponent<CharacterStatus>().getQuest(NPC_number).getProgress() == true)
                    {
                        if (this.gameObject.GetComponent<Dialogue>().ChangeDialogue("questprogress"))
                        {
                            player.GetComponent<CharacterStatus>().setBlocked(true);
                            this.gameObject.GetComponent<Dialogue>().TurnOnDialogue();
                            dialogue_occurence = true;
                        }
                    }
                }

                /* check if the NPC is target NPC */
                else
                {
                    Debug.Log("Checking the NPC is target or not");
                    Quest qst;
                    if ((qst = pl.GetComponent<CharacterStatus>().isTarget(NPC_number)) != null)
                    {
                        Debug.Log("I am quest Target ! ");
                        if (this.gameObject.GetComponent<Dialogue>().ChangeDialogue("questend_" + qst.getHostNPCNumber().ToString() ))
                        {
                            player.GetComponent<CharacterStatus>().setBlocked(true);
                            this.gameObject.GetComponent<Dialogue>().TurnOnDialogue();
                            dialogue_occurence = true;
                            player.GetComponent<CharacterStatus>().deleteQuest(qst.getHostNPCNumber());
                        }
                        Debug.Log("Cleared Quest");
                    }
                }
            }
        }

        if (type != NPCType.WARP)
        {
            /* Default Dialogue */
            if (dialogue_occurence == false)
            {
                if (gameObject.GetComponent<Dialogue>().ChangeDialogueToDefault())
                {
                    player.GetComponent<CharacterStatus>().setBlocked(true);
                    this.gameObject.GetComponent<Dialogue>().TurnOnDialogue();
                    dialogue_occurence = true;
                    Debug.Log("Default Dialogue Occur");
                }
            }
        }

        /* Trigger Each Type's Special Things */
        if (type == NPCType.SHOPPER)
        {
            // Shpping Menu Pop Up //
        }
        else if (type == NPCType.TRAINER)
        {
            if (battleEnd == false)
            {
                // Battle Phase Should be Occur //
            }
        }
        else if (type == NPCType.DOCTOR)
        {
            for (int i = 0; i < 6; i++)
            {
                Student temp;
                if ((temp = pl.GetComponent<CharacterStatus>().getStudent(i)) != null)
                    ; // restore the student's current health
            }
        }
        else if (type == NPCType.TALKER)
        {
        }
        else if (type == NPCType.WARP)
        {
            GameManager.ChangeMap(ChangeSceneTo);
            GameManager.WarpCharacter(WarpPositionX, WarpPositionY);
            dialogue_occurence = true;
        }
        else
        {
            Debug.LogError("Invalid NPC Type");
        }
    }
}

