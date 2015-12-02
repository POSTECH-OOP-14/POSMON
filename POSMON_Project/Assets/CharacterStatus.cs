using UnityEngine;
using System.Collections;

public class CharacterStatus : MonoBehaviour
{
    private string text = "아이템 설명";
    private string text2;

    public AudioClip WarningSound;
    public AudioClip SuccessSound;

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
    private int money = 10000;

    /**** methods related to inventory ****/
    public void setInventory(Item item, int index)
    {
        if (inventory[index] != null)
            return;
        else
            inventory[index] = item;
    }

    public bool removeItemToInventory(Item item)
    {
        bool remove = false;
        for (int i = 0; i < 256; i++)
        {
            if (inventory[i] == item)
            {
                for (int j = i; j < 255; j++)
                {
                    inventory[j] = inventory[j + 1];
                    remove = true;
                }
            }
        }
        return remove;
    }

    public bool addItemToInventory(Item item)
    {
        bool add = false;
        for (int i = 0; i < 256; i++)
        {
            if (inventory[i] == null && !add)
            {
                inventory[i] = item;
                if (item.getitem_Amount() <= 0)
                    item.additem_Amount(1);
                return true;
            }
            else if (inventory[i].getName() == item.getName())
            {
                inventory[i].additem_Amount(1);
                add = true;
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

    public int getMoney()
    {
        return money;
    }

    public void setMoney(int _money)
    {
        money = _money;
    }

    public void addMoney(int addition)
    {
        money += addition;
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
    public bool addQuest(Quest q)
    {
        if (quest_list[q.getHostNPCNumber()] == null)
        {
            quest_list[q.getHostNPCNumber()] = q;
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

    public bool deleteQuest(int hostnum)
    {
        for (int i = 0; i < 100; i++)
        {
            if (quest_list[i] != null)
            {
                if (quest_list[i].getHostNPCNumber() == hostnum)
                {
                    quest_list[i] = null;
                    return true;
                }
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
        student_list[0] = new Student(stu_no.BIO1);
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
            gameObject.transform.position = new Vector3(Mathf.Round(gameObject.transform.position.x),
                                            Mathf.Round(gameObject.transform.position.y),
                                            0f);
            SetMovement();
        }
    }
	
	// Update is called once per frame
    private bool show = false;
	void Update ()
    {
        if (blocked != true)
        {
            /* Set Movement Mode */
            SetMovement();

            /* Move */
            UpdateMovement();
            if (Input.GetKeyDown("i"))
                show = !show;
        }
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("[Player] Collision Occur");

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

    public bool getBlocked()
    {
        return blocked;
    }

    public void setBlocked(bool block)
    {
        blocked = block;
    }

    /* Character Status Information */
    private string[] toolbar_str = { "Character", "Student", "Item" };
    private int toolbarint = 0;
    private Vector2 scroll = Vector2.zero;
    private string[] stu_toolbar = new string[6];
    private int stu_toolbarint = 0;
    private int item_gridint = 0;
    private string[] item_grid;
    private int k = 0;
    private bool use_item = false;
    void UseItem(int windowID)
    {
        for (int j = 0; student_list[j] != null; j++)
        {
            stu_toolbar[j] = student_list[j].getDept();
        }
        stu_toolbarint = GUI.SelectionGrid(new Rect(10, 40, (Screen.width / 4), 60), stu_toolbarint, stu_toolbar, 2);
        if (GUI.Button(new Rect(10, 110, (Screen.width / 8), 25), "Use"))
        {
            if (inventory[item_gridint].useItem(student_list[stu_toolbarint]) == true)
            {
                GetComponent<AudioSource>().PlayOneShot(SuccessSound);
                inventory[item_gridint].additem_Amount(-1);
            }
            else
                GetComponent<AudioSource>().PlayOneShot(WarningSound);
            use_item = false;
        }
        if (GUI.Button(new Rect((Screen.width / 8)+10, 110, (Screen.width / 8), 25), "Cancel"))
        {
            use_item = false;
        }
    }
    void OnGUI()
    {
        if (show)
        {
            GUIStyle myStyle = new GUIStyle(GUI.skin.box);
            myStyle.fontSize = 25;
            myStyle.font = (Font)Resources.Load("NANUMBARUNGOTHICBOLD", typeof(Font));
            myStyle.alignment = TextAnchor.UpperLeft;
            myStyle.normal.textColor = Color.white;
            GUIStyle itemStyle = new GUIStyle(GUI.skin.box);
            itemStyle.fontSize = 15;
            itemStyle.font = (Font)Resources.Load("NANUMBARUNGOTHICBOLD", typeof(Font));
            itemStyle.alignment = TextAnchor.UpperLeft;
            itemStyle.normal.textColor = Color.white;

            /* Status Menu Init */
            GUI.Box(new Rect(Screen.width / 8, Screen.height / 8, (Screen.width * 6) / 8, (Screen.height * 6) / 7), "Info");
            toolbarint = GUI.Toolbar(new Rect(Screen.width / 6, Screen.height / 5, (Screen.width * 3) / 10, (Screen.height * 3) / 40), toolbarint, toolbar_str);
            scroll = GUI.BeginScrollView(new Rect(Screen.width / 5, (Screen.height / 3) - 10, (Screen.width * 3) / 5, (Screen.height * 3) / 5), scroll, new Rect(0, 0, Screen.width, Screen.height));

            /* case Student */
            if (toolbarint == 0)
            {
                GUI.Box(new Rect(Screen.width / 20, (Screen.height / 20) + 30, (Screen.width * 9) / 10, Screen.height / 2),
                    "소지금 : " + money.ToString() + "\n"
                    , myStyle);
            }
            else if (toolbarint == 1 && student_list[0] != null)
            {
                for (int i = 0; student_list[i] != null; i++)
                {
                    stu_toolbar[i] = student_list[i].getDept();
                }
                stu_toolbarint = GUI.Toolbar(new Rect(0, 0, Screen.width / 2, 30), stu_toolbarint, stu_toolbar);
                for (int i = 0; student_list[i] != null; i++)
                {
                    if (stu_toolbarint == i)
                    {
                        GUI.Box(new Rect(0, 40, (Screen.width * 9) / 10, Screen.height / 2),
                            "Level : " + student_list[i].getLevel().ToString() + "\n" +
                            "HP : " + student_list[i].getHP().ToString() + "/" + student_list[i].getMAXHP().ToString() + "\n" +
                            "Exp : " + student_list[i].getExp().ToString() + "/" + "100" + "\n" +
                            "Status : " + student_list[i].retStuStatus().ToString() + "\n" +
                            "Attack : " + student_list[i].retStuStat(0).ToString() + "\n" +
                            "Special Attack : " + student_list[i].retStuStat(1).ToString() + "\n" +
                            "Defence : " + student_list[i].retStuStat(2).ToString() + "\n" +
                            "Special Defence : " + student_list[i].retStuStat(3).ToString() + "\n" +
                            "Speed : " + student_list[i].retStuStat(4).ToString() + "\n", itemStyle);
                    }
                }
            }
            /* case Item */
            else if (toolbarint == 2 && inventory[0] != null)
            {
                for (k = 0; inventory[k] != null; k++)
                {
                    if (inventory[k].getitem_Amount() <= 0)
                        removeItemToInventory(inventory[k]);

                    if (inventory[k] == null) break;
                    
                }
                if (k == 0) item_grid[0] = null;
                else
                    item_grid = new string[k];


                for (int i = 0; i < k; i++)
                {

                    if (inventory[i].getitem_Amount() > 0)
                    {
                        item_grid[i] = inventory[i].getName();
                        
                    }
                       
                }


                item_gridint = GUI.SelectionGrid(new Rect(0, 0, (Screen.width / 2), 25 * (((k + 1) / 2))), item_gridint, item_grid, 2);

                for (int i = 0; inventory[i] != null; i++)
                {
                    if (item_gridint == i)
                    {
                        if (GUI.Button(new Rect(0, 25 * (((k + 1) / 2)) + 10, 50, 25), "Use"))
                        {
                            /* Use Item, cannot use capture item in character status context */
                            if (inventory[i].getType() != type.capture)
                                use_item = true;
                        }
                        text = "아이템 설명";
                        text2 = "\n";

                        text = text + "\n\n 아이템 이름 : " + inventory[i].getName();
                        text = text + "\n\n 아이템 종류 : ";
                        if (inventory[item_gridint].getType() == type.skill)
                            text = text + "기술 머신";
                        else if (inventory[item_gridint].getType() == type.cure)
                            text = text + "치료 아이템";
                        else if (inventory[item_gridint].getType() == type.capture)
                            text = text + "포획 아이템";
                        text = text + "\n\n 설명 : " + inventory[item_gridint].getInfo();
                        text = text + "\n\n 수량 : " + inventory[item_gridint].getitem_Amount().ToString();
                        if (inventory[item_gridint].getAmount() > 0)
                            text = text + "\n\n 회복량 : " + inventory[item_gridint].getAmount().ToString();

                        
                        GUI.Box(new Rect(60, 25 * (((k + 1) / 2)) + 10, Screen.width - 60, Screen.height - (25 * (((k + 1) / 2)) + 10)),
                            text, itemStyle);
                    }
                }
            }

            GUI.EndScrollView();
            if (use_item)
            {
                GUI.Window(0, new Rect(Screen.width / 2, Screen.height / 2, (Screen.width/4)+20, 140), UseItem, "아이템을 사용할 학생을\n 선택하십시오.");
                
            }
        }
    }
        

}
