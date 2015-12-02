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
    private SkillList list= new SkillList();
    private SkillInfo[] skill = new SkillInfo[4];//스킬배열
    private int[] skillNumberList = new int[4]; //tempory skill list
    private string department;//학과
    private double[] stat = new double[6];//능력치배열
    private int index;//포스몬의 번호
    private status st;//상태(정상, 마비, 잠, 독)
    private int level;
    private int exp;
    private double HP;
    private double MAXHP;
    private int skillNum;//스킬 개수
    private Texture StuImage;

    public Student(stu_no n)
    {
        switch (n)
        {
            case stu_no.MATH1:
                {
                    skill = new SkillInfo[] { list.retSkillInfo(18), list.retSkillInfo(0), null, null };
                    department = "수학과";
                    stat = new double[] { 30, 5, 10, 15, 25, 15 };
                    st = status.none;
                    level = 1;
                    exp = 0;
                    MAXHP = stat[5]*5;
                    HP = MAXHP;
                    skillNum = 2;
                    index = (int)n;
                    break;
                }
            case stu_no.MATH2:
                {
                    skill = new SkillInfo[] { list.retSkillInfo(19), list.retSkillInfo(3), null, null };
                    department = "수학과";
                    stat = new double[] { 35, 10, 25, 5, 15, 10 };
                    st = status.none;
                    level = 1;
                    exp = 0;
                    MAXHP = stat[5] * 5;
                    HP = MAXHP;
                    skillNum = 2;
                    index = (int)n;
                    break;
                }
            case stu_no.PHYS1:
                {
                    skill = new SkillInfo[] { list.retSkillInfo(0), list.retSkillInfo(11), null, null };
                    department = "물리학과";
                    stat = new double[] { 15, 15, 5, 35, 5, 25 };
                    st = status.none;
                    level = 1;
                    exp = 0;
                    MAXHP = stat[5] * 5;
                    HP = MAXHP;
                    skillNum = 2;
                    index = (int)n;
                    break;
                }
            case stu_no.PHYS2:
                {
                    skill = new SkillInfo[] { list.retSkillInfo(1), list.retSkillInfo(16), null, null };
                    department = "물리학과";
                    stat = new double[] { 10,30,25,5,5,25 };
                    st = status.none;
                    level = 1;
                    exp = 0;
                    MAXHP = stat[5] * 5;
                    HP = MAXHP;
                    skillNum = 2;
                    index = (int)n;
                    break;
                }
            case stu_no.CHEM1:
                {
                    skill = new SkillInfo[] { list.retSkillInfo(2), list.retSkillInfo(12), null, null };
                    department = "화학과";
                    stat = new double[] { 10,30,25,5,5,25 };
                    st = status.none;
                    level = 1;
                    exp = 0;
                    MAXHP = stat[5] * 5;
                    HP = MAXHP;
                    skillNum = 2;
                    index = (int)n;
                    break;
                }
            case stu_no.CHEM2:
                {
                    skill = new SkillInfo[] { list.retSkillInfo(3), list.retSkillInfo(8), null, null };
                    department = "화학과";
                    stat = new double[] { 15,25,30,10,10,10 };
                    st = status.none;
                    level = 1;
                    exp = 0;
                    MAXHP = stat[5] * 5;
                    HP = MAXHP;
                    skillNum = 2;
                    index = (int)n;
                    break;
                }
            case stu_no.CME1:
                {
                    skill = new SkillInfo[] { list.retSkillInfo(4), list.retSkillInfo(20), null, null };
                    department = "화학공학과";
                    stat = new double[] { 10,10,15,20,20,25 };
                    st = status.none;
                    level = 1;
                    exp = 0;
                    MAXHP = stat[5] * 5;
                    HP = MAXHP;
                    skillNum = 2;
                    index = (int)n;
                    break;
                }
            case stu_no.CME2:
                {
                    skill = new SkillInfo[] { list.retSkillInfo(5), list.retSkillInfo(21), null, null };
                    department = "화학공학과";
                    stat = new double[] { 5,15,20,15,20,25 };
                    st = status.none;
                    level = 1;
                    exp = 0;
                    MAXHP = stat[5] * 5;
                    HP = MAXHP;
                    skillNum = 2;
                    index = (int)n;
                    break;
                }
            case stu_no.BIO1:
                {
                    skill = new SkillInfo[] { list.retSkillInfo(6), list.retSkillInfo(13), null, null };
                    department = "생명공학과";
                    stat = new double[] { 15,10,20,15,10,30 };
                    st = status.none;
                    level = 1;
                    exp = 0;
                    MAXHP = stat[5] * 5;
                    HP = MAXHP;
                    skillNum = 2;
                    index = (int)n;
                    break;
                }
            case stu_no.BIO2:
                {
                    skill = new SkillInfo[] { list.retSkillInfo(7), list.retSkillInfo(17), null, null };
                    department = "생명공학과";
                    stat = new double[] { 10,15,15,20,5,35 };
                    st = status.none;
                    level = 1;
                    exp = 0;
                    MAXHP = stat[5] * 5;
                    HP = MAXHP;
                    skillNum = 2;
                    index = (int)n;
                    break;
                }
            case stu_no.MSE1:
                {
                    skill = new SkillInfo[] { list.retSkillInfo(8), list.retSkillInfo(1), null, null };
                    department = "신소재공학과";
                    stat = new double[] { 10,25,10,25,15,15 };
                    st = status.none;
                    level = 1;
                    exp = 0;
                    MAXHP = stat[5] * 5;
                    HP = MAXHP;
                    skillNum = 2;
                    index = (int)n;
                    break;
                }
            case stu_no.MSE2:
                {
                    skill = new SkillInfo[] { list.retSkillInfo(9), list.retSkillInfo(15), null, null };
                    department = "신소재공학과";
                    stat = new double[] { 10,20,15,20,20,15 };
                    st = status.none;
                    level = 1;
                    exp = 0;
                    MAXHP = stat[5] * 5;
                    HP = MAXHP;
                    skillNum = 2;
                    index = (int)n;
                    break;
                }
            case stu_no.ME1:
                {
                    skill = new SkillInfo[] { list.retSkillInfo(10), list.retSkillInfo(16), null, null };
                    department = "기계공학과";
                    stat = new double[] { 5,30,10,30,10,15 };
                    st = status.none;
                    level = 1;
                    exp = 0;
                    MAXHP = stat[5] * 5;
                    HP = MAXHP;
                    skillNum = 2;
                    index = (int)n;
                    break;
                }
            case stu_no.ME2:
                {
                    skill = new SkillInfo[] { list.retSkillInfo(11), list.retSkillInfo(3), null, null };
                    department = "기계공학과";
                    stat = new double[] { 10,30,5,30,5,20 };
                    st = status.none;
                    level = 1;
                    exp = 0;
                    MAXHP = stat[5] * 5;
                    HP = MAXHP;
                    skillNum = 2;
                    index = (int)n;
                    break;
                }
            case stu_no.CSE1:
                {
                    skill = new SkillInfo[] { list.retSkillInfo(12), list.retSkillInfo(14), null, null };
                    department = "컴퓨터공학과";
                    stat = new double[] { 15,20,15,15,20,15 };
                    st = status.none;
                    level = 1;
                    exp = 0;
                    MAXHP = stat[5] * 5;
                    HP = MAXHP;
                    skillNum = 2;
                    index = (int)n;
                    break;
                }
            case stu_no.CSE2:
                {
                    skill = new SkillInfo[] { list.retSkillInfo(13), list.retSkillInfo(19), null, null };
                    department = "컴퓨터공학과";
                    stat = new double[] { 20,15,15,20,15,15 };
                    st = status.none;
                    level = 1;
                    exp = 0;
                    MAXHP = stat[5] * 5;
                    HP = MAXHP;
                    skillNum = 2;
                    index = (int)n;
                    break;
                }
            case stu_no.CITE1:
                {
                    skill = new SkillInfo[] { list.retSkillInfo(14), list.retSkillInfo(2), null, null };
                    department = "창의IT융합공학과";
                    stat = new double[] { 5,5,40,40,5,5 };
                    st = status.none;
                    level = 1;
                    exp = 0;
                    MAXHP = stat[5] * 5;
                    HP = MAXHP;
                    skillNum = 2;
                    index = (int)n;
                    break;
                }
            case stu_no.CITE2:
                {
                    skill = new SkillInfo[] { list.retSkillInfo(15), list.retSkillInfo(5), null, null };
                    department = "창의IT융합공학과";
                    stat = new double[] { 10,5,35,35,10,5 };
                    st = status.none;
                    level = 1;
                    exp = 0;
                    MAXHP = stat[5] * 5;
                    HP = MAXHP;
                    skillNum = 2;
                    index = (int)n;
                    break;
                }
            case stu_no.IME1:
                {
                    skill = new SkillInfo[] { list.retSkillInfo(16), list.retSkillInfo(6), null, null };
                    department = "산업경영공학과";
                    stat = new double[] { 15,15,5,25,20,20 };
                    st = status.none;
                    level = 1;
                    exp = 0;
                    MAXHP = stat[5] * 5;
                    HP = MAXHP;
                    skillNum = 2;
                    index = (int)n;
                    break;
                }
            case stu_no.IME2:
                {
                    skill = new SkillInfo[] { list.retSkillInfo(17), list.retSkillInfo(21), null, null };
                    department = "산업경영공학과";
                    stat = new double[] { 10,15,10,20,25,20 };
                    st = status.none;
                    level = 1;
                    exp = 0;
                    MAXHP = stat[5] * 5;
                    HP = MAXHP;
                    skillNum = 2;
                    index = (int)n;
                    break;
                }
            case stu_no.EE1:
                {
                    skill = new SkillInfo[] { list.retSkillInfo(20), list.retSkillInfo(2), null, null };
                    department = "전기전자공학과";
                    stat = new double[] { 10,25,5,20,20,20 };
                    st = status.none;
                    level = 1;
                    exp = 0;
                    MAXHP = stat[5] * 5;
                    HP = MAXHP;
                    skillNum = 2;
                    index = (int)n;
                    break;
                }
            case stu_no.EE2:
                {
                    skill = new SkillInfo[] { list.retSkillInfo(21), list.retSkillInfo(12), null, null };
                    department = "전기전자공학과";
                    stat = new double[] { 10,20,10,20,15,25 };
                    st = status.none;
                    level = 1;
                    exp = 0;
                    MAXHP = stat[5] * 5;
                    HP = MAXHP;
                    skillNum = 2;
                    index = (int)n;
                    break;
                }
        }
    }

    public double getMAXHP()
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
        for (int i = 0; i < 6; i++)
        {
            stat[i] = stat[i] * 1.1;
        }
        MAXHP = stat[6] * 5;
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

    public bool setHP(double amount)
    {
        if (HP == getMAXHP())
            return false;
        /* heal */
        if (amount > 0 && amount < 1)
            HP += getMAXHP() * amount;
        else
            HP += amount;
        if (HP > getMAXHP())
            HP = getMAXHP();
        if (amount == (double)status.all)
            HP = getMAXHP();
        return true;
    }

    public double getHP()
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
            this.skill[++skillNum] = it.getSkillInfo();
            return true;
        }
    }

    public bool changeStatus(Item it)
    {
        double amount = it.getAmount();
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

    public stu_no getStuIndex()
    {
        int i = this.index;
        return (stu_no)index;
    }
    //return stat of student
    //0 is atk, 1 is special atk, 2 is def, 3 is special def, 4 is speed, 5 is hp
    //6 is level
    public double retStuStat(int i)
    {
        if (i < 6)
        {
            double a;
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
            this.giveAStatus(status.faint);
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

    public string retStudentName()
    {
        string a = "";
        a = this.department +" " +this.index+ "번 학생";
        return a;
    }
}

