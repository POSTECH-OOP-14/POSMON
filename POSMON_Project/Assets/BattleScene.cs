
using UnityEngine;
using System.Collections;

    
public class BattleScene {
    int menuState;
    AttributeMatrix Attribute = new AttributeMatrix();


    //give animation to object
    public void moveAnimation(Vector3 a)
    { 
    
    }


    //If player attack, calculate damage
    public int BattleDamageCalculate(SkillInfo usedSkill, Student attacker, Student defenser, int[] _battleTempStat)
    {
        int attributeRelation1 = (int)attacker.getStuIndex() / 10;
        int attributeRelation2 = (int)defenser.getStuIndex() / 10;

        double AttackRate = 1;   //find attack is special attack or normal attack
        double damage = 0;       //return total damage that defenser have to take
        float AttributeCal = 1; //Calculate damage Changed by AttributeCal
        float[] battleTempStat = new float[12];

        if(usedSkill.retType() == attributeRelation1) //if Attacker type and used Skill type is same, get bonus damage 
        {AttributeCal *= 1.5f;}
        
        int RandomRate  = (int)Random.Range(217,255)*100/255;    //make random damage;

        for (int i = 0; i < 12; i++)
        {
            if (_battleTempStat[i] > 0)
                battleTempStat[i] = (2 + _battleTempStat[i]) / (float)(_battleTempStat[i]);
            else if (_battleTempStat[i] < 0)
                battleTempStat[i] = (float)(2) / (2 + _battleTempStat[i]);
            else
                battleTempStat[i] = 1;
        }

        if (usedSkill.retAtkType() == 0)
        {  //calculate the damage of 
            AttackRate = (usedSkill.retDamage())*(attacker.retStuStat(0))*battleTempStat[0]/(defenser.retStuStat(2)/battleTempStat[8]);
        }else        {
            AttackRate = (usedSkill.retDamage())*(attacker.retStuStat(1))*battleTempStat[1]/(defenser.retStuStat(3)/battleTempStat[9]);
        }

        damage =(((   
                    (
                        (attacker.retStuStat(6)*2/5)+2  //attackers level
                    )
                    *AttackRate/50                          //used skill damage * attacker,defensers ability
                )+2)
                *RandomRate/100                             //random damage
                *AttributeCal*Attribute.attribute[attributeRelation1,attributeRelation2] //attribute;
                );
 
        return (int)damage;
    }

    //calculate who will gonna act first at battle,
    //normally this will determine by speed of 
    //intput 0 is someone changed student or use item. its player act first.
    public int CalFirstGo(double mySpeed, double EnemySpeed, int[] battletempStat)
    {
        double mySpeedCal=1.0f;
        double enemySpeedCal = 1.0f;


        //calculate the change by tempStat;
        if (battletempStat[4] < 1)
            mySpeedCal = mySpeedCal * 2 / (battletempStat[4] + 2);
        else
            mySpeedCal = mySpeedCal * (2 + battletempStat[4]) / 2;
        if (battletempStat[4] < 1)
            enemySpeedCal = enemySpeedCal * 2 / (battletempStat[4] + 2);
        else
            enemySpeedCal = enemySpeedCal * (2 + battletempStat[4]) / 2;


        if (mySpeed == 0)
            return 0;
        else if (EnemySpeed == 0)
            return 1;
        else{
            if (mySpeed * mySpeedCal >= EnemySpeed * enemySpeedCal)
                return 1;
            else
                return 0;
        }
    }
    public Student changeStudent(Student[] a)
    {
        int repeat = 0;
        int i = 0;
        while (repeat == 0)
        {
            while (i == 0)
            {
                if (Input.GetKey(KeyCode.Alpha1))
                    i = 1;
                if (Input.GetKey(KeyCode.Alpha2))
                    i = 2;
                if (Input.GetKey(KeyCode.Alpha3))
                    i = 3;
                if (Input.GetKey(KeyCode.Alpha4))
                    i = 4;
                if (Input.GetKey(KeyCode.Alpha5))
                    i = 5;
                if (Input.GetKey(KeyCode.Alpha6))
                    i = 6;
            }
            if (a[i] != null || a[i].retStuStatus() != status.faint)
                repeat = 1;
        }
        return a[i];
    }

    public void checkBattleTurnEndEvent(Student stu)
    {
        if (stu.retStuStatus() == status.poison)
            stu.getDamage((int)(stu.retStuStat(5)*5.0/8));
    }

    //student doesn't move on this turn when return 1(by sleep), 2(by paralyze). 0 is normal.
    public int checkBattleTurnStartEvent(Student stu)
    {
        if (stu.retStuStatus() == status.sleep)
        {
            float value = Random.value;
            if (value <= 0.2f)    {
                stu.giveAStatus(status.none);
                return 0;
            }
            else {
                return 1;
            }
        }
        if (stu.retStuStatus() == status.paralysis)
        {
            float value = Random.value;
            if (value <= 0.25f)
            {
                return 0;
            }
            else
            {
                return 2;
            }
        }
        return 0;
    }
}
