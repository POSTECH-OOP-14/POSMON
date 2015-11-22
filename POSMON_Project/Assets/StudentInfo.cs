﻿﻿using UnityEngine;
using System.Collections;

public class StudentInfo
{

    int StudentMaxHealth;
    int StudentCurHealth;
    int StudentLevel;
    int StudentAttack;
    int StudentSpecialAttack;
    int StudentDefense;
    int StudentSpecialDefense;
    int StudentSpeed;
    public SkillInfo[] skillList;
    public int type;
    

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public StudentInfo()
    {
        StudentMaxHealth = 100;
        StudentCurHealth = 100;
        StudentLevel = 10;
        StudentAttack = 50;
        StudentSpecialAttack = 50;
        StudentDefense = 50;
        StudentSpecialDefense = 50;
        StudentSpeed = 50;
        type = 3;
        skillList = new SkillInfo[4];
        skillList[0] = new SkillInfo();
        skillList[1] = new SkillInfo();
        skillList[2] = new SkillInfo();
        skillList[3] = new SkillInfo();

    }
    public int retStudentCurHealth() { return StudentCurHealth; }
    public int retStudentMaxHealth() { return StudentMaxHealth; }
    public int retStudentLevel() { return StudentLevel; }
    public int retStudentAttack() { return StudentAttack; }
    public int retStudentSpecialAttack() { return StudentSpecialAttack; }
    public int retStudentDefense() { return StudentDefense; }
    public int retStudentSpecialDefense() { return StudentSpecialDefense; }
    public int retStudentSpeed() { return StudentSpeed; }
    public int getDamage(int damage) 
    {
        StudentCurHealth -= damage;
        if (StudentCurHealth <= 0)
        {
            StudentCurHealth = 0;
            return 1;
        }
        else
            return 0;
    }
}
