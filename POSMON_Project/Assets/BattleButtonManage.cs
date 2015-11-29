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
    public Student[] MineStudentList;
    public Student[] EnemyStudentList;

    //information about skill
    SkillList SkillData = new SkillList();

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
        //MineStudentList = GetComponent<CharacterStatus>().getStuList();
    //    StudentInfo getnew = new StudentInfo(); doesn't used
        MineStudentList = gameObject.GetComponent<StudentInfo>().myDebugStuList;
        EnemyStudentList = gameObject.GetComponent<StudentInfo>().enemyDebugStuList;
        CurrentMine = gameObject.GetComponent<StudentInfo>().retStuData(0, 0);
        CurrentEnemy = gameObject.GetComponent<StudentInfo>().retStuData(1, 0);
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
 
        int MyMoveSpeed = 0;//get speed, use for check whos first. 
        int EnemyMoveSpeed = 0;
        
        //get what people selected to do. 0 is attack, 1 is item, 2 is change, 3 is run.
        int myMove=0;
        int enemyMove=0;
        

        Rect FirstPos = new Rect(cam.pixelWidth * 5/7 - 10, cam.pixelHeight * 5 / 7 - 10, cam.pixelWidth / 7, cam.pixelHeight / 7);
        Rect SecondPos = new Rect(cam.pixelWidth * 6/7 - 10, cam.pixelHeight * 5 / 7 - 10, cam.pixelWidth / 7, cam.pixelHeight / 7);
        Rect ThirdPos = new Rect(cam.pixelWidth * 5/7 - 10, cam.pixelHeight *6/ 7 - 10, cam.pixelWidth / 7, cam.pixelHeight / 7);
        Rect ForthPos = new Rect(cam.pixelWidth * 6/7 - 10, cam.pixelHeight *6/ 7 - 10, cam.pixelWidth / 7, cam.pixelHeight / 7);

        //cam.pixelwidth = 956, cam.pixelheight = 426 
        if (Battle == BattleButtonState.DefaultState)
        {
            if (GUI.Button(FirstPos, "Attack")) { Battle = BattleButtonState.AttackState; }
            if (GUI.Button(SecondPos, "Change")) { Battle = BattleButtonState.ExchangeState; }
            if (GUI.Button(ThirdPos, "Item")) { Battle = BattleButtonState.ItemState; }
            if (GUI.Button(ForthPos, "Run")) {
                Battle = BattleButtonState.RunState;
                //text message 
                Application.LoadLevel(1);
            }
            if (CurrentMine == null)
                Debug.Log("current mine is empty");
            if (MineStudentList == null)
                Debug.Log("current mine is empty");
        }
        if (Battle == BattleButtonState.AttackState)
        {
            alived = 0;
            if (GUI.Button(FirstPos,SkillData.retSkillName(CurrentMine.retSkillList()[0])))  {
                damage = a.BattleDamageCalculate(SkillData.retSkillInfo(CurrentMine.retSkillList()[0]), CurrentMine, CurrentEnemy,battleTempStat);
                Battle = BattleButtonState.NextState;
                myMove = 0;
            }
            if (GUI.Button(SecondPos, SkillData.retSkillName(CurrentMine.retSkillList()[1]))) {
                damage = a.BattleDamageCalculate(SkillData.retSkillInfo(CurrentMine.retSkillList()[1]), CurrentMine, CurrentEnemy, battleTempStat);
                Battle = BattleButtonState.NextState;
                myMove = 0;
            }
            if (GUI.Button(ThirdPos, SkillData.retSkillName(CurrentMine.retSkillList()[2]))) {
                damage = a.BattleDamageCalculate(SkillData.retSkillInfo(CurrentMine.retSkillList()[2]), CurrentMine, CurrentEnemy, battleTempStat);
                Battle = BattleButtonState.NextState;
                myMove = 0;
            }
            if (GUI.Button(ForthPos, SkillData.retSkillName(CurrentMine.retSkillList()[3]))) {
                damage = a.BattleDamageCalculate(SkillData.retSkillInfo(CurrentMine.retSkillList()[3]), CurrentMine, CurrentEnemy, battleTempStat);
                Battle = BattleButtonState.NextState;
                myMove = 0;
            }
        }
        else if (Battle == BattleButtonState.ExchangeState)
        {
            int i= 0;
            i = DrawStudentGUI(MineStudentList);
            if(i == 1){
                myMove = 2;
                Battle = BattleButtonState.NextState;
            }
        }
        //if player give input, the battle proceed.
        else if (Battle == BattleButtonState.NextState)
        {
            //npc select its move.
            //
            //calculate who is first to move.
            // enemy attacks
            //int b = (int)Random.value;  //random으로 0에서 3의 값을 구한다.
            int b = 0;
            myToOppoDamage = a.BattleDamageCalculate(SkillData.retSkillInfo(CurrentEnemy.retSkillList()[b]), CurrentEnemy, CurrentMine, battleTempStat);


            // 명령을 내리고 나서 배틀을 시작하기 전에 상태이상이나 특수효과로 공격하는지 마는지 확인한다.
            int battleStartMove = a.checkBattleTurnStartEvent(CurrentMine); 

            int whoFirst = a.CalFirstGo(CurrentMine.retStuStat(4),CurrentEnemy.retStuStat(4),battleTempStat);

            if (whoFirst == 0)  //player go first
            {
                if (battleStartMove != 0)
                { 
                    //여기서 상태이상으로 행동이 종료되었을 때 메세지를 출력하면 된다.,
                }
                else if (myMove == 0)
                {
                    alived = CurrentEnemy.getDamage(damage);
                    if (alived == 1) //if enemy student fainted.
                    {
                        CurrentMine.setExp(CurrentEnemy.getExp()); //give exp;
                        
                        //change student;
                        //
                        Battle = BattleButtonState.DefaultState;
                    }
                }

                //enemy attacks.
                alived = CurrentMine.getDamage(myToOppoDamage);
                battleStartMove = a.checkBattleTurnStartEvent(CurrentEnemy); 
                if (battleStartMove != 0)
                {
                    //여기서 상태이상으로 행동이 종료되었을 때 메세지를 출력하면 된다.,
                }
                else if (alived == 1)    // if our student dead.
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
                if (battleStartMove != 0)
                {
                    //여기서 상태이상으로 행동이 종료되었을 때 메세지를 출력하면 된다.,
                }
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


                a.checkBattleTurnEndEvent(CurrentMine);
                a.checkBattleTurnEndEvent(CurrentEnemy);

                Battle = BattleButtonState.DefaultState;
            }
        }
    }

    int DrawStudentGUI(Student[] list)
    {
        Rect StuPos1 = new Rect(cam.pixelWidth * 1 / 3, cam.pixelHeight / 9, cam.pixelWidth / 3, cam.pixelHeight / 9);
        Rect StuPos2 = new Rect(cam.pixelWidth * 1 / 3, cam.pixelHeight * 2 / 9 + 20, cam.pixelWidth / 3, cam.pixelHeight / 9);
        Rect StuPos3 = new Rect(cam.pixelWidth * 1 / 3, cam.pixelHeight * 3 / 9 + 40, cam.pixelWidth / 3, cam.pixelHeight / 9);
        Rect StuPos4 = new Rect(cam.pixelWidth * 1 / 3, cam.pixelHeight * 4 / 9 + 60, cam.pixelWidth / 3, cam.pixelHeight / 9);
        Rect StuPos5 = new Rect(cam.pixelWidth * 1 / 3, cam.pixelHeight * 5 / 9 + 80, cam.pixelWidth / 3, cam.pixelHeight / 9);
        Rect StuPos6 = new Rect(cam.pixelWidth * 1 / 3, cam.pixelHeight * 6 / 9 + 100, cam.pixelWidth / 3, cam.pixelHeight / 9);

        if (GUI.Button(StuPos1, list[0].retStuIndex().ToString()))
            return 1;

        return 0;

    }

}
