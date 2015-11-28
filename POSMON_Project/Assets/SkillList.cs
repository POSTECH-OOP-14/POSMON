using UnityEngine;
using System.Collections;

public class SkillList : MonoBehaviour {


    public SkillInfo[] SkillArray = new SkillInfo[5]{
             new SkillInfo(1,"headbutt",50,1,0,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.none),
            new SkillInfo(2,"FireBlast",50,1,0,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.none),
            new SkillInfo(3,"adsf",50,1,0,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.none),
            new SkillInfo(4,"posion",50,1,0,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.poison),
            new SkillInfo(5,"GetOut",50,1,0,new int[12]{0,0,0,0,0,0,0,0,0,0,0,0},status.none)
};
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public string retSkillName(int number) { return SkillArray[number].retSkillName(); }
    public int retNumber(int number) { return number; }
    public int retDamage(int number) { return SkillArray[number].retDamage(); }
    public int retType(int number) { return SkillArray[number].retType(); }
    public int retAtkType(int number) { return SkillArray[number].retAtkType(); }
    public int[] retEffect(int number) { return SkillArray[number].retEffect(); }
    public status retGiveStatus(int number) { return SkillArray[number].retGiveStatus(); }
public SkillInfo retSkillInfo(int number) { return SkillArray[number]; }

}
