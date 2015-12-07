using UnityEngine;
using System.Collections;

public class MyStatusAnimation : MonoBehaviour {
    Student myStudent;
    Student enemyStudent;
    float blend = 0;
    protected Animator anim;
	// Use this for initialization
	void Start () {
        myStudent = GameObject.Find("battleBackground").GetComponent<BattleButtonManage>().CurrentMine;
        enemyStudent = GameObject.Find("battleBackground").GetComponent<BattleButtonManage>().CurrentEnemy;
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        myStudent = GameObject.Find("battleBackground").GetComponent<BattleButtonManage>().CurrentMine;
        enemyStudent = GameObject.Find("battleBackground").GetComponent<BattleButtonManage>().CurrentEnemy;
        if (myStudent.retStuStatus() == status.fire)
            blend = 0;
        else if (myStudent.retStuStatus() == status.none)
            blend = 0.2f;
        else if (myStudent.retStuStatus() == status.paralysis)
            blend = 0.4f;
        else if (myStudent.retStuStatus() == status.poison)
            blend = 0.6f;
        else if (myStudent.retStuStatus() == status.sleep)
            blend = 0.8f;
        else if(myStudent.retStuStatus() == status.faint)
            blend = 1.0f;

        anim.SetFloat("Blend", blend);
	
	}
}
