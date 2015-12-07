using UnityEngine;
using System.Collections;

public class EnemyStatusAnimation : MonoBehaviour
{
    Student myStudent;
    Student enemyStudent;
    float blend = 0.2f;
    protected Animator anim;
    // Use this for initialization
    void Start()
    {
        myStudent = GameObject.Find("battleBackground").GetComponent<BattleButtonManage>().CurrentMine;
        enemyStudent = GameObject.Find("battleBackground").GetComponent<BattleButtonManage>().CurrentEnemy;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        myStudent = GameObject.Find("battleBackground").GetComponent<BattleButtonManage>().CurrentMine;
        enemyStudent = GameObject.Find("battleBackground").GetComponent<BattleButtonManage>().CurrentEnemy;
       
        if (enemyStudent.retStuStatus() == status.fire)
            blend = 0f;
        else if (enemyStudent.retStuStatus() == status.none)
            blend = 0.2f;
        else if (enemyStudent.retStuStatus() == status.paralysis)
            blend = 0.4f;
        else if (enemyStudent.retStuStatus() == status.poison)
            blend = 0.6f;
        else if (enemyStudent.retStuStatus() == status.sleep)
            blend = 0.8f;
        else if (enemyStudent.retStuStatus() == status.faint)
            blend = 0.2f;

        Debug.Log(blend.ToString());
        anim.SetFloat("Blend", blend);

    }
}
