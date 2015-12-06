using UnityEngine;
using System.Collections;

public class SkillList{

    //int _number, string _skillName, int _damage, int _type, int _atktype, int[] _effect, status _status, int _maxChance, int _nowChance
    //type은 department 
    //0은 공격, 1은 특수공격

    public SkillInfo[] SkillArray = new SkillInfo[22]{

            new SkillInfo(1,"푸코의 진자",30,10,1,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.none,10,10),
            new SkillInfo(2,"돌 떨구기",65,11,1,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.none,5,5),
            new SkillInfo(3,"붉은 시약 마시기",0,50,0,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.poison,10,10),
            new SkillInfo(4,"푸른 시약 마시기",0,51,0,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.none,10,10),
            new SkillInfo(5,"산성 시약 뿌리기",30,60,1,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.none,10,10),
            new SkillInfo(6,"콜록콜록 독가스",25,61,0,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.none,10,10),
            new SkillInfo(7,"인체 개조",0,80,0,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.none,10,10),
            new SkillInfo(8,"맹독침",20,81,0,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.poison,10,10),
            new SkillInfo(9,"강철주먹",30,70,1,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.paralysis,10,10),
            new SkillInfo(10,"얼음분사",25,71,1,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.none,10,10),
            new SkillInfo(11,"단단해지기",0,100,1,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.none,10,10),
            new SkillInfo(12,"톱니바퀴 갈갈이",35,101,1,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.none,10,10),
            new SkillInfo(13,"두뇌 해킹",20,20,0,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.none,10,10),
            new SkillInfo(14,"코드 짜기",45,21,0,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.none,10,10),
            new SkillInfo(15,"내 몸을 검으로",70,40,0,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.none,5,5),
            new SkillInfo(16,"아싸의 아싸아싸",40,41,1,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.none,7,7),
            new SkillInfo(17,"인맥동원",60,30,1,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.none,5,5),
            new SkillInfo(18,"퉤퉤퉷 말빨",0,31,0,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.sleep,10,10),
            new SkillInfo(19,"람다 펑션",30,0,0,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.none,10,10),
            new SkillInfo(20,"미방을 풀어라",20,1,1,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.sleep,10,10),
            new SkillInfo(21,"화염방사",70,10,1,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.none,5,5),
            new SkillInfo(22,"전기감전",10,11,1,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.paralysis, 10,10),

};
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public string retSkillName(int number) { return SkillArray[number].retSkillName(); }
    public int retNumber(int number) { return number; }
    public int retDamage(int number) { return SkillArray[number].retDamage(); }
    public int retType(int number) { return SkillArray[number].retType(); }
    public int retAtkType(int number) { return SkillArray[number].retAtkType(); }
    public int[] retEffect(int number) { return SkillArray[number].retEffect(); }
    public status retGiveStatus(int number) { return SkillArray[number].retGiveStatus(); }
    public int retMaxChance(int number) { return SkillArray[number].retMaxChance(); }
    public int retNowChance(int number) { return SkillArray[number].retNowChance(); }
    public SkillInfo retSkillInfo(int number) { return SkillArray[number]; }

}
