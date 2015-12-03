﻿using UnityEngine;
using System.Collections;

//manage game menu and game background and game object, battle flow
public class BattleButtonManage : MonoBehaviour
{
    // get the screen info.
    public Camera cam;

    //save the battle state.
    public enum BattleButtonState  
    {
        DefaultState,
        AttackState,
        ExchangeState,
        ItemState,
        RunState,
        NextState,
        OneOutState
    };

    //save temporay stat. 
    //my students int(atk), str(spcial atk), mental(def), guard(special def), sense(speed), health
    //opponent students int(atk), str(spcial atk), mental(def), guard(special def), sense(speed), health
    // each data save integer. applyed by (n+2)/n ~ 2/(n+2)
    public int[] battleTempStat =new int[12]; 

    // battle starts with default state
    public BattleButtonState Battle = BattleButtonState.DefaultState;
    //save information about battle end. 0 is run, 1 is defeated by npc, 2 is win
    int HowBattleEnd = 0;
    public bool WhenMouseDown = false;
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
    
    IEnumerator TestCode;

    //information about skill
    SkillList SkillData = new SkillList();

    // get the battle related code 
    BattleScene a = new BattleScene();

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
       TestCode = WholeBattleCode();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentMine.getHP() == 0)
            Battle = BattleButtonState.OneOutState;
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
        if (Input.GetMouseButtonDown(0)||Input.GetKey(KeyCode.Z))
            WhenMouseDown = true;
    }

    void OnGUI()
    {
       TestCode.MoveNext();
    }

    int outOfStudent(Student[] list)
    { 
        for(int i = 0 ; i < 6 ; i ++ )
        {
            if(list[i] != null){
                if (list[i].retStuStatus() != status.faint)
                    return i+1;
            }
        }
        return 0;
    }

    void callBattleEnd()
    { 
           
    
    }

    IEnumerator WholeBattleCode()
    {
            bool aba = false;
            int myToOppoDamage = 0; //save the damage that student give to opponent.
            int damage = 0;
            int MyStudentAlived = 0; //save the info wether player student is dead.
            int OppoStudentAlived = 0; // save the info wether opponent student is dead.
            int alived = 0; //check wther student get faint.
            int toChangeP = 0;
            //get what people selected to do. 0 is attack, 1 is item, 2 is change, 3 is run.
            int myMove = 0;
            int enemyMove = 0;
            
            Rect FirstPos = new Rect(cam.pixelWidth * 5 / 7 - 10, cam.pixelHeight * 5 / 7 - 10, cam.pixelWidth / 7, cam.pixelHeight / 7);
            Rect SecondPos = new Rect(cam.pixelWidth * 6 / 7 - 10, cam.pixelHeight * 5 / 7 - 10, cam.pixelWidth / 7, cam.pixelHeight / 7);
            Rect FifthPos = new Rect(cam.pixelWidth * 6 / 7 - 10, cam.pixelHeight * 4 / 7 - 10, cam.pixelWidth / 7, cam.pixelHeight / 7);
            
            Rect ThirdPos = new Rect(cam.pixelWidth * 5 / 7 - 10, cam.pixelHeight * 6 / 7 - 10, cam.pixelWidth / 7, cam.pixelHeight / 7);
            Rect ForthPos = new Rect(cam.pixelWidth * 6 / 7 - 10, cam.pixelHeight * 6 / 7 - 10, cam.pixelWidth / 7, cam.pixelHeight / 7);
            while (true)
            {
            //cam.pixelwidth = 956, cam.pixelheight = 426 
            if (Battle == BattleButtonState.DefaultState)
            {
                GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "What Should we do?" + "\n" + "");
                
                if (GUI.Button(FirstPos, "공격")) { Battle = BattleButtonState.AttackState; }
                if (GUI.Button(SecondPos, "학생 교체")) { Battle = BattleButtonState.ExchangeState; }
                if (GUI.Button(ThirdPos, "아이템 사용")) { Battle = BattleButtonState.ItemState; }
                if (GUI.Button(ForthPos, "도망치기")) { Battle = BattleButtonState.RunState; }
                if (GUI.Button(FifthPos, "교수의 권한으로 승리")) { Battle = BattleButtonState.RunState; HowBattleEnd = 2;}
                
            }
            else if (Battle == BattleButtonState.RunState)
            {
                if (GameManager.getBattleHostNum() != 0 && HowBattleEnd == 0) // NPC와 싸우고 있는 경우, 불가능하다.
                {
                    for (int i = 0; i < 150; i++)
                    {
                        GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "지금은 도망칠 수 없다.");
                        yield return new WaitForEndOfFrame();
                    }
                    Battle = BattleButtonState.DefaultState;
                }
                else if(HowBattleEnd == 0) //전투에서 도망쳤을 때
                {
                    for (int i = 0; i < 150; i++)
                    {
                        GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "성공적으로 도망쳤다.");
                        yield return new WaitForEndOfFrame();
                    }
                    GameManager.pl_stored.SetActive(true);
                    Application.LoadLevel(GameManager.PrevSceneNumber);
                }
                else if (HowBattleEnd == 1) //전투에서 패배를 했을 때
                {
                    for (int i = 0; i < 150; i++)
                    {
                        GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "학생들이 모두 지쳐서 더는 싸울 수 없다...");
                        yield return new WaitForEndOfFrame();
                    }
                    GameManager.pl_stored.SetActive(true);
                    Application.LoadLevel(GameManager.PrevSceneNumber);
                }

                else if (HowBattleEnd == 2) // 전투에서 승리했을 때
                {
                    for (int i = 0; i < 150; i++)
                    {
                        GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "전투에서 승리했다!");
                        yield return new WaitForEndOfFrame();
                    }
                    GameManager.pl_stored.SetActive(true);
                    /* update quest info */
                    if (GameManager.getBattleHostNum() != 0)
                        GameManager.pl_stored.GetComponent<CharacterStatus>().getQuest(GameManager.getBattleHostNum()).markCompleted();

                    Application.LoadLevel(GameManager.PrevSceneNumber);
                }
            }
            else if (Battle == BattleButtonState.AttackState)
            {
                GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "어떻게 공격을 할 것인가?" + "\n" + "(x 버튼으로 돌아갈 수 있습니다.)");

                alived = 0;
                if (GUI.Button(FirstPos, SkillData.retSkillName(CurrentMine.retSkillList()[0])))
                {
                    damage = a.BattleDamageCalculate(SkillData.retSkillInfo(CurrentMine.retSkillList()[0]), CurrentMine, CurrentEnemy, battleTempStat);
                    Battle = BattleButtonState.NextState;
                    myMove = 0;
                }
                if (GUI.Button(SecondPos, SkillData.retSkillName(CurrentMine.retSkillList()[1])))
                {
                    damage = a.BattleDamageCalculate(SkillData.retSkillInfo(CurrentMine.retSkillList()[1]), CurrentMine, CurrentEnemy, battleTempStat);
                    Battle = BattleButtonState.NextState;
                    myMove = 0;
                }
                if (GUI.Button(ThirdPos, SkillData.retSkillName(CurrentMine.retSkillList()[2])))
                {
                    damage = a.BattleDamageCalculate(SkillData.retSkillInfo(CurrentMine.retSkillList()[2]), CurrentMine, CurrentEnemy, battleTempStat);
                    Battle = BattleButtonState.NextState;
                    myMove = 0;
                }
                if (GUI.Button(ForthPos, SkillData.retSkillName(CurrentMine.retSkillList()[3])))
                {
                    damage = a.BattleDamageCalculate(SkillData.retSkillInfo(CurrentMine.retSkillList()[3]), CurrentMine, CurrentEnemy, battleTempStat);
                    Battle = BattleButtonState.NextState;
                    myMove = 0;
                }
            }
            else if (Battle == BattleButtonState.ExchangeState)
            {
                Rect StuPos1 = new Rect(cam.pixelWidth * 1 / 3, cam.pixelHeight / 9, cam.pixelWidth / 3, cam.pixelHeight / 9);
                Rect StuPos2 = new Rect(cam.pixelWidth * 1 / 3, cam.pixelHeight * 2 / 9, cam.pixelWidth / 3, cam.pixelHeight / 9);
                Rect StuPos3 = new Rect(cam.pixelWidth * 1 / 3, cam.pixelHeight * 3 / 9, cam.pixelWidth / 3, cam.pixelHeight / 9);
                Rect StuPos4 = new Rect(cam.pixelWidth * 1 / 3, cam.pixelHeight * 4 / 9, cam.pixelWidth / 3, cam.pixelHeight / 9);
                Rect StuPos5 = new Rect(cam.pixelWidth * 1 / 3, cam.pixelHeight * 5 / 9, cam.pixelWidth / 3, cam.pixelHeight / 9);
                Rect StuPos6 = new Rect(cam.pixelWidth * 1 / 3, cam.pixelHeight * 6 / 9, cam.pixelWidth / 3, cam.pixelHeight / 9);
                int[] availableStu = new int[7] { 0, 0, 0, 0, 0, 0, 0 }; //get info whether player can change student to it
                int retStu = 0;
                GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "교체할 학생을 고르십시오." + "\n" + "X로 돌아가기 가능");

                //학생 리스트에 들어있는 학생들이 존재하는지, 기절했는지 알아보는 반복문이다.
                for (int j = 0; j < 6; j++)
                {
                    if (MineStudentList[j] == null)
                        availableStu[j] = 1;
                    else if (MineStudentList[j].retStuStatus() == status.faint)
                        availableStu[j] = 2;
                    availableStu[6] += availableStu[j];
                }
                int i = 0;
                //아래서 부터는 존재하는 학생마다 학생의 이름, 학생의 체력 정보, 학생의 레벨, 학생의 상태를 출력한다.
                if (availableStu[0] != 1)
                {
                    if (GUI.Button(StuPos1, MineStudentList[0].retStudentName()+ "HP : " + MineStudentList[0].getHP() + "/" + MineStudentList[0].getMAXHP() + " " + MineStudentList[0].retStuStatus().ToString()))
                        i = 1;
                }
                if (availableStu[1] != 1)
                {
                    if (GUI.Button(StuPos2, MineStudentList[1].retStudentName() + "HP : " + MineStudentList[1].getHP() + "/" + MineStudentList[1].getMAXHP() + " " + MineStudentList[1].retStuStatus().ToString()))
                        i = 2;
                }
                if (availableStu[2] != 1)
                {
                    if (GUI.Button(StuPos3, MineStudentList[2].retStudentName() + "HP : " + MineStudentList[2].getHP() + "/" + MineStudentList[2].getMAXHP() + " " + MineStudentList[2].retStuStatus()))
                        i = 3;
                }
                if (availableStu[3] != 1)
                {
                    if (GUI.Button(StuPos4, MineStudentList[3].retStudentName() + "HP : " + MineStudentList[3].getHP() + "/" + MineStudentList[3].getMAXHP() + " " + MineStudentList[3].retStuStatus().ToString()))
                        i = 4;
                }
                if (availableStu[4] != 1)
                {
                    if (GUI.Button(StuPos5, MineStudentList[4].retStudentName() + "HP : " + MineStudentList[4].getHP() + "/" + MineStudentList[4].getMAXHP() + " " + MineStudentList[4].retStuStatus().ToString()))
                        i = 5;
                }
                if (availableStu[5] != 1)
                {
                    if (GUI.Button(StuPos6, MineStudentList[5].retStudentName() + "HP : " + MineStudentList[5].getHP() + "/" + MineStudentList[5].getMAXHP() + " " + MineStudentList[5].retStuStatus().ToString()
                                   ))
                        i = 6;
                }
                if (i != 0)
                {
                    //선택한 학생이 이미 나가 있다면
                    if (MineStudentList[i-1] == CurrentMine)
                    {
                        bool aaa = false;
                        while (!aaa)
                        {
                            GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "그 학생은 이미 싸우고 있다!\n(z나 enter를 입력)");
                            aaa = Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetMouseButton(0);
                            yield return null;
                        }
                        i = 0;
                    }
                    //선택한 버튼이 기절한 학생이라면
                    else if (MineStudentList[i-1].retStuStatus() == status.faint)
                    {
                        bool aaa = false;
                        while (!aaa)
                        {
                            GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "그 학생은 이미 싸우고 있다!"+ "\n(z나 enter를 입력)");
                            aaa = Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetMouseButton(0);
                            yield return null;
                        } 
                        i = 0;
                    }
                    else
                    {
                        toChangeP = i-1;
                        myMove = 2;
                        Battle = BattleButtonState.NextState;
                    }
                }
            }
            else if (Battle == BattleButtonState.OneOutState) 
            {
                Rect StuPos1 = new Rect(cam.pixelWidth * 1 / 3, cam.pixelHeight / 9, cam.pixelWidth / 3, cam.pixelHeight / 9);
                Rect StuPos2 = new Rect(cam.pixelWidth * 1 / 3, cam.pixelHeight * 2 / 9, cam.pixelWidth / 3, cam.pixelHeight / 9);
                Rect StuPos3 = new Rect(cam.pixelWidth * 1 / 3, cam.pixelHeight * 3 / 9, cam.pixelWidth / 3, cam.pixelHeight / 9);
                Rect StuPos4 = new Rect(cam.pixelWidth * 1 / 3, cam.pixelHeight * 4 / 9, cam.pixelWidth / 3, cam.pixelHeight / 9);
                Rect StuPos5 = new Rect(cam.pixelWidth * 1 / 3, cam.pixelHeight * 5 / 9, cam.pixelWidth / 3, cam.pixelHeight / 9);
                Rect StuPos6 = new Rect(cam.pixelWidth * 1 / 3, cam.pixelHeight * 6 / 9, cam.pixelWidth / 3, cam.pixelHeight / 9);
                int[] availableStu = new int[7] { 0, 0, 0, 0, 0, 0, 0 }; //get info whether player can change student to it
                GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "새로 내보낼 학생을 고르자" + "");

                //학생 리스트에 들어있는 학생들이 존재하는지, 기절했는지 알아보는 반복문이다.
                for (int j = 0; j < 6; j++)
                {
                    if (MineStudentList[j] == null)
                        availableStu[j] = 1;
                    else if (MineStudentList[j].retStuStatus() == status.faint)
                        availableStu[j] = 2;
                    availableStu[6] += availableStu[j];
                }
                int i = 0;
                //아래서 부터는 존재하는 학생마다 학생의 이름, 학생의 체력 정보, 학생의 레벨, 학생의 상태를 출력한다.
                if (availableStu[0] != 1)               {
                    if (GUI.Button(StuPos1, MineStudentList[0].retStudentName() + "HP : " + MineStudentList[0].getHP() + "/" + MineStudentList[0].getMAXHP() + " " + MineStudentList[0].retStuStatus().ToString()))
                        i = 1;
                }                if (availableStu[1] != 1)                {
                    if (GUI.Button(StuPos2, MineStudentList[1].retStudentName() + "HP : " + MineStudentList[1].getHP() + "/" + MineStudentList[1].getMAXHP() + " " + MineStudentList[1].retStuStatus().ToString()))
                        i = 2;
                }                if (availableStu[2] != 1)                {
                    if (GUI.Button(StuPos3, MineStudentList[2].retStudentName() + "HP : " + MineStudentList[2].getHP() + "/" + MineStudentList[2].getMAXHP() + " " + MineStudentList[2].retStuStatus()))
                        i = 3;
                }                if (availableStu[3] != 1)                {
                    if (GUI.Button(StuPos4, MineStudentList[3].retStudentName() + "HP : " + MineStudentList[3].getHP() + "/" + MineStudentList[3].getMAXHP() + " " + MineStudentList[3].retStuStatus().ToString()))
                        i = 4;
                }                if (availableStu[4] != 1)                {
                    if (GUI.Button(StuPos5, MineStudentList[4].retStudentName() + "HP : " + MineStudentList[4].getHP() + "/" + MineStudentList[4].getMAXHP() + " " + MineStudentList[4].retStuStatus().ToString()))
                        i = 5;
                }                if (availableStu[5] != 1)                {
                    if (GUI.Button(StuPos6, MineStudentList[5].retStudentName() + "HP : " + MineStudentList[5].getHP() + "/" + MineStudentList[5].getMAXHP() + " " + MineStudentList[5].retStuStatus().ToString()
                                   ))
                        i = 6;
                }
                if (i != 0)
                {
                    //선택한 학생이 이미 나가 있다면
                    if (MineStudentList[i-1] == CurrentMine)
                    {
                        bool aaa = false;
                        while (!aaa)
                        {
                            GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "그 학생은 이미 싸우고 있다!\n(z나 enter를 입력)");
                            aaa = Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetMouseButton(0);
                            yield return null;
                        }
                        i = 0;
                    }
                    //선택한 버튼이 기절한 학생이라면
                    else if (MineStudentList[i-1].retStuStatus() == status.faint)
                    {
                        bool aaa = false;
                        while (!aaa)
                        {
                            GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "그 학생은 이미 지쳐 있다!" + "\n(z나 enter를 입력)");
                            aaa = Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetMouseButton(0);
                            yield return null;
                        }
                        i = 0;
                    }
                    else
                    {
                
                        toChangeP = i - 1;
                        myMove = 0;
                        CurrentMine = MineStudentList[toChangeP];
                
                        aba = false;
                        while (!aba)
                        {
                            GUI.Box(new Rect(1, Screen.height - 100, 600, 100), CurrentMine.retStudentName() + "을 내보냈다!\n" + "(z나 enter를 입력)");
                            aba = Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetMouseButton(0);
                            yield return null;
                        }
                
                        Battle = BattleButtonState.DefaultState;
                    }
                }
            }
           //if player give input, the battle proceed.
            else if (Battle == BattleButtonState.NextState)
            {
                //npc select its move.
                //calculate who is first to move.
                // enemy attacks
                int b = Mathf.FloorToInt(Random.Range(0f,2f));  //random으로 0에서 3의 값을 구한다.
                myToOppoDamage = a.BattleDamageCalculate(SkillData.retSkillInfo(CurrentEnemy.retSkillList()[b]), CurrentEnemy, CurrentMine, battleTempStat);


                // 명령을 내리고 나서 배틀을 시작하기 전에 상태이상이나 특수효과로 공격하는지 마는지 확인한다.
                int battleStartMove = a.checkBattleTurnStartEvent(CurrentMine);
                //누가 우선 시작할지 결정한다.
                int whoFirst = 0;
                if (myMove != 0)
                {
                    whoFirst = 0;
                }
                else
                {
                    whoFirst = a.CalFirstGo(CurrentMine.retStuStat(4), CurrentEnemy.retStuStat(4), battleTempStat);
                }
                if (whoFirst == 0)  //player go first
                {
                    if (myMove == 2)    //if i change character.
                    {                          //write how to change character.
                        if (toChangeP != -1)
                        {
                            CurrentMine = MineStudentList[toChangeP];
                            Debug.Log("이것을 출력하고 싶다!");
                            aba = false;
                            while (!aba)
                            {
                                GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "교수님은" + MineStudentList[toChangeP].retStudentName() + "을 내보냈다!");
                                aba = Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetMouseButton(0);
                                yield return null;
                            }
                        }
                        else
                        {
                            aba = false;
                            while (!aba)
                            {
                                GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "유효하지 않은 상태입니다.");
                                aba = Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetMouseButton(0);
                                yield return null;
                            }
                        }
                    }
                    else if (myMove == 1)
                    {
                        // write how item is used
                    }
                    else if (battleStartMove != 0)
                    {
                        //여기서 상태이상으로 행동이 종료되었을 때 메세지를 출력하면 된다.,
                        aba = false;
                        while (!aba)
                        {
                            GUI.Box(new Rect(1, Screen.height - 100, 600, 100), CurrentMine.retStudentName() + "은 움직일 수 없다." + "\n" + "(z나 enter를 입력)");
                            aba = Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetMouseButton(0);
                            yield return null;
                        }
                    }
                    else if (myMove == 0) // 내 행동이 공격일 때
                    {
                        alived = CurrentEnemy.getDamage(damage);
                        Debug.Log("아군이 먼저 공격을 시작했다.");
                        aba = false;
                        while (!aba)
                        {
                            GUI.Box(new Rect(1, Screen.height - 100, 600, 100), CurrentMine.retStudentName() + "은 " + damage.ToString() + "만큼의 데미지를 주었다.\n" + "(z나 enter를 입력)");
                            aba = Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetMouseButton(0);
                            yield return null;
                        }

                        if (alived == 1) //if enemy student fainted.
                        {
                            aba = false;
                            while (!aba)
                            {
                                GUI.Box(new Rect(1, Screen.height - 100, 600, 100), CurrentEnemy.retStuIndex().ToString() + "학생은 " + damage.ToString() + "쓰러졌다" + "\n(z나 enter를 입력)");
                                aba = Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetMouseButton(0);
                                yield return null;
                            }

                            /* get experience */
                            int exp = CurrentEnemy.getLevel() * Random.Range(8, 35);
                            CurrentMine.setExp(exp);

                            aba = false;
                            while (!aba)
                            {
                                GUI.Box(new Rect(1, Screen.height - 100, 600, 100), CurrentMine.retStudentName()+"은 " + exp.ToString() + "만큼의 경험치를 얻었다." + "\n(z나 enter를 입력)");
                                aba = Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetMouseButton(0);
                                yield return null;
                            }

                            int i = outOfStudent(EnemyStudentList); //전투가 종료되었는지 확인한다.
                            if (i == 0) //전투가 종료되었을 때 
                            {
                                HowBattleEnd = 2;
                                Battle = BattleButtonState.RunState;
                            }
                            else
                            {
                                CurrentEnemy = EnemyStudentList[i-1];
                                Battle = BattleButtonState.DefaultState;
                            }
                        }
                    }
                    if (Battle == BattleButtonState.NextState)
                    {  //enemy attacks.
                        battleStartMove = a.checkBattleTurnStartEvent(CurrentEnemy);
                        if (battleStartMove != 0)
                        {
                            aba = false;

                            while (!aba)
                            {
                                GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "상대는 움직일 수 없었다" + "\n(z나 enter를 입력)");
                                aba = Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetMouseButton(0);
                                yield return null;
                            }
                            //여기서 상태이상으로 행동이 종료되었을 때 메세지를 출력하면 된다.,
                        }
                        else
                        {
                            alived = CurrentMine.getDamage(myToOppoDamage);
                            aba = false;
                            while (!aba)
                            {
                                GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "상대는 " + myToOppoDamage.ToString() + "만큼의 피해를 주었다." + "\n(z나 enter를 입력)");
                                aba = Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetMouseButton(0);
                                yield return null;
                            }
                        }

                        if (alived == 1)    // if our student dead.
                        {

                            CurrentMine.giveAStatus(status.faint);
                            aba = false;
                            while (!aba)
                            {
                                GUI.Box(new Rect(1, Screen.height - 100, 600, 100), CurrentMine.retStudentName() + "은 쓰러졌다." + "\n(z나 enter를 입력)");
                                aba = Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetMouseButton(0);
                                yield return null;
                            }
                            //check battle end;
                            int i = outOfStudent(MineStudentList); //전투가 종료되었는지 확인한다.

                            if (i == 0)
                            {
                                //battle ended.
                                HowBattleEnd = 1;
                                Battle = BattleButtonState.RunState;
                            }
                            else
                            {
                                // 학생이 반드시 학생을 교체하게 만들어야 한다.
                                Battle = BattleButtonState.ExchangeState;
                            }
                        }
                        Battle = BattleButtonState.DefaultState;
                    }
                }
                else  // oppponent go first
                {
                    //enemy attacks.
                    Debug.Log("상대가 먼저 공격을 시작했다.");
                    battleStartMove = a.checkBattleTurnStartEvent(CurrentEnemy);
                    if (battleStartMove != 0)
                    {
                        aba = false;
                        while (!aba)
                        {
                            GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "상대는 움직일 수 없었다" + "\n(z나 enter를 입력)");
                            aba = Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetMouseButton(0);
                            yield return null;
                        }
                        //여기서 상태이상으로 행동이 종료되었을 때 메세지를 출력하면 된다.,
                    }
                    else if (true)
                    {
                        alived = CurrentMine.getDamage(myToOppoDamage);
                        aba = false;
                        while (!aba)
                        {
                            GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "상대는 " + myToOppoDamage.ToString() + "만큼의 피해를 주었다." + "\n(z나 enter를 입력)");
                            aba = Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetMouseButton(0);
                            yield return null;
                        }
                    }
                    if (alived == 1)    // if our student dead.
                    {

                        CurrentMine.giveAStatus(status.faint);
                        aba = false;
                        while (!aba)
                        {
                            GUI.Box(new Rect(1, Screen.height - 100, 600, 100), CurrentMine.retStudentName() + "은 쓰러졌다." + "\n(z나 enter를 입력)");
                            aba = Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetMouseButton(0);
                            yield return null;
                        }
                        //check battle end;
                        int i = outOfStudent(MineStudentList); //전투가 종료되었는지 확인한다.

                        if (i == 0)
                        {
                            //battle ended.
                            HowBattleEnd = 1;
                            Battle = BattleButtonState.RunState;
                        }
                        else
                        {
                            // 학생이 반드시 학생을 교체하게 만들어야 한다.
                            Battle = BattleButtonState.OneOutState;
                        }
                    }
                    if (Battle == BattleButtonState.NextState)
                    {
                        battleStartMove = a.checkBattleTurnStartEvent(CurrentMine);
                        if (myMove == 2)    //if i change character.
                        {

                            CurrentMine = MineStudentList[toChangeP];
                            if (toChangeP != -1)
                            {
                                aba = false;
                                while (!aba)
                                {
                                    GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "교수님은" + MineStudentList[toChangeP].retStudentName() + "을 내보냈다!");
                                    aba = Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetMouseButton(0);
                                    yield return null;
                                }
                            }
                            else
                            {
                                aba = false;
                                while (!aba)
                                {
                                    GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "유효하지 않은 상태입니다.");
                                    aba = Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetMouseButton(0);
                                    yield return null;
                                }
                            }
                        }
                        else if (myMove == 1)
                        {
                            // write how item is used
                        }
                        else if (battleStartMove != 0)
                        {
                            //여기서 상태이상으로 행동이 종료되었을 때 메세지를 출력하면 된다.,
                            aba = false;
                            while (!aba)
                            {
                                GUI.Box(new Rect(1, Screen.height - 100, 600, 100), CurrentMine.retStuIndex().ToString() + "번 학생은 움직일 수 없다." + "\n(z나 enter를 입력)");
                                aba = Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetMouseButton(0);
                                yield return null;
                            }
                        }
                        else if (myMove == 0) // 내 행동이 공격일 때
                        {
                            alived = CurrentEnemy.getDamage(damage);

                            aba = false;
                            while (!aba)
                            {
                                GUI.Box(new Rect(1, Screen.height - 100, 600, 100), CurrentMine.retStudentName() + "은 " + damage.ToString() + "만큼의 데미지를 주었다." + "\n(z나 enter를 입력)");
                                aba = Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetMouseButton(0);
                                yield return null;
                            }

                            if (alived == 1) //if enemy student fainted.
                            {
                                aba = false;
                                while (!aba)
                                {
                                    GUI.Box(new Rect(1, Screen.height - 100, 600, 100), CurrentEnemy.retStudentName() + "은 " + damage.ToString() + "쓰러졌다" + "\n(z나 enter를 입력)");
                                    aba = Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetMouseButton(0);
                                    yield return null;
                                }

                                CurrentMine.setExp(CurrentEnemy.getExp()); //give exp;

                                aba = false;
                                while (!aba)
                                {
                                    GUI.Box(new Rect(1, Screen.height - 100, 600, 100), CurrentEnemy.retStuIndex().ToString() + "학생은 " + CurrentEnemy.getExp().ToString() + "만큼의 경험치를 얻었다." + "\n(z나 enter를 입력)");
                                    aba = Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetMouseButton(0);
                                    yield return null;
                                }

                                int i = outOfStudent(EnemyStudentList); //전투가 종료되었는지 확인한다.
                                if (i == 0) //전투가 종료되었을 때 
                                {
                                    HowBattleEnd = 2;
                                    Battle = BattleButtonState.RunState;
                                }
                                else
                                {
                                    CurrentEnemy = EnemyStudentList[i];
                                    Battle = BattleButtonState.DefaultState;
                                }
                
                            }
                            Battle = BattleButtonState.DefaultState;
                        }
                    }
                }
            }
            yield return null;
        }
    }

}
