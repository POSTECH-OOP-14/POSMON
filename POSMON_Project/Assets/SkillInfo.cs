﻿using UnityEngine;
using System.Collections;

public class SkillInfo {
    private string skillName;
    private int number;
    private int damage;
    private int type;
    private int AtkType;
    private int[] effect;
    private status giveStatus;
    
    
    //define information of skill. a list of skill information. could be dirty and not effective but is esay to know
    public SkillInfo(int _number, string _skillName, int _damage, int _type, int _atktype, int[] _effect, status _status)
    {
        skillName = _skillName;
        number = _number;
        damage = _damage;
        type = _type;
        AtkType = _atktype;
        effect = _effect;
        giveStatus = _status;
    }

    public SkillInfo(int _number)
    {
        SkillList asg = new SkillList();
        skillName = asg.SkillArray[_number].retSkillName();
        number = _number;
        damage = asg.SkillArray[_number].retDamage();
        type = asg.SkillArray[_number].retType();
        AtkType = asg.SkillArray[_number].retAtkType();
        effect = asg.SkillArray[_number].retEffect();
        giveStatus = asg.SkillArray[_number].retGiveStatus();
    }
    public string retSkillName() { return this.skillName; }
    public int retNumber() { return this.number; }
    public int retDamage() { return this.damage; }
    public int retType() { return this.type;}
    public int retAtkType() { return this.AtkType; }
    public int[] retEffect() { return this.effect; }
    public status retGiveStatus() { return this.giveStatus; }
}
