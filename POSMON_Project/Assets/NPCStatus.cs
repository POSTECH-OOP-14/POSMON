﻿using UnityEngine;
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
    private bool battleEnd = false;

    public int[] students_level = new int[6];
    public stu_no[] students_type = new stu_no[6];

    /* Data for Warp */
    public int ChangeSceneTo;
    public float WarpPositionX;
    public float WarpPositionY;    

	// Use this for initialization
	void Start () {
        /* Make Quest if Target exists */
        if (QuestTargetNPCNumber != 0 && type == NPCType.TALKER)
            quest = new Quest(NPC_number, QuestTargetNPCNumber, QuestReward);

        /* Make Student List for Trainer */
        for (int i = 0; i < 6; i++)
        {
            if (students_level[i] != 0)
            {
                student_list[i] = new Student(students_type[i]);
                for (int j = students_level[i]; j > 1; j--)
                    student_list[i].levelUP();
            }
        }
	}

    // Update is called once per frame
    void Update()
    {
	
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
                /* check if the NPC is target NPC */
                else if (pl.GetComponent<CharacterStatus>().isTarget(NPC_number) != null)
                {
                    Debug.Log("Checking the NPC is target or not");
                    Quest qst = pl.GetComponent<CharacterStatus>().isTarget(NPC_number);
                    if (type == NPCType.TRAINER)
                    {
                        if (this.gameObject.GetComponent<Dialogue>().ChangeDialogue("questend_" + qst.getHostNPCNumber().ToString() ))
                        {
                            player.GetComponent<CharacterStatus>().setBlocked(true);
                            this.gameObject.GetComponent<Dialogue>().TurnOnDialogue();
                            dialogue_occurence = true;
                        }
                        Debug.Log("Cleared Quest");
                    }

                    if (qst != null)
                    {
                        Debug.Log("I am quest Target ! ");
                        if (this.gameObject.GetComponent<Dialogue>().ChangeDialogue("questend_" + qst.getHostNPCNumber().ToString() ))
                        {
                            player.GetComponent<CharacterStatus>().setBlocked(true);
                            this.gameObject.GetComponent<Dialogue>().TurnOnDialogue();
                            dialogue_occurence = true;
                            qst.QuestClear();
                            player.GetComponent<CharacterStatus>().deleteQuest(qst.getHostNPCNumber());
                        }
                        Debug.Log("Cleared Quest");
                    }
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

        /* Trigger WARP */
        if (type == NPCType.WARP)
        {
            GameManager.ChangeMap(ChangeSceneTo);
            GameManager.WarpCharacter(WarpPositionX, WarpPositionY);
            dialogue_occurence = true;
        }
    }
}

