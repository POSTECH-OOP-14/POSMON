using UnityEngine;
using System.Collections;

public enum stu_no
{
    MATH1=0,MATH2,MATH3,MATH4,
    PHYS1=10,PHYS2,PHYS3,PHYS4,
    CSE1=20,CSE2,CSE3,CSE4,
    IME1=30,IME2,IME3,IME4,
    CITE1=40,CITE2,CITE3,CITE4,
    CHEM1=50,CHEM2,CHEM3,CHEM4,
    CME1=60,CME2,CME3,CME4,
    MSE1=70,MSE2,MSE3,MSE4,
    BIO1=80,BIO2,BIO3,BIO4,
    EE1=90,EE2,EE3,EE4
}

static class Constants
{
    public const int MAXHP = 100;
}

public class Student{
    private string[] skill=new string[4];//스킬배열
    private int[] skillNumberList = new int[4]; //tempory skill list
    private string department;//학과
    //0 is atk, 1 is special atk, 2 is def, 3 is special def, 4 is speed, 5 is hp
    private int[] stat=new int[6];//능력치배열
    private int index;//포스몬의 번호
    private status st;//상태(정상, 마비, 잠, 독)
    private int level;
    private int exp;
    private int HP;
    private Texture StuImage;

    public Student(int[] _skill,string _department,int[] _stat,int _level, int _Hp, Texture image)
    {
     skillNumberList = _skill;
        department = _department;
        stat = _stat;
        level = _level;
        HP = _Hp;
        StuImage = image;
    }


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
                    HP = Constants.MAXHP;
                    break;
                }


        }
    }

    void setIndex(int index)
    {
        this.index = index;
    }

    void levelUP()
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

    void setHP(int amount)
    {
        HP += amount;
        if (HP > Constants.MAXHP)
            HP = Constants.MAXHP;
        if (amount == (int)status.all)
            HP = Constants.MAXHP;
    }

    void setStatus(status st)
    {
        if (this.st == status.paralysis && st == status.paralysis)
            st = status.none;
        if (this.st == status.sleep && st == status.sleep)
            st = status.none;
        if (this.st == status.poison && st == status.poison)
            st = status.none;
        if (st == status.all)
            st = status.none;
    }

    public int getIndexSkill()
    {
        int i=0;
        while (!Input.GetKey(KeyCode.Z))
        {
            if (Input.GetKey(KeyCode.DownArrow))
                i++;
            if(Input.GetKey(KeyCode.UpArrow))
                i--;
            if (Input.GetKey(KeyCode.Z))
                break;
        }
        return i;
    }

    public void changeSkill(Item it) {
        int index_skill = getIndexSkill();
        this.skill[index_skill] = it.getName();
    }

    public void changeStatus(Item it)
    {
        int amount=it.getAmount();
        status st_it = it.getStatus();

        if (amount > 0)//체력을 회복시키는 아이템
            setHP(amount);
        else//포스몬의 상태를 바꾸어주는 아이템
            setStatus(st_it);
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
            a = this.stat[i] *(this.level+5) /15;
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
    public status retStuStatus()    { return this.st; }

    //give damage to student, if student die set faint and return 1. else return 0
    public int getDamage(int damage)
    {
        HP -= damage;
        if (HP < 0)
        {
            HP = 0;
            this.setStatus(status.faint);
            return 1;
        }
        return 0;
    }
    public int getExp() {   return this.exp; }
    public int[] retSkillList() { return skillNumberList;   }
    public int retHp()  {   return this.HP; }
    public int retLevel() { return this.level; }
    public int retMaxHp() { return this.retStuStat(5) * 5; }
}

