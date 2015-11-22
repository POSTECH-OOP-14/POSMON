/*
 using UnityEngine;
using System.Collections;

public enum item_no
{
    SKILL1 = 1, SKILL2, SKILL3, SKILL4, SKILL5, SKILL6, SKILL7, SKILL8, SKILL9,
    CURE1 = 100, CURE2, CURE3, CURE4, CURE5, CURE6, CURE7, CURE8, CURE9,
    CAPTURE1 = 1000, CAPTURE2, CAPTURE3, CAPTURE4, CAPTURE5, CAPTURE6, CAPTURE7, CAPTURE8, CAPTURE9,
    QUEST1 = 10000, QUEST2, QUEST3, QUEST4, QUEST5, QUEST6, QUEST7, QUEST8, QUEST9
};

public enum status { paralysis, sleep, poison, none, all };

public enum type { skill, cure, capture, quest };

public class Item
{
    private int index;//아이템 번호
    private bool battle;//전투 중에 사용가능한 아이템이면 true, 아니면 false
    private int price;//아이템 가격
    private string name;//아이템 이름
    private int amount;//회복 아이템이 회복시키는 HP
    private status st;//회복 아이템이 회복시키는 상태
    private type type;//아이템 종류

    public Item(item_no n)
    {
        switch (n)
        {
            case item_no.SKILL1:
                {
                    name = "기술머신01";
                    index = (int)n;
                    price = 200;
                    battle = false;
                    type = type.skill;
                    break;
                }
            case item_no.SKILL2:
                {
                    name = "기술머신02";
                    index = (int)n;
                    price = 250;
                    battle = false;
                    type = type.skill;
                    break;
                }
            case item_no.SKILL3:
                {
                    name = "기술머신03";
                    index = (int)n;
                    price = 350;
                    battle = false;
                    type = type.skill;
                    break;
                }
            case item_no.SKILL4:
                {
                    name = "기술머신04";
                    index = (int)n;
                    price = 400;
                    battle = false;
                    type = type.skill;
                    break;
                }
            case item_no.CURE1:
                {
                    name = "나무열매";
                    index = (int)n;
                    price = 50;
                    amount = 5;
                    battle = true;
                    type = type.cure;
                    st = status.none;
                    break;
                }
            case item_no.CURE2:
                {
                    name = "학식";
                    index = (int)n;
                    price = 120;
                    amount = 7;
                    battle = true;
                    type = type.cure;
                    st = status.none;
                    break;
                }
            case item_no.CURE3:
                {
                    name = "초코에몽";
                    index = (int)n;
                    price = 200;
                    amount = 20;
                    battle = true;
                    type = type.cure;
                    st = status.none;
                    break;
                }
            case item_no.CURE4:
                {
                    name = "마비";
                    index = (int)n;
                    price = 400;
                    amount = 0;
                    st = status.paralysis;
                    battle = true;
                    type = type.cure;
                    break;
                }
            case item_no.CAPTURE1:
                {
                    break;
                }
            case item_no.QUEST1:
                {
                    break;
                }

        }

    }

    public int getPrice()
    {
        return price;
    }

    public string getName()
    {
        return name;

    }
    public type getType()
    {
        return type;
    }
    public int getAmount()
    {
        return amount;
    }
    public status getStatus()
    {
        return st;
    }

    public void useItem(Item it, Student s)
    {
        if (it.getType() == type.skill)
        {
            s.changeSkill(it);
        }
        else if (it.getType() == type.cure)
        {
            s.changeStatus(it);
        }
        else if (it.getType() == type.capture)
        {

        }
        else
        {

        }
    }

    public int getIndex()
    {
        return index;
    }

    public int chooseIndex()//키입력을 통해 고른 아이템의 번호를 반환
    {
        int ind = -1;//아이템 고르는 것을 취소하면 -1 반환
        int row = 0;
        int col = 0;

        while (!Input.GetKey(KeyCode.Z) && !Input.GetKey(KeyCode.X))
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                row++;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                row--;
            }
            if (Input.GetKey(KeyCode.X))
            {
                return ind;
            }
        }
        row = row % 4;
        while (!Input.GetKey(KeyCode.Z) && !Input.GetKey(KeyCode.X))
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                col++;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                col--;
            }
            if (Input.GetKey(KeyCode.X))
            {
                return ind;
            }
        }
        col = col % 10;
        ind = row * 10 + col;

        return ind;
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
*/