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
    //opponent students "same" 
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
    public ChangeCharacter studentInfo;
    public Student[] EnemyStudentList;

    // get the every battle 
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
        int damage = 0; //save the damage that student give to opponent.
        int MyStudentAlived = 0; //save the info wether player student is dead.
        int OppoStudentAlived = 0; // save the info wether opponent student is dead.

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
                Application.LoadLevel(1);
            }
        }
        if (Battle == BattleButtonState.AttackState)
        {
            int alived = 0;
            if (GUI.Button(FirstPos,Mine.skillList[0].skillName))
            {
                damage = a.BattleDamageCalculate(Mine.skillList[0], Mine, Enemy);
                alived = Mine.getDamage(damage);
                Battle = BattleButtonState.NextState;
            }
            if (GUI.Button(SecondPos, Mine.skillList[1].skillName))
            {
                damage = a.BattleDamageCalculate(Mine.skillList[1], Mine, Enemy);
                alived = Mine.getDamage(damage);
                Battle = BattleButtonState.NextState;
            }
            if (GUI.Button(ThirdPos, Mine.skillList[2].skillName))
            {
                damage = a.BattleDamageCalculate(Mine.skillList[2], Mine, Enemy);
                alived = Enemy.getDamage(damage);
            }

            if (GUI.Button(ForthPos, Mine.skillList[3].skillName))
            {
                damage = a.BattleDamageCalculate(Mine.skillList[3], Mine, Enemy);
                alived = Enemy.getDamage(damage);
            }
        }

        //if player give input, the battle proceed.
        if (Battle == BattleButtonState.NextState)
        {
            //npc select its move.
            //
            //calculate who is first to move.

/*            if (alived != 0)    //my student is dead
            {
                //change pokemon;
            }
            else // enemy attacks
            {
                int b = (int)Random.value;  //random으로 0에서 3의 값을 구한다.
                damage = a.BattleDamageCalculate(Enemy.skillList[b], Enemy, Mine);
                alived = Mine.getDamage(damage);
            }
            if (alived != 0)    // my student is dead.
            {
                //change pokemon;
            }
        
   */     
        
        }

    }
}
