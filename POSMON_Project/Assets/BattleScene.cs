using UnityEngine;
using System.Collections;

public class BattleScene : MonoBehaviour {
    int menuState;
    StudentInfo Mine;
    StudentInfo Enemy;
	int[,] attribute = new int[10,10];
    // Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    
    //draw game field
    void drawStudentInfo() 
    {
        
    }

    void drawMenuState(int i)
    { 
        
    }
    void moveAnimation(Vector3 a)
    { 
    
    }
    int BattleDamageCalculate(SkillInfo usedSkill, StudentInfo attacker, StudentInfo defenser)
    {
        float AttackRate = 1;
        float damage = 0;
        float AttributeCal = 1;
        if(usedSkill.type == attacker.type)
        {AttributeCal = 1.5f;}
        int RandomRate  = Random.Range(217,255)*100/255;
        if(usedSkill.AtkType == 0)       {
            AttackRate = (usedSkill.damage)*(attacker.retStudentAttack())/(defenser.retStudentDefense());
        }else        {
            AttackRate = (usedSkill.damage)*(attacker.retStudentSpecialAttack())/(defenser.retStudentSpecialDefense());
        }
        damage =(((   
                    (
                        (attacker.retStudentLevel()*2/5)+2
                    )
                    *usedSkill.damage*AttackRate/50
                )+2)
                *RandomRate/100*AttributeCal*attribute[attacker.type,defenser.type]
                );

          
        return 0;
    }
}
