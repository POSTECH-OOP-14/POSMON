
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
    public int BattleDamageCalculate(SkillInfo usedSkill, Student attacker, Student defenser, int[] battleTempStat)
    {
        float AttackRate = 1;   //find attack is special attack or normal attack
        float damage = 0;       //return total damage that defenser have to take
        float AttributeCal = 1; //Calculate damage Changed by AttributeCal

        if(usedSkill.type == attacker.retStuDept()) //if Attacker type and used Skill type is same, get bonus damage 
        {AttributeCal *= 1.5f;}
        
        int RandomRate  = (int)Random.Range(217,255)*100/255;    //make random damage;
        
        if(usedSkill.AtkType == 0)       {  //calculate the 
            AttackRate = (usedSkill.damage)*(attacker.retStuStat(0))/(defenser.retStuStat(2));
        }else        {
            AttackRate = (usedSkill.damage)*(attacker.retStuStat(1))/(defenser.retStuStat(3));
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
}
