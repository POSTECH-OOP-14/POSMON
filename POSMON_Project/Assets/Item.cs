﻿using UnityEngine;
using System.Collections;

public enum item_no
{
    SKILL1 = 1, SKILL2, SKILL3, SKILL4, SKILL5, SKILL6, SKILL7, SKILL8, SKILL9,
    CURE1 = 100, CURE2, CURE3, CURE4, CURE5, CURE6, CURE7, CURE8, CURE9,
    CAPTURE1 = 1000, CAPTURE2, CAPTURE3, CAPTURE4, CAPTURE5, CAPTURE6, CAPTURE7, CAPTURE8, CAPTURE9,
    QUEST1 = 10000, QUEST2, QUEST3, QUEST4, QUEST5, QUEST6, QUEST7, QUEST8, QUEST9
};

public enum status { paralysis, sleep, poison, none, all,faint };

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
    private double possibility;//포획 아이템 성공률
    private string info;//아이템 설명

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
                    price = 500;
                    amount = 15;
                    battle = true;
                    type = type.cure;
                    st = status.none;
                    info = "폭풍의 언덕의 나무에서 직접 채취한 나무열매! 향긋향긋~ ";
                    break;
                }
            case item_no.CURE2:
                {
                    name = "콜라";
                    index = (int)n;
                    price = 1200;
                    amount = 30;
                    battle = true;
                    type = type.cure;
                    st = status.none;
                    info = "탄산은 역시 콜라! 꿀꺾꿀꺾 끄어어어억";
                    break;
                }
            case item_no.CURE3:
                {
                    name = "몬스터 드링크";
                    index = (int)n;
                    price = 2000;
                    amount = 50;
                    battle = true;
                    type = type.cure;
                    st = status.none;
                    info = "공대생의 필수품! 에너지 드링크!";
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
                    name = "몬스터볼";
                    index = (int)n;
                    price = 200;
                    battle = true;
                    type = type.capture;
                    possibility = 1.5;
                    break;
                }
        }

    }

    public string getInfo()
    {
        return info;
    }

    public double getPossibility()
    {
        return possibility;
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

    public bool useItem(Item it, Student s)
    {
        if (it.getType() == type.skill)
        {
            return true;// s.addSkill(it);
        }
        else if (it.getType() == type.cure)
        {
            return true;// s.changeStatus(it);
        }
        else if (it.getType() == type.capture)
        {
            return true;//s.capture(it);
        }
        return false;
    }

    public int getIndex()
    {
        return index;
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
