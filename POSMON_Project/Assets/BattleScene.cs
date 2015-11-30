
using UnityEngine;
using System.Collections;

    
public class BattleScene {
    int menuState;
    AttributeMatrix Attribute;
    // Use this for initialization


    

    //give animation to object
    public void moveAnimation(Vector3 a)
    { 
    
    }


    //If player attack, calculate damage
    public int BattleDamageCalculate(SkillInfo usedSkill, StudentInfo attacker, StudentInfo defenser)
    {
        float AttackRate = 1;   //find attack is special attack or normal attack
        float damage = 0;       //return total damage that defenser have to take
        float AttributeCal = 1; //Calculate damage Changed by AttributeCal

        if(usedSkill.retType() == attacker.type) //if Attacker type and used Skill type is same, get bonus damage 
        {AttributeCal *= 1.5f;}
        
        int RandomRate  = Random.Range(217,255)*100/255;    //make random damage;
        
        if(usedSkill.retAtkType() == 0)       {
            AttackRate = (usedSkill.retDamage())*(attacker.retStudentAttack())/(defenser.retStudentDefense());
        }else        {
            AttackRate = (usedSkill.retDamage())*(attacker.retStudentSpecialAttack())/(defenser.retStudentSpecialDefense());
        }

        damage =(((   
                    (
                        (attacker.retStudentLevel()*2/5)+2  //attackers level
                    )
                    *AttackRate/50                          //used skill damage * attacker,defensers ability
                )+2)
                *RandomRate/100                             //random damage
                *AttributeCal*Attribute.attribute[attacker.type,defenser.type] //attribute;
                );

          
        return (int)damage;
    }
}
