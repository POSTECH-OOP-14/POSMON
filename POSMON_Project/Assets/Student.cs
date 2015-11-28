using UnityEngine;
using System.Collections;

public enum stu_no
{
    MATH1 = 0, MATH2,
    PHYS1 = 10, PHYS2,
    CSE1 = 20, CSE2,
    IME1 = 30, IME2,
    CITE1 = 40, CITE2,
    CHEM1 = 50, CHEM2,
    CME1 = 60, CME2,
    MSE1 = 70, MSE2,
    BIO1 = 80, BIO2,
    EE1 = 90, EE2,
    ME1 = 100, ME2
}


public class Student
{
    private string[] skill = new string[4];//스킬배열
    private int[] skillNumberList = new int[4]; //tempory skill list
    private string department;//학과
    private int[] stat = new int[6];//능력치배열
    private int index;//포스몬의 번호
    private status st;//상태(정상, 마비, 잠, 독)
    private int level;
    private int exp;
    private int HP;
    private int MAXHP;
    private int skillNum;//스킬 개수
    private Texture StuImage;

    public Student(stu_no n)
    {
        switch (n)
        {
            case stu_no.MATH1:
                {
                    skill[4] = "s1,s2,s3,s4";
                    department = "수학과";
                    st = status.none;
                    level = 1;
                    exp = 0;
                    HP = 15;
                    MAXHP = 15;
                    skillNum = 2;
                    break;
                }
        }
    }

    public int getMAXHP()
    {
        return MAXHP;
    }

    public void setIndex(int index)
    {
        this.index = index;
    }

    public void levelUP()
    {
        level++;
    }

    public void setExp(int exp)
    {
        this.exp += exp;
        if (this.exp > 100)
        {
            levelUP();
            this.exp -= 100;
        }
    }

    public bool setHP(int amount)
    {
        if (HP == getMAXHP())
            return false;
        HP += amount;
        if (HP > getMAXHP())
            HP = getMAXHP();
        if (amount == (int)status.all)
            HP = getMAXHP();
        return true;
    }

    public int getHP()
    {
        return HP;
    }

    public bool setStatus(status st)
    {
        if (this.st == status.paralysis && st == status.paralysis)
        {
            st = status.none;
            return true;
        }
        if (this.st == status.sleep && st == status.sleep)
        {
            st = status.none;
            return true;
        }
        if (this.st == status.poison && st == status.poison)
        {
            st = status.none;
            return true;
        }
        if (st == status.all)
        {
            st = status.none;
            return true;
        }
        return false;
    }


    public bool addSkill(Item it)//skill이 추가되었으면 true, 아니면 false
    {
        if (skillNum == 4)
            return false;
        else
        {
            this.skill[++skillNum] = it.getName();
            return true;
        }
    }

    public bool changeStatus(Item it)
    {
        int amount = it.getAmount();
        status st_it = it.getStatus();

        if (amount > 0)//체력을 회복시키는 아이템
            return setHP(amount);
        else//포스몬의 상태를 바꾸어주는 아이템
            return setStatus(st_it);
    }

    public bool capture(Item it)
    {
        if (it.getPossibility() / getHP() > 10)
            return true;
        else
            return false;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //song Si Ho 작성

    //return the index of student. could be change to return image of stu.
    public int retStuIndex()
    {
        return this.index;
    }

    public int retStuDept()
    {
        return this.index / 10;
    }

    //return stat of student
    //0 is atk, 1 is special atk, 2 is def, 3 is special def, 4 is speed, 5 is hp
    //6 is level
    public int retStuStat(int i)
    {
        if (i < 6)
        {
            int a;
            a = this.stat[i] / this.level * 10;
            return a;
        }
        else
            return this.level;
    }
    //set status 
    public int giveAStatus(status state)
    {
        if (this.st == status.none)
            this.st = state;
        else if (state == status.faint)
            this.st = state;
        else
        {
            //this state, student cannot change its state
            return 1;
        }
        return 0;
    }
    //return stuStatus
    public status retStuStatus()
    {
        return this.st;
    }

    //give damage to student
    public int getDamage(int damage)
    {
        HP -= damage;
        if (HP < 0)
        {
            HP = 0;

            return 1;
        }

        return 0;
    }
    public int getExp()
    {
        return this.exp;
    }

    public int getLevel()
    {
        return this.level;
    }

    public string getDept()
    {
        return this.department;
    }

    public int[] retSkillList()
    {
        return skillNumberList;
    }
}

