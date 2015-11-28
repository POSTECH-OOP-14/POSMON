using UnityEngine;
using System.Collections;

public class SkillInfo {
    private string skillName;
    private int number;
    private int damage;
    private int type;
    private int AtkType;
    private int[] effect;
    private status giveStatus;

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
    public SkillInfo[] SkillList = new SkillInfo[5]
        {
            //
            new SkillInfo(1,"headbutt",50,1,0,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.none),
            new SkillInfo(2,"FireBlast",50,1,0,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.none),
            new SkillInfo(3,"adsf",50,1,0,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.none),
            new SkillInfo(4,"posion",50,1,0,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.poison),
            new SkillInfo(5,"GetOut",50,1,0,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.none)
        };

    public string retSkillName() { return this.skillName; }
    public int retNumber() { return this.number; }
    public int retDamage() { return this.damage; }
    public int retType() { return this.type;}
    public int retAtkType() { return this.AtkType; }
    public int[] retEffect() { return this.effect; }
    public status retGiveStatus() { return this.giveStatus; }

    public string retSkillName(int number) { return SkillList[number].retSkillName(); }
    public int retNumber(int number) { return number; }
    public int retDamage(int number) { return SkillList[number].retDamage(); }
    public int retType(int number) { return SkillList[number].retType(); }
    public int retAtkType(int number) { return SkillList[number].retAtkType(); }
    public int[] retEffect(int number) { return SkillList[number].retEffect(); }
    public status retGiveStatus(int number) { return SkillList[number].retGiveStatus(); }



}
