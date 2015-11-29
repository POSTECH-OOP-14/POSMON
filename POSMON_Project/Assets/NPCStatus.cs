using UnityEngine;
using System.Collections;

public class NPCStatus : MonoBehaviour {
    public enum NPCType
    {
        TALKER,
        SHOPPER,
        TRAINER,
        DOCTOR
    };

    public enum FaceDirection
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    };

    public int NPC_number;
    private NPCType type;
    public FaceDirection Facing;
   
    /* Queest Information */
    public Quest quest = null;

    /* Data for Battle */
    public Student[] student_list = new Student[6];

	// Use this for initialization
	void Start () {
        switch (NPC_number)
        {
            case 1:
                type = NPCType.TALKER;
                quest = new Quest(this.gameObject.name, 1, 100);
                break;

            case 2:
                type = NPCType.TRAINER;
                // student_list initialize
                break;

            case 3:
                type = NPCType.SHOPPER;
                break;

            case 4:
                break;

            case 5:
                break;

            case 6:
                break;
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /* Shop Menu Pop-Up */
    public void openShop()
    {
        // this.gameObject.GetComponent<GUIbuttonsho>().menuToggle = true;
    }

    /* when got collision + Activate Input(Z) from Player, it would be executed */
    public void interaction(GameObject player)
    {
        bool dialogue_occurence = false;
        GameObject pl = GameManager.pl_stored;

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


        /* if quest exists */
        if (this.quest != null)
        {
            Debug.Log("here!");
            /* give new quest */
            if (quest.isCompleted() != true && quest.getProgress() == false)
            {
                this.gameObject.GetComponent<Dialogue>().ChangeDialogue("queststart");
                player.GetComponent<CharacterStatus>().setBlocked(true);
                this.gameObject.GetComponent<Dialogue>().TurnOnDialogue();
                // Something that pop dialogue //
                quest.setProgress(true);
                player.GetComponent<CharacterStatus>().addQuest(ref quest);
                Debug.Log("Got Quest");
            }
        }

        /* check if the NPC is target NPC */
        Quest qst;
        if ((qst = pl.GetComponent<CharacterStatus>().isTarget(NPC_number)) != null)
        {
            this.gameObject.GetComponent<Dialogue>().ChangeDialogue("questend");
            player.GetComponent<CharacterStatus>().setBlocked(true);
            this.gameObject.GetComponent<Dialogue>().TurnOnDialogue();
            qst.QuestClear();
            dialogue_occurence = true;
            player.GetComponent<CharacterStatus>().deleteQuest(ref qst);
            Debug.Log("Cleared Quest");
        }

        if (type == NPCType.SHOPPER)
        {
            // Shpping Menu Pop Up //
        }
        else if (type == NPCType.TRAINER)
        {
            // Battle Phase Should be Occur //
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
        else
        {
            Debug.LogError("Invalid NPC Type");
        }

        /* Default Dialogue */
        if (dialogue_occurence == false)
        {
            player.GetComponent<CharacterStatus>().setBlocked(true);
            this.gameObject.GetComponent<Dialogue>().TurnOnDialogue();
            dialogue_occurence = true;
            Debug.Log("Default Dialogue Occur");
        }
    }


}

