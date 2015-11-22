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
    private int walk_remaining = 0;
    private status moving_status = status.IDLE;
    private face_direction direction = face_direction.DOWN;

    /* inventory information */
    private Item[] inventory = new Item[256];

    /**** methods related to inventory ****/
    void setInventory(Item item, int index)
    {
        if (inventory[index] != null)
            return;
        else
            inventory[index] = item;
    }

    Item getInventory(int i)
    {
        return inventory[i];
    }
    /*****************************************/

    /* Students information */
    private Student[] student_list = new Student[6];

    /**** methods related to Student list ****/
    void setStudent(Student student, int index)
    {
        if (student_list[index] != null)
            return;
        else
            student_list[index] = student;
    }

    Student getStudent(int i)
    {
        return student_list[i];
    }
    /*****************************************/

	// Use this for initialization
	void Start ()
    {
        walk_remaining = 0;
        moving_status = status.IDLE;
        direction = face_direction.DOWN;
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
        /* Set Movement Mode */
        SetMovement();

        /* Move */
        UpdateMovement();
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
            if (Input.GetKey(KeyCode.Z))
            {
                
            }
        }
    }
}
