﻿using UnityEngine;
using System.Collections;

//manage game menu and game background and game object, battle flow
public class BattleButtonManage : MonoBehaviour
{
    // get the screen info.
    public Camera cam;
    public int attackstate = 0;
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
    public int whoFirst = 0;
    public SkillInfo mystuSkill;
    public SkillInfo EnemySkill;
    public bool Iattack = false;
    //save information about battle end. 0 is run, 1 is defeated by npc, 2 is win
    int HowBattleEnd = 0;
    public bool WhenMouseDown = false;

    //operand for inventory
    int toolbarint = 0;
    int item_gridint = 0;
    int stu_toolbarint = 0;
    bool use_item = false;
    Item use_for_prit;
  
    // set the postion of buttons.
    Rect FirstPos;
    Rect SecondPos;
    Rect ThirdPos;
    Rect ForthPos;

    /* MyProInfo */
    CharacterStatus MyProfInfo;

    //set the infomation of student who will came out to battle.
    public Student CurrentMine;
    public Student CurrentEnemy;

    //set the information of 
    public Student[] MineStudentList;
    public Student[] EnemyStudentList;
    
    IEnumerator TestCode;
    IEnumerator aTextTime;

    //information about skill

    // get the battle related code 
    BattleScene a = new BattleScene();

    int info_usedSkill;
    // Use this for initialization. get information from 
    void Start()
    {
        //이 코드가 startScene으로 시작해야만 사용 가능함.
        MyProfInfo = GameManager.pl_stored.GetComponent<CharacterStatus>();

        //init battleTempStat
        for (int i = 0; i < 12; i++)
            battleTempStat[i] = 0;
        //MineStudentList = GetComponent<CharacterStatus>().getStuList();
        //StudentInfo getnew = new StudentInfo(); doesn't used
        MineStudentList = gameObject.GetComponent<StudentInfo>().myDebugStuList;
        EnemyStudentList = gameObject.GetComponent<StudentInfo>().enemyDebugStuList;
        CurrentMine = gameObject.GetComponent<StudentInfo>().retStuData(0, 0);
        CurrentEnemy = gameObject.GetComponent<StudentInfo>().retStuData(1, 0);
        TestCode = WholeBattleCode();
       
    }

    // Update is called once per frame
    void Update()
    {
        int find = outOfStudent(MineStudentList); //전투가 종료되었는지 확인한다.
        if (find == 0)
        {
            HowBattleEnd = 1;
            Battle = BattleButtonState.RunState;
        }
        else if (CurrentMine.getHP() < 0.5 && Battle != BattleButtonState.RunState)
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
       if (TestCode != null)
           TestCode.MoveNext();
    }

    int outOfStudent(Student[] list)
    { 
        for(int i = 0 ; i < 6 ; i ++ )
        {
            if(list[i] != null) {
                if (list[i].retStuStatus() != status.faint && list[i].getHP() > 0.5f)
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
                if (GUI.Button(FirstPos, "공격")) { Iattack = true;  Battle = BattleButtonState.AttackState; }
                if (GUI.Button(SecondPos, "학생 교체")) { Iattack = false;  Battle = BattleButtonState.ExchangeState; }
                if (GUI.Button(ThirdPos, "아이템 사용")) { Iattack = false;  Battle = BattleButtonState.ItemState; }
                if (GUI.Button(ForthPos, "도망치기")) { Iattack = false;  Battle = BattleButtonState.RunState; }
                if (GUI.Button(FifthPos, "교수의 권한으로 승리")) { Iattack = false; Battle = BattleButtonState.RunState; HowBattleEnd = 2; }
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
                else if (HowBattleEnd == 0) //전투에서 도망쳤을 때
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
                else if (HowBattleEnd == 5) {
                    GameManager.pl_stored.SetActive(true);
                    Application.LoadLevel(GameManager.PrevSceneNumber);
                }
            }
            else if (Battle == BattleButtonState.AttackState)
            {
                GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "어떻게 공격을 할 것인가?" + "\n" + "(x 버튼으로 돌아갈 수 있습니다.)");
                
                alived = 0;
                if (CurrentMine.retSkillList()[0] != null)
                {
                    if ((CurrentMine.retSkillList()[0].retNowChance() != 0) && GUI.Button(FirstPos, CurrentMine.retSkillList()[0].retSkillName() + "\n" + CurrentMine.retSkillList()[0].retNowChance() + "/" + CurrentMine.retSkillList()[0].retMaxChance()))
                    {
                        damage = a.BattleDamageCalculate(CurrentMine.retSkillList()[0], CurrentMine, CurrentEnemy, battleTempStat);
                        mystuSkill = CurrentMine.retSkillList()[0];
                        Battle = BattleButtonState.NextState;
                        myMove = 0;
                        info_usedSkill = 0;
                    }
                }
                if (CurrentMine.retSkillList()[1] != null)
                {
                    if ((CurrentMine.retSkillList()[1].retNowChance() != 0) && GUI.Button(SecondPos, CurrentMine.retSkillList()[1].retSkillName() + "\n" + CurrentMine.retSkillList()[1].retNowChance() + "/" + CurrentMine.retSkillList()[1].retMaxChance()))
                    {
                        damage = a.BattleDamageCalculate(CurrentMine.retSkillList()[1], CurrentMine, CurrentEnemy, battleTempStat);
                        mystuSkill = CurrentMine.retSkillList()[1];
                        Battle = BattleButtonState.NextState;
                        myMove = 0;
                        info_usedSkill = 1;
                    }
                }
                if (CurrentMine.retSkillList()[2] != null)
                {
                    if ((CurrentMine.retSkillList()[2].retNowChance() != 0) && GUI.Button(ThirdPos, CurrentMine.retSkillList()[2].retSkillName() + "\n" + CurrentMine.retSkillList()[2].retNowChance() + "/" + CurrentMine.retSkillList()[2].retMaxChance()))
                    {
                        damage = a.BattleDamageCalculate(CurrentMine.retSkillList()[2], CurrentMine, CurrentEnemy, battleTempStat);
                        mystuSkill = CurrentMine.retSkillList()[2];
                        Battle = BattleButtonState.NextState;
                        myMove = 0;
                        info_usedSkill = 2;
                    }
                }
                if (CurrentMine.retSkillList()[3] != null)
                {
                    if ((CurrentMine.retSkillList()[3].retNowChance() != 0) && GUI.Button(ForthPos, CurrentMine.retSkillList()[3].retSkillName() + "\n" + CurrentMine.retSkillList()[3].retNowChance() + "/" + CurrentMine.retSkillList()[3].retMaxChance()))
                    {

                        damage = a.BattleDamageCalculate(CurrentMine.retSkillList()[3], CurrentMine, CurrentEnemy, battleTempStat);
                        mystuSkill = CurrentMine.retSkillList()[3];
                        Battle = BattleButtonState.NextState;
                        myMove = 0;
                        info_usedSkill = 3;
                    }
                }
            }
            else if (Battle == BattleButtonState.ItemState)
            {
                if (true)
                {

                    string[] toolbar_str = { "Character", "Student", "Item" };
                    string text = "아이템 설명";
                    string text2;
                    Item[] inventory = MyProfInfo.returnInven();
                    Vector2 scroll = Vector2.zero;
                    int money = MyProfInfo.retMoney();

                    string[] stu_toolbar = new string[6];
                    string[] item_grid = new string[256];

                    Student[] student_list = MineStudentList;
                    GUIStyle myStyle = new GUIStyle(GUI.skin.box);
                    myStyle.fontSize = 25;
                    myStyle.font = (Font)Resources.Load("NANUMBARUNGOTHICBOLD", typeof(Font));
                    myStyle.alignment = TextAnchor.UpperLeft;
                    myStyle.normal.textColor = Color.white;
                    GUIStyle itemStyle = new GUIStyle(GUI.skin.box);
                    itemStyle.fontSize = 15;
                    itemStyle.font = (Font)Resources.Load("NANUMBARUNGOTHICBOLD", typeof(Font));
                    itemStyle.alignment = TextAnchor.UpperLeft;
                    itemStyle.normal.textColor = Color.white;

                    /* Status Menu Init */
                    GUI.Box(new Rect(Screen.width / 8, Screen.height / 8, (Screen.width * 6) / 8, (Screen.height * 6) / 7), "Info");
                    toolbarint = GUI.Toolbar(new Rect(Screen.width / 6, Screen.height / 5, (Screen.width * 3) / 10, (Screen.height * 3) / 40), toolbarint, toolbar_str);
                    scroll = GUI.BeginScrollView(new Rect(Screen.width / 5, (Screen.height / 3) - 10, (Screen.width * 3) / 5, (Screen.height * 3) / 5), scroll, new Rect(0, 0, Screen.width, Screen.height));

                    /* case Student */
                    if (toolbarint == 0)
                    {
                        GUI.Box(new Rect(Screen.width / 20, (Screen.height / 20) + 30, (Screen.width * 9) / 10, Screen.height / 2),
                            "소지금 : " + money.ToString() + "\n", myStyle);
                    }
                    else if (toolbarint == 1 && student_list[0] != null)
                    {
                        for (int i = 0; student_list[i] != null; i++)
                        {
                            stu_toolbar[i] = student_list[i].getDept();
                        }
                        stu_toolbarint = GUI.Toolbar(new Rect(0, 0, Screen.width / 2, 30), stu_toolbarint, stu_toolbar);
                        for (int i = 0; student_list[i] != null; i++)
                        {
                            if (stu_toolbarint == i)
                            {
                                GUI.Box(new Rect(0, 40, (Screen.width * 9) / 10, Screen.height / 2),
                                    "Level : " + student_list[i].getLevel().ToString() + "\n" +
                                    "HP : " + student_list[i].getHP().ToString() + "/" + student_list[i].getMAXHP().ToString() + "\n" +
                                    "Exp : " + student_list[i].getExp().ToString() + "/" + "100" + "\n" +
                                    "Status : " + student_list[i].retStuStatus().ToString() + "\n" +
                                    "Attack : " + student_list[i].retStuStat(0).ToString() + "\n" +
                                    "Special Attack : " + student_list[i].retStuStat(1).ToString() + "\n" +
                                    "Defence : " + student_list[i].retStuStat(2).ToString() + "\n" +
                                    "Special Defence : " + student_list[i].retStuStat(3).ToString() + "\n" +
                                    "Speed : " + student_list[i].retStuStat(4).ToString() + "\n", itemStyle);
                            }
                        }
                    }
                    /* case Item */
                    else if (toolbarint == 2 && inventory[0] != null)
                    {
                        int k;
                        for (k = 0; inventory[k] != null; k++)
                        {
                            if (inventory[k].getitem_Amount() <= 0)
                                MyProfInfo.removeItemToInventory(inventory[k]);
                            if (inventory[k] == null) break;

                        }
                        if (k == 0) item_grid[0] = null;
                        else
                            item_grid = new string[k];


                        for (int i = 0; i < k; i++)
                        {

                            if (inventory[i].getitem_Amount() > 0)
                            {
                                item_grid[i] = inventory[i].getName();
                            }

                        }

                        item_gridint = GUI.SelectionGrid(new Rect(0, 0, (Screen.width / 2), 25 * (((k + 1) / 2))), item_gridint, item_grid, 2);

                        for (int i = 0; inventory[i] != null; i++)
                        {
                            if (item_gridint == i)
                            {
                                if (GUI.Button(new Rect(0, 25 * (((k + 1) / 2)) + 10, 50, 25), "Use"))
                                {
                                    myMove = 1;
                                    /* Use Item, cannot use capture item in character status context */
                                    if (inventory[i].getType() != type.capture) //사용할 아이템이 포획 아이템이 아니라면
                                    {
                                        use_for_prit = inventory[i];
                                        use_item = true;
                                    }
                                    else 
                                    {  //사용할 아이템이 포획아이템이라면
                                        if (GameManager.getBattleHostNum() != 0 ) // NPC와 싸우고 있는 경우, 불가능하다.
                                        {
                                            bool aaa = false;
                                            yield return null;
                                            while (!aaa)
                                            {
                                                GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "다른 교수의 학생을 회유할 수는 없어.");
                                                if (GUI.Button(FirstPos, "다음"))
                                                    aaa = true;
                                                yield return null;
                                            }
                                        }
                                        else {
                                            Battle = BattleButtonState.NextState;
                                            myMove = 1;
                                            use_for_prit = inventory[i];
                                        }
                                    }
                                }
                                text = "아이템 설명";
                                text2 = "\n";

                                text = text + "\n\n 아이템 이름 : " + inventory[i].getName();
                                text = text + "\n\n 아이템 종류 : ";
                                if (inventory[item_gridint].getType() == type.skill)
                                    text = text + "기술 머신";
                                else if (inventory[item_gridint].getType() == type.cure)
                                    text = text + "치료 아이템";
                                else if (inventory[item_gridint].getType() == type.capture)
                                    text = text + "포획 아이템";
                                text = text + "\n\n 설명 : " + inventory[item_gridint].getInfo();
                                text = text + "\n\n 수량 : " + inventory[item_gridint].getitem_Amount().ToString();
                                if (inventory[item_gridint].getAmount() > 0)
                                    text = text + "\n\n 회복량 : " + inventory[item_gridint].getAmount().ToString();

                                GUI.Box(new Rect(60, 25 * (((k + 1) / 2)) + 10, Screen.width - 60, Screen.height - (25 * (((k + 1) / 2)) + 10)),
                                    text, itemStyle);
                            }
                        }
                    }
                    GUI.EndScrollView();
                    if (use_item)
                    {
                        int i = 0;
                        GUI.Box(new Rect(1, Screen.height - 100, 600, 100),"아이템을 사용할 학생을 선택하십시오.");

                        int[] availableStu = new int[7] { 0, 0, 0, 0, 0, 0, 0 }; //get info whether player can change student to it
                
                        for (int j = 0; j < 6; j++)
                        {
                            if (MineStudentList[j] == null)
                                availableStu[j] = 1;
                            else if (MineStudentList[j].retStuStatus() == status.faint)
                                availableStu[j] = 2;
                            availableStu[6] += availableStu[j];
                        }
                        int usedtarget = 0;
                        for (int iterat = 0; iterat < 6; iterat++)
                        {
                            Rect StuPos1 = new Rect(cam.pixelWidth * 1 / 3, cam.pixelHeight * (iterat + 1) / 9, cam.pixelWidth / 3, cam.pixelHeight / 9);
                            if (availableStu[iterat] != 1)
                            {
                                if (GUI.Button(StuPos1, MineStudentList[iterat].retStudentName() + "HP : " + MineStudentList[iterat].getHP() + "/" + MineStudentList[iterat].getMAXHP() + "\n" +"상태이상: " +MineStudentList[iterat].retStuStatus().ToString()))
                                    usedtarget = iterat+1;
                            }   
                        }
                        if (usedtarget != 0)
                        {
                            Battle = BattleButtonState.NextState;
                            myMove = 1;
                            toChangeP = usedtarget - 1;
                            use_item = false;
                            usedtarget = 0;
                        }
                    }
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
                    if (GUI.Button(StuPos1, MineStudentList[0].retStudentName() + "HP : " + MineStudentList[0].getHP() + "/" + MineStudentList[0].getMAXHP() + " " + MineStudentList[0].retStuStatus().ToString()))
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
                    if (MineStudentList[i - 1] == CurrentMine)
                    {

                        bool aaa = false;
                        yield return null;
                        while (!aaa)
                        {
                            GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "그 학생은 이미 싸우고 있다!\n(버튼을 눌러서 진행)");
                            if (GUI.Button(FirstPos, "다음"))
                                aaa = true;
                            yield return null;
                        }
                        i = 0;
                    }
                    //선택한 버튼이 기절한 학생이라면
                    else if (MineStudentList[i - 1].retStuStatus() == status.faint || MineStudentList[i - 1].getHP() == 0)
                    {

                        bool aaa = false;
                        yield return null;
                        while (!aaa)
                        {
                            GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "그 학생은 싸우기엔 너무 지쳐있다." + "(\n버튼을 눌러서 진행)");
                            if (GUI.Button(FirstPos, "다음"))
                                aaa = true;
                            yield return null;
                        }
                        i = 0;
                    }
                    else
                    {
                        toChangeP = i - 1;
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
                if (availableStu[0] != 1)
                {
                    if (GUI.Button(StuPos1, MineStudentList[0].retStudentName() + "HP : " + MineStudentList[0].getHP() + "/" + MineStudentList[0].getMAXHP() + " " + MineStudentList[0].retStuStatus().ToString()))
                        i = 1;
                } if (availableStu[1] != 1)
                {
                    if (GUI.Button(StuPos2, MineStudentList[1].retStudentName() + "HP : " + MineStudentList[1].getHP() + "/" + MineStudentList[1].getMAXHP() + " " + MineStudentList[1].retStuStatus().ToString()))
                        i = 2;
                } if (availableStu[2] != 1)
                {
                    if (GUI.Button(StuPos3, MineStudentList[2].retStudentName() + "HP : " + MineStudentList[2].getHP() + "/" + MineStudentList[2].getMAXHP() + " " + MineStudentList[2].retStuStatus()))
                        i = 3;
                } if (availableStu[3] != 1)
                {
                    if (GUI.Button(StuPos4, MineStudentList[3].retStudentName() + "HP : " + MineStudentList[3].getHP() + "/" + MineStudentList[3].getMAXHP() + " " + MineStudentList[3].retStuStatus().ToString()))
                        i = 4;
                } if (availableStu[4] != 1)
                {
                    if (GUI.Button(StuPos5, MineStudentList[4].retStudentName() + "HP : " + MineStudentList[4].getHP() + "/" + MineStudentList[4].getMAXHP() + " " + MineStudentList[4].retStuStatus().ToString()))
                        i = 5;
                } if (availableStu[5] != 1)
                {
                    if (GUI.Button(StuPos6, MineStudentList[5].retStudentName() + "HP : " + MineStudentList[5].getHP() + "/" + MineStudentList[5].getMAXHP() + " " + MineStudentList[5].retStuStatus().ToString()))
                        i = 6;
                }
                if (i != 0)
                {
                    //선택한 학생이 이미 나가 있다면
                    if (MineStudentList[i - 1] == CurrentMine)
                    {
                        bool aaa = false;
                        yield return null;
                        while (!aaa)
                        {
                            GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "그 학생은 이미 싸우고 있다!\n(버튼을 눌러서 진행)");
                            if (GUI.Button(FirstPos, "다음"))
                                aaa = true;
                            yield return null;
                        }
                        i = 0;
                    }
                    //선택한 버튼이 기절한 학생이라면
                    else if (MineStudentList[i - 1].retStuStatus() == status.faint || MineStudentList[i - 1].getHP() == 0)
                    {
                        bool aaa = false;
                        yield return null;
                        while (!aaa)
                        {
                            GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "그 학생은 이미 지쳐 있다!" + "\n(버튼을 눌러서 진행)");
                            if (GUI.Button(FirstPos, "다음"))
                                aaa = true;
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
                        yield return null;
                        while (!aba)
                        {
                            GUI.Box(new Rect(1, Screen.height - 100, 600, 100), CurrentMine.retStudentName() + "을 내보냈다!" + "\n(버튼을 눌러서 진행)");
                            if (GUI.Button(FirstPos, "다음"))
                                aba = true;
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
                int b = Mathf.FloorToInt(Random.Range(0f, 2f));  //random으로 0에서 3의 값을 구한다.
                myToOppoDamage = a.BattleDamageCalculate(CurrentEnemy.retSkillList()[b], CurrentEnemy, CurrentMine, battleTempStat);
                EnemySkill = CurrentEnemy.retSkillList()[b];

                // 명령을 내리고 나서 배틀을 시작하기 전에 상태이상이나 특수효과로 공격하는지 마는지 확인한다.
                int battleStartMove = a.checkBattleTurnStartEvent(CurrentMine);
                //누가 우선 시작할지 결정한다.

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
                            yield return null;
                            while (!aba)
                            {
                                GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "뽀교수는" + MineStudentList[toChangeP].retStudentName() + "을 내보냈다!\n버튼을 눌러서 진행");
                                if (GUI.Button(FirstPos, "다음"))
                                    aba = true;
                                yield return null;
                            }
                        }
                        else
                        {
                            aba = false;
                            yield return null;
                            while (!aba)
                            {
                                GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "유효하지 않은 상태입니다.\n버튼을 눌러서 진행");
                                if (GUI.Button(FirstPos, "다음"))
                                    aba = true;
                                yield return null;
                            }
                        }
                    }
                    else if (myMove == 1)
                    {
                        // write how item is used
                        if (use_for_prit.getType() == type.capture)
                        { //포흭 아이템 을 사용했다.
                            aba = false;
                            yield return null;
                            while (!aba)
                            {
                                GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "교수는 " + use_for_prit.getName()+"을 사용했다!");
                                if (GUI.Button(FirstPos, "다음"))
                                    aba = true;
                                yield return null;
                            }
                            if (GameManager.getBattleHostNum() != 0)
                            {   //지금이 플레이어와 전투중일 경우 상대 포켓몬은 잡을 수 없다.
                                aba = false;
                                yield return null;
                                while (!aba)
                                {
                                    GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "하지만 이미 상대는 다른 교수가 지도하고 있는 학생이다.");
                                    if (GUI.Button(FirstPos, "다음"))
                                        aba = true;
                                    yield return null;
                                }
                            }
                            else if (MineStudentList[5] != null)
                            {    //학생을 너무 많이 데이고 다니고 있다!
                                aba = false;
                                yield return null;
                                while (!aba)
                                {
                                    GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "하지만 지도학생이 너무 많이 학생을 붙잡을 수 없었다.");
                                    if (GUI.Button(FirstPos, "다음"))
                                        aba = true;
                                    yield return null;
                                }
                            }
                            else
                            {
                                bool i = CurrentEnemy.capture(use_for_prit);

                                aba = false;
                                yield return null;
                                while (!aba)
                                {
                                    GUI.Box(new Rect(1, Screen.height - 100, 600, 100), ".....");
                                    if (GUI.Button(FirstPos, "다음"))
                                        aba = true;
                                    yield return null;
                                }

                                if (i == true)
                                {
                                    aba = false;
                                    yield return null;
                                    while (!aba)
                                    {
                                        GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "학생을 지도학생으로 만드는데 성공했다!");
                                        if (GUI.Button(FirstPos, "다음"))
                                            aba = true;
                                        yield return null;
                                    }
                                    MyProfInfo.setStudent(CurrentEnemy);
                                    MyProfInfo.useOneItem(use_for_prit);
                                    HowBattleEnd = 5;
                                    Battle = BattleButtonState.RunState;
                                }
                                else
                                {
                                    aba = false;
                                    yield return null;
                                    while (!aba)
                                    {
                                        GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "하지만 학생을 회유할 수 없었다!");
                                        if (GUI.Button(FirstPos, "다음"))
                                            aba = true;
                                        yield return null;
                                    }
                                    MyProfInfo.useOneItem(use_for_prit);
                                }
                            }    
                        }
                        else
                        {   //포획이 아닌 다른 아이템 상태이다.
                            //아이템의 정보를 출력한다.

                            Debug.Log("교수는 일반 아이템을 사용 했다.");
                            use_for_prit.useItem(MineStudentList[toChangeP]);
                            aba = false;
                            yield return null;
                            while (!aba)
                            {
                                GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "뽀교수는 " + use_for_prit.getName() + "을 사용했다!");
                                if (GUI.Button(FirstPos, "다음"))
                                    aba = true;
                                yield return null;
                            }
                            MyProfInfo.useOneItem(use_for_prit);
                        }
                    }
                    else if (battleStartMove != 0)
                    {
                        //여기서 상태이상으로 행동이 종료되었을 때 메세지를 출력하면 된다.,

                        aba = false;
                        yield return null;
                        while (!aba)
                        {
                            GUI.Box(new Rect(1, Screen.height - 100, 600, 100), CurrentMine.retStudentName() + "은 움직일 수 없다." + "\n버튼을 눌러서 진행");
                            if (GUI.Button(FirstPos, "다음"))
                                aba = true;
                            yield return null;
                        }
                    }
                    else if (myMove == 0) // 내 행동이 공격일 때
                    {
                        alived = CurrentEnemy.getDamage(damage);
                        Debug.Log("아군이 먼저 공격을 시작했다.");

                        aba = false;
                        yield return null;
                        gameObject.GetComponent<AudioSource>().PlayOneShot(gameObject.GetComponent<AudioSource>().clip);
                        while (!aba)
                        {
                            GUI.Box(new Rect(1, Screen.height - 100, 600, 100), CurrentMine.retStudentName() + "은 " +CurrentMine.retSkillList()[info_usedSkill].retSkillName()+"을 사용했다!\n" + damage.ToString() + "만큼의 데미지를 주었다.\n버튼을 눌러서 진행");
                            if (GUI.Button(FirstPos, "다음"))
                            {
                                aba = true;
                            }
                            yield return null;
                        }

                        if (alived == 1) //if enemy student fainted.
                        {
                            aba = false;
                            yield return null;
                            while (!aba)
                            {
                                GUI.Box(new Rect(1, Screen.height - 100, 600, 100), CurrentEnemy.retStuIndex().ToString() + "학생은 " + damage.ToString() + "쓰러졌다.\n버튼을 눌러서 진행");
                                if (GUI.Button(FirstPos, "다음"))
                                    aba = true;
                                yield return null;
                            }

                            /* get experience */
                            int exp = CurrentEnemy.getLevel() * Random.Range(8, 35);
                            CurrentMine.setExp(exp);

                            aba = false;
                            yield return null;
                            while (!aba)
                            {
                                GUI.Box(new Rect(1, Screen.height - 100, 600, 100), CurrentMine.retStudentName() + "은 " + exp + "만큼의 경험치를 얻었다.\n버튼을 눌러서 진행");
                                if (GUI.Button(FirstPos, "다음"))
                                    aba = true;
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
                                CurrentEnemy = EnemyStudentList[i - 1];
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
                            yield return null;
                            while (!aba)
                            {
                                GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "상대는 움직일 수 없었다.\n버튼을 눌러서 진행");
                                if (GUI.Button(FirstPos, "다음"))
                                    aba = true;
                                yield return null;
                            }
                            //여기서 상태이상으로 행동이 종료되었을 때 메세지를 출력하면 된다.,
                        }
                        else
                        {
                            alived = CurrentMine.getDamage(myToOppoDamage);

                            aba = false;
                            yield return null;
                            gameObject.GetComponent<AudioSource>().PlayOneShot(gameObject.GetComponent<AudioSource>().clip);
                            while (!aba)
                            {
                                GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "상대는 " + CurrentEnemy.retSkillList()[b].retSkillName() + "을 사용했다!\n" + myToOppoDamage.ToString() + "만큼의 피해를 주었다.\n버튼을 눌러서 진행");
                                if (GUI.Button(FirstPos, "다음"))
                                    aba = true;
                                yield return null;
                            }
                        }

                        if (alived == 1)    // if our student dead.
                        {

                            CurrentMine.giveAStatus(status.faint);

                            aba = false;
                            yield return null;
                            while (!aba)
                            {
                                GUI.Box(new Rect(1, Screen.height - 100, 600, 100), CurrentMine.retStudentName() + "은 쓰러졌다.\n버튼을 눌러서 진행");
                                if (GUI.Button(FirstPos, "다음"))
                                    aba = true;
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

                        yield return null;
                        while (!aba)
                        {
                            GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "상대는 움직일 수 없었다.\n버튼을 눌러서 진행");
                            if (GUI.Button(FirstPos, "다음"))
                                aba = true;
                            yield return null;
                        }
                        //여기서 상태이상으로 행동이 종료되었을 때 메세지를 출력하면 된다.,
                    }
                    else if (true)
                    {
                        alived = CurrentMine.getDamage(myToOppoDamage);

                        aba = false;
                        yield return null;
                        
                        gameObject.GetComponent<AudioSource>().PlayOneShot(gameObject.GetComponent<AudioSource>().clip);
                        while (!aba)
                        {
                            GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "상대는 "  + CurrentMine.retSkillList()[b].retSkillName() + "을 했다!\n" + myToOppoDamage.ToString() + "만큼의 피해를 주었다.\n버튼을 눌러서 진행");
                            if (GUI.Button(FirstPos, "다음"))
                                aba = true;
                            yield return null;
                        }
                    }
                    if (alived == 1)    // if our student dead.
                    {

                        CurrentMine.giveAStatus(status.faint);
                        aba = false;
                        yield return null;
                        while (!aba)
                        {
                            GUI.Box(new Rect(1, Screen.height - 100, 600, 100), CurrentMine.retStudentName() + "은 쓰러졌다.\n버튼을 눌러서 진행");
                            if (GUI.Button(FirstPos, "다음"))
                                aba = true;
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
                                yield return null;
                                while (!aba)
                                {
                                    GUI.Box(new Rect(1, Screen.height - 100, 600, 100), "유효하지 않은 상태입니다.\n버튼을 눌러서 진행");
                                    if (GUI.Button(FirstPos, "다음"))
                                        aba = true;
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
                            yield return null;
                            while (!aba)
                            {
                                GUI.Box(new Rect(1, Screen.height - 100, 600, 100), CurrentMine.retStudentName() + "은 움직일 수 없다.\n버튼을 눌러서 진행");
                                if (GUI.Button(FirstPos, "다음"))
                                    aba = true;
                                yield return null;
                            }
                        }
                        else if (myMove == 0) // 내 행동이 공격일 때
                        {
                            alived = CurrentEnemy.getDamage(damage);

                            aba = false; yield return null;
                            while (!aba)
                            {
                                GUI.Box(new Rect(1, Screen.height - 100, 600, 100), CurrentMine.retStudentName() + "은 "  + CurrentMine.retSkillList()[info_usedSkill].retSkillName() + "을 했다!\n" + damage.ToString() + "만큼의 데미지를 주었다.\n버튼을 눌러서 진행");
                                if (GUI.Button(FirstPos, "다음"))
                                    aba = true;
                                yield return null;
                            }

                            if (alived == 1) //if enemy student fainted.
                            {
                                aba = false;
                                yield return null;
                                while (!aba)
                                {
                                    GUI.Box(new Rect(1, Screen.height - 100, 600, 100), CurrentEnemy.retStudentName() + "은 " + damage.ToString() + "쓰러졌다\n버튼을 눌러서 진행");
                                    if (GUI.Button(FirstPos, "다음"))
                                        aba= true;
                                    yield return null;
                                }

                                /* get experience */
                                int exp = CurrentEnemy.getLevel() * Random.Range(8, 35);
                                CurrentMine.setExp(exp);

                                aba = false; yield return null;
                                while (!aba)
                                {
                                    GUI.Box(new Rect(1, Screen.height - 100, 600, 100), CurrentMine.retStudentName()+"은 " + exp + "만큼의 경험치를 얻었다.\n버튼을 눌러서 진행");
                                    if (GUI.Button(FirstPos, "다음"))
                                        aba = true;
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
                                    CurrentEnemy = EnemyStudentList[i - 1];
                                    Battle = BattleButtonState.DefaultState;
                                }

                            }
                        }
                    }
                }

                // 전투가 종료되었고 중독으로 인한 데미지를 받을 차례이다.
                if (Battle != BattleButtonState.RunState)
                {
                    int aliminePoi = 0;
                    int alienemyPoi = 0;
                    string temp = "";
                    if (CurrentMine.retStuStatus() == status.poison)
                    {
                        aliminePoi = CurrentMine.getDamage((int)CurrentMine.getMAXHP() / 12);
                        temp = temp + CurrentMine.retStudentName() + "은 " + (int)CurrentMine.getMAXHP() / 12 + "만큼의 독 데미지를 받았다.\n";
                    }
                    if (CurrentEnemy.retStuStatus() == status.poison)
                    {
                        alienemyPoi = CurrentEnemy.getDamage((int)CurrentMine.getMAXHP() / 12);
                        temp = temp + "상대는 " + (int)CurrentMine.getMAXHP() / 12 + "만큼의 독 데미지를 받았다.\n";
                    }
                    if (CurrentEnemy.retStuStatus() == status.poison || CurrentMine.retStuStatus() == status.poison)
                    {
                        aba = false;
                        while (aba == false)
                        {
                            GUI.Box(new Rect(1, Screen.height - 100, 600, 100), temp + "버튼을 눌러서 진행");
                            if (GUI.Button(FirstPos, "다음"))
                                aba = true;
                            yield return null;
                        }
                    }

                    if (alienemyPoi == 1) //if enemy student fainted.
                    {
                        aba = false;
                        yield return null;
                        while (!aba)
                        {
                            GUI.Box(new Rect(1, Screen.height - 100, 600, 100), CurrentEnemy.retStudentName() + "은 " + damage.ToString() + "쓰러졌다\n버튼을 눌러서 진행");
                            if (GUI.Button(FirstPos, "다음"))
                                aba = true;
                            yield return null;
                        }

                        /* get experience */
                        int exp = CurrentEnemy.getLevel() * Random.Range(8, 35);
                        CurrentMine.setExp(exp);

                        aba = false; yield return null;
                        while (!aba)
                        {
                            GUI.Box(new Rect(1, Screen.height - 100, 600, 100), CurrentMine.retStudentName() + "은 " + exp + "만큼의 경험치를 얻었다.\n버튼을 눌러서 진행");
                            if (GUI.Button(FirstPos, "다음"))
                                aba = true;
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
                            CurrentEnemy = EnemyStudentList[i - 1];
                            Battle = BattleButtonState.DefaultState;
                        }

                    }
                    if (Battle != BattleButtonState.RunState)
                        Battle = BattleButtonState.DefaultState;
                }
            }
            yield return null;
        }
    }
}
