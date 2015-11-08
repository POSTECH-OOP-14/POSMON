using UnityEngine;
using System.Collections;

public class CharacterKeyInput : MonoBehaviour
{

    enum status
    {
        IDLE,
        MOVE_LEFT,
        MOVE_RIGHT,
        MOVE_DOWN,
        MOVE_UP
    };

    private const int WALK_SLICE = 5;
    private const float WALK_DIST = 1.0f / WALK_SLICE;

    /* movement information */
    private int walk_remaining = 0;
    private status moving_status = status.IDLE;

    /* inventory information */
    private int[] inventory = new int[256];

	// Use this for initialization
	void Start ()
    {
        /* initialize inventory */
        for (int i = 0; i < 256; i++)
            inventory[i] = 0;        	
	}

    void SetMovement()
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

    void UpdateMovement()
    {
        switch (moving_status)
        {
            case status.MOVE_LEFT:
                transform.position = new Vector2(transform.position.x - WALK_DIST, transform.position.y);
                walk_remaining--;
                break;

            case status.MOVE_RIGHT:
                transform.position = new Vector2(transform.position.x + WALK_DIST, transform.position.y);
                walk_remaining--;
                break;

            case status.MOVE_DOWN:
                transform.position = new Vector2(transform.position.x, transform.position.y - WALK_DIST);
                walk_remaining--;
                break;

            case status.MOVE_UP:
                transform.position = new Vector2(transform.position.x, transform.position.y + WALK_DIST);
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
        /* Check Collision */

        /* Set Movement Mode */
        SetMovement();

        /* Move */
        UpdateMovement();

        /* Get Function Key Input */
        if (Input.GetKey(KeyCode.Z))
        {
        }
        else if (Input.GetKey(KeyCode.X))
        {
        }
	}
}
