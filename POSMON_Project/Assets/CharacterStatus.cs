using UnityEngine;
using System.Collections;

public class CharacterStatus : MonoBehaviour
{

    enum status
    {
        IDLE,
        MOVE_LEFT,
        MOVE_RIGHT,
        MOVE_DOWN,
        MOVE_UP
    };

    enum face_direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    };

    /* constants for movement */
    private const int WALK_SLICE = 5;
    private const float WALK_DIST = 1.0f / WALK_SLICE;

    /* movement information */
    private bool blocked = false;
    private int walk_remaining = 0;
    private status moving_status = status.IDLE;
    private face_direction direction = face_direction.DOWN;

    /* inventory information */
    private Item[] inventory = new Item[256];

    /**** methods related to inventory ****/
    public void setInventory(Item item, int index)
    {
        if (inventory[index] != null)
            return;
        else
            inventory[index] = item;
    }

    public bool setInventory(Item item)
    {
        for (int i = 0; i < 256; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = item;
                return true;
            }
        }
        /* No available inventory */
        return false;
    }

    public Item getInventory(int i)
    {
        return inventory[i];
    }
    /*****************************************/

    /* Students information */
    private Student[] student_list = new Student[6];

    /**** methods related to Student list ****/
    public void setStudent(Student student, int index)
    {
        if (student_list[index] != null)
            return;
        else
            student_list[index] = student;
    }

    public bool setStudent(Student student)
    {
        for (int i = 0; i < 6; i++)
        {
            if (student_list[i] == null)
            {
                student_list[i] = student;
                return true;
            }
        }
        /* no available space for additional student */
        return false;
    }

    public Student getStudent(int i)
    {
        return student_list[i];
    }
    /*****************************************/


    private Quest[] quest_list = new Quest[100];
    /**** methods related to Quest ****/
    public bool addQuest(ref Quest q)
    {
        for (int i = 0; i < 100; i++)
        {
            if (quest_list[i] == null)
                quest_list[i] = q;
            return true;
        }
        return false;
    }

    public Quest getQuest(int index)
    {
        return quest_list[index];
    }

    public Quest isTarget(int NPCNumber)
    {
        for (int i = 0; i < 100; i++)
        {
            if (quest_list[i] != null)
            {
                if (quest_list[i].getTarget() == NPCNumber)
                    return quest_list[i];
            }
        }
        return null;
    }

    public bool deleteQuest(ref Quest q)
    {
        for (int i = 0; i < 100; i++)
        {
            if (quest_list[i] == q)
            {
                quest_list[i] = null;
                return true;
            }
        }
        return false;
    }
    /**********************************/
    

	// Use this for initialization
	void Start ()
    {
        walk_remaining = 0;
        moving_status = status.IDLE;
        direction = face_direction.DOWN;
	}

    public int getStudentCount()
    {
        int count = 0;
        for (int i = 0; i < 6; i++)
            if (student_list[i] != null)
                count++;
        return count;
    }

    public int getItemCount()
    {
        int count = 0;
        for (int i = 0; i < 256; i++)
            if (inventory[i] != null)
                count++;
        return count;
    }

    private void SetMovement()
    {
        if (moving_status == status.IDLE)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                walk_remaining = WALK_SLICE;
                moving_status = status.MOVE_LEFT;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                walk_remaining = WALK_SLICE;
                moving_status = status.MOVE_RIGHT;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                walk_remaining = WALK_SLICE;
                moving_status = status.MOVE_DOWN;
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                walk_remaining = WALK_SLICE;
                moving_status = status.MOVE_UP;
            }
        }
    }

    private void UpdateMovement()
    {
        switch (moving_status)
        {
            case status.MOVE_LEFT:
                transform.position = new Vector2(transform.position.x - WALK_DIST, transform.position.y);
                direction = face_direction.LEFT;
                walk_remaining--;
                break;

            case status.MOVE_RIGHT:
                transform.position = new Vector2(transform.position.x + WALK_DIST, transform.position.y);
                direction = face_direction.RIGHT;
                walk_remaining--;
                break;

            case status.MOVE_DOWN:
                transform.position = new Vector2(transform.position.x, transform.position.y - WALK_DIST);
                direction = face_direction.DOWN;
                walk_remaining--;
                break;

            case status.MOVE_UP:
                transform.position = new Vector2(transform.position.x, transform.position.y + WALK_DIST);
                direction = face_direction.UP;
                walk_remaining--;
                break;

            default:
                break;
        }

        if (walk_remaining <= 0)
        {
            walk_remaining = 0;
            moving_status = status.IDLE;
            SetMovement();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (blocked != true)
        {
            /* Set Movement Mode */
            SetMovement();

            /* Move */
            UpdateMovement();
        }
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision Occur");

        /* Round position to board grid */
        {
            gameObject.transform.position = new Vector3(Mathf.Round(gameObject.transform.position.x),
                                                        Mathf.Round(gameObject.transform.position.y),
                                                        0f);
            walk_remaining = 0;
            moving_status = status.IDLE;
        }

        /* When Collide with NPC Instance, check the object's tag "NPC" and if
         * the object's tag is "NPC" then call NPC's method */
        if (other.gameObject.tag == "NPC")
        {
            /* When Collision occurred, if keyinput existed.... */
            if (true)
            {
                other.gameObject.GetComponent<NPCStatus>().interaction(this.gameObject);                
            }
        }
    }

    public void setBlocked(bool block)
    {
        blocked = block;
    }
}
