using UnityEngine;
using System.Collections;

public class ChangeCharacter : MonoBehaviour {
    
    public Camera cam;
    public Texture Bunchi;
    public Texture Assin;
    Texture myTexture;
    Texture opponentTexture;
    GameObject mineStu;
    Student myStudent;
    Student enemyStudent;
    StudentTextureList textureList;
    AttackAnimationList animationlist;
    int myi = 0;
    int enemyi = 0;
    int myj = 0;
    int enemyj = 0;
    Texture[] Myframe;
    Texture[] Enemyframe;
    BattleButtonManage battlestate;
    Rect OpponentStudentImage;
    Rect OurStudentImage;
    bool myAtkStart = false;
    bool EnemyAtkStart = false;
    double prevMyHp;
    double currMyHp;
    double PrevEnemyHp;
    double currEnemyHp;
    bool myAtk = false;
    bool EnemyAtk = false;
    int mycount = 0;
    int enemycount = 0;
    int k1 = 0;

    // Use this for initialization
	void Start () {
        textureList = GameObject.Find("Main Camera").GetComponent<StudentTextureList>();
        mineStu = GameObject.Find("battleBackground");
        myStudent = GameObject.Find("battleBackground").GetComponent<BattleButtonManage>().CurrentMine;
        enemyStudent = GameObject.Find("battleBackground").GetComponent<BattleButtonManage>().CurrentEnemy;
        animationlist = GameObject.Find("Main Camera").GetComponent<AttackAnimationList>();
        battlestate = GameObject.Find("battleBackground").GetComponent<BattleButtonManage>();
        if (myStudent == null)
            Debug.Log("myStu is null");

        if (myStudent.retSkillList() == null)
            Debug.Log("mySkillList is null");
     
	}
    
    void Update () {
        myStudent = GameObject.Find("battleBackground").GetComponent<BattleButtonManage>().CurrentMine;
        enemyStudent = GameObject.Find("battleBackground").GetComponent<BattleButtonManage>().CurrentEnemy;
       
        myTexture = textureList.retTexture(myStudent.retStuIndex());
        opponentTexture = textureList.retTexture(enemyStudent.retStuIndex());
        if (battlestate.Battle == BattleButtonManage.BattleButtonState.DefaultState)
        {
            prevMyHp = myStudent.getHP();
            PrevEnemyHp = enemyStudent.getHP();
        }
        currMyHp = myStudent.getHP();
        currEnemyHp = enemyStudent.getHP();

        if (prevMyHp < currMyHp) EnemyAtkStart = true;
        
        if (PrevEnemyHp < currEnemyHp) myAtkStart = true;
        
        if (battlestate.EnemySkill != null && battlestate.EnemySkill.retAtkType() == 0)
        {
            Myframe = animationlist.Atk;
        }
        else if (battlestate.EnemySkill != null && battlestate.EnemySkill.retAtkType() == 1)
        {
            Myframe = animationlist.SAtk;
        }
        if (battlestate.mystuSkill != null && battlestate.mystuSkill.retAtkType() == 0)
        {
            Enemyframe = animationlist.Atk;
        }
        else if (battlestate.mystuSkill != null && battlestate.mystuSkill.retAtkType() == 1)
        {
            Enemyframe = animationlist.SAtk;
        }

        if (Myframe != null)
        {
            myi = (int)(Time.time * framesecond) % Myframe.Length;
        }
        if (EnemyAtkStart)
        {
            myj = myi;
            EnemyAtkStart = false;
            EnemyAtk = true;
        }

        if (Enemyframe != null)
        {
            enemyi = (int)(Time.time * framesecond) % Enemyframe.Length;
        }
        if (myAtkStart)
        {
            enemyj = enemyi;
            myAtkStart = false;
            myAtk = true;
        }
        
  /*
  //the code will save index of posmon. set texture to current index student.
         
  */
    }

    void moveGameObject(GameObject alpha, Vector2 vector)
    {
        alpha.transform.position += (Vector3)vector;
    }

    void ChangeGameImage(GameObject alpha, Texture ImageName)
    {
        Graphics.DrawTexture(OpponentStudentImage, opponentTexture);
    }
    
    private double framesecond = 10;
    
    void OnGUI()
    {
      OpponentStudentImage = new Rect(cam.pixelWidth * 3 / 4, cam.pixelHeight * 1 / 8, cam.pixelWidth / 8, cam.pixelWidth / 8);
      OurStudentImage = new Rect(cam.pixelWidth * 1 / 8, cam.pixelHeight * 2 / 4, cam.pixelWidth / 8, cam.pixelWidth / 8);

     
        if (myTexture != null && opponentTexture != null)
      {
          Graphics.DrawTexture(OpponentStudentImage, opponentTexture);
          Graphics.DrawTexture(OurStudentImage, myTexture);
          
              
          
      }

        if (Myframe != null && battlestate.Battle == BattleButtonManage.BattleButtonState.NextState)
        {
            if (myi - myj == Myframe.Length - 1) myAtk = false;
            if (enemyi - enemyj == Enemyframe.Length - 1) EnemyAtk = false;

            if (myAtk)
                Graphics.DrawTexture(OurStudentImage, Myframe[myi - myj]);
            if (EnemyAtk)
                Graphics.DrawTexture(OpponentStudentImage, Enemyframe[enemyi - enemyj]);

        }
        else
        {

            myAtk = true;
            if(battlestate.Battle == BattleButtonManage.BattleButtonState.AttackState)
                EnemyAtk = true;
        }
    }
    
}
