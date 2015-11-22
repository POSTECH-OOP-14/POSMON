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
    private string department;//학과
    private int[] stat=new int[6];//능력치배열
    private int index;//포스몬의 번호
    private status st;//상태(정상, 마비, 잠, 독)
    private int level;
    private int exp;
    private int HP;

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

    void setExp(int exp)
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
}

