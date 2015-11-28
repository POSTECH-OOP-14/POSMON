using UnityEngine;
using System.Collections;

//manage game menu and game background and game object, battle flow
public class BattleButtonManage : MonoBehaviour
{
    // get the screen info.
    public Camera cam;

    //save the battle state.
    enum BattleButtonState  
    {
        DefaultState,
        AttackState,
        ExchangeState,
        ItemState,
        RunState,
        NextState
    };

    //save temporay stat. 
    //my students int(atk), str(spcial atk), mental(def), guard(special def), sense(speed), health
    //opponent students int(atk), str(spcial atk), mental(def), guard(special def), sense(speed), health
    // each data save integer. applyed by (n+2)/n ~ 2/(n+2)
    public int[] battleTempStat =new int[12]; 

    // battle starts with default state
    private BattleButtonState Battle = BattleButtonState.DefaultState;
    
    // set the postion of buttons.
    Rect FirstPos;
    Rect SecondPos;
    Rect ThirdPos;
    Rect ForthPos;

    //set the infomation of student who will came out to battle.
    public Student CurrentMine;
    public Student CurrentEnemy;

    //set the information of 
    public CharacterStatus studentInfo;
    public Student[] EnemyStudentList;

    //information about skill
    public SkillInfo SkillData;

    // get the battle related code 
    BattleScene a = new BattleScene();

    // testing GUI button
    string alphahah = "fire blast";
    
    // Use this for initialization. get information from 
    void Start()
    {
        //init battleTempStat
        for (int i = 0; i < 12; i++)
            battleTempStat[i] = 0;
        //   Mine = new StudentInfo();
       // Enemy = new StudentInfo();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.X))
        {
            if (Battle == BattleButtonState.AttackState)
                Battle = BattleButtonState.DefaultState;
            else if (Battle == BattleButtonState.ExchangeState)
                Battle = BattleButtonState.DefaultState;
            else if (Battle == BattleButtonState.ItemState)
                Battle = BattleButtonState.DefaultState;
            else if (Battle == BattleButtonState.NextState)
                Battle = BattleButtonState.DefaultState;
        }
    }

    void OnGUI()
    {
        int myToOppoDamage = 0; //save the damage that student give to opponent.
        int damage = 0;
        int MyStudentAlived = 0; //save the info wether player student is dead.
        int OppoStudentAlived = 0; // save the info wether opponent student is dead.
        int alived = 0; //check wther student get faint.
        //get speed, use for check whos first.
        int MyMoveSpeed;
        int EnemyMoveSpeed;
        
        //get what people selected to do. 0 is attack, 1 is item, 2 is change, 3 is run.
        int myMove;
        int enemyMove;
        

        Rect FirstPos = new Rect(cam.pixelWidth * 5/7 - 10, cam.pixelHeight * 5 / 7 - 10, cam.pixelWidth / 7, cam.pixelHeight / 7);
        Rect SecondPos = new Rect(cam.pixelWidth * 6/7 - 10, cam.pixelHeight * 5 / 7 - 10, cam.pixelWidth / 7, cam.pixelHeight / 7);
        Rect ThirdPos = new Rect(cam.pixelWidth * 5/7 - 10, cam.pixelHeight *6/ 7 - 10, cam.pixelWidth / 7, cam.pixelHeight / 7);
        Rect ForthPos = new Rect(cam.pixelWidth * 6/7 - 10, cam.pixelHeight *6/ 7 - 10, cam.pixelWidth / 7, cam.pixelHeight / 7);

        //cam.pixelwidth = 956, cam.pixelheight = 426 
        if (Battle == BattleButtonState.DefaultState)
        {
            if (GUI.Button(FirstPos, "Attack"))
            { Battle = BattleButtonState.AttackState; }
            if (GUI.Button(SecondPos, "Change"))
            { Battle = BattleButtonState.ExchangeState; }

            if (GUI.Button(ThirdPos, "Item"))
            { Battle = BattleButtonState.ItemState; }

            if (GUI.Button(ForthPos, "Run"))
            {
                Battle = BattleButtonState.RunState;
                //text message 
                Application.LoadLevel(1);
            }
        }
        if (Battle == BattleButtonState.AttackState)
        {
            alived = 0;
            if (GUI.Button(FirstPos,CurrentMine.retSkillList[0]))
            {
                damage = a.BattleDamageCalculate(Mine.skillList[0], Mine, Enemy);
                Battle = BattleButtonState.NextState;
                myMove = 0;
            }
            if (GUI.Button(SecondPos, Mine.skillList[1].skillName))
            {
                damage = a.BattleDamageCalculate(Mine.skillList[1], Mine, Enemy);
                Battle = BattleButtonState.NextState;
                myMove = 0;
            }
            if (GUI.Button(ThirdPos, Mine.skillList[2].skillName))
            {
                damage = a.BattleDamageCalculate(Mine.skillList[2], Mine, Enemy);
                Battle = BattleButtonState.NextState;
                myMove = 0;
            }

            if (GUI.Button(ForthPos, Mine.skillList[3].skillName))
            {
                damage = a.BattleDamageCalculate(Mine.skillList[3], Mine, Enemy);
                Battle = BattleButtonState.NextState;
                myMove = 0;
            }
        }

        //if player give input, the battle proceed.
        if (Battle == BattleButtonState.NextState)
        {
            //npc select its move.
            //
            //calculate who is first to move.
            // enemy attacks
            
            //int b = (int)Random.value;  //random으로 0에서 3의 값을 구한다.
            int b = 0;
            myToOppoDamage = a.BattleDamageCalculate(Enemy.skillList[b], Enemy, Mine);
                
            

            int whoFirst = a.CalFirstGo(CurrentMine.retStuStat(4),CurrentEnemy.retStuStat(4),battleTempStat);

            if (whoFirst == 0)  //player go first
            {
                if (myMove == 0)
                {
                    alived = CurrentEnemy.getDamage(damage);
                    if (alived == 1) //if enemy student fainted.
                    {
                        //give exp;
                        CurrentMine.setExp(CurrentEnemy.getExp());
                        //change student;
                        //
                        Battle = BattleButtonState.DefaultState;
                    }
                }

                //enemy attacks.
                alived = CurrentMine.getDamage(myToOppoDamage);
                if (alived == 1)    // if our student dead.
                {
                    CurrentMine.giveAStatus(status.faint);
                    //check battle end;
                    if (0 == 0)
                    {
                        //battle ended.
                    }
                    else
                    {
                        //changeStudent.
                        //currentMine = a.changeStudent(studentInfo.getStudent());

                    }
                }
                Battle = BattleButtonState.DefaultState;
            }
            else //enemy go first
            {
                //enemy attacks.
                alived = CurrentMine.getDamage(myToOppoDamage);
                if (alived == 1)    // if our student dead.
                {
                    CurrentMine.giveAStatus(status.faint);
                    //check battle end;
                    if (0 == 0)
                    {
                        //battle ended.
                    }
                    else
                    {
                        //changeStudent.
                        //currentMine = a.changeStudent(studentInfo.getStudent());
                    }
                }

                //player attacks
                if (myMove == 0)
                {
                    alived = CurrentEnemy.getDamage(damage);
                    if (alived == 1) //if enemy student fainted.
                    {
                        //give exp;
                        CurrentMine.setExp(CurrentEnemy.getExp());
                        //change student;
                        //
                        Battle = BattleButtonState.DefaultState;
                    }
                }
                Battle = BattleButtonState.DefaultState;
            }
        
        }

    }
}
