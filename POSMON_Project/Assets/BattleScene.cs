
using UnityEngine;
using System.Collections;

    
public class BattleScene {
    int menuState;
    AttributeMatrix Attribute = new AttributeMatrix();
    // Use this for initialization


    

    //give animation to object
    public void moveAnimation(Vector3 a)
    { 
    
    }


    //If player attack, calculate damage
    public int BattleDamageCalculate(SkillInfo usedSkill, Student attacker, Student defenser, int[] _battleTempStat)
    {
        float AttackRate = 1;   //find attack is special attack or normal attack
        float damage = 0;       //return total damage that defenser have to take
        float AttributeCal = 1; //Calculate damage Changed by AttributeCal
        float[] battleTempStat = new float[12];

        if(usedSkill.retType() == attacker.retStuDept()) //if Attacker type and used Skill type is same, get bonus damage 
        {AttributeCal *= 1.5f;}
        
        int RandomRate  = (int)Random.Range(217,255)*100/255;    //make random damage;

        if (usedSkill.retAtkType() == 0)
        {  //calculate the damage of 
            AttackRate = (usedSkill.retDamage())*(attacker.retStuStat(0))/(defenser.retStuStat(2));
        }else        {
            AttackRate = (usedSkill.retDamage())*(attacker.retStuStat(1))/(defenser.retStuStat(3));
        }

        damage =(((   
                    (
                        (attacker.retStuStat(6)*2/5)+2  //attackers level
                    )
                    *AttackRate/50                          //used skill damage * attacker,defensers ability
                )+2)
                *RandomRate/100                             //random damage
                *AttributeCal*Attribute.attribute[attacker.retStuDept(),defenser.retStuDept()] //attribute;
                );

          
        return (int)damage;
    }

    //calculate who will gonna act first at battle,
    //normally this will determine by speed of 
    //intput 0 is someone changed student or use item,
    public int CalFirstGo(int mySpeed, int EnemySpeed, int[] battletempStat)
    {
        float mySpeedCal=1.0f;
        float enemySpeedCal = 1.0f;


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

}
