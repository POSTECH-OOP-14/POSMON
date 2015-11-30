using UnityEngine;
using System.Collections;

public class BattleButtonManage : MonoBehaviour
{
    //manage game menu and game background and game object
    public Camera cam;
    enum BattleButtonState
    {
        DefaultState,
        AttackState,
        ExchangeState,
        ItemState,
        RunState,
        NextState
    };
    private BattleButtonState Battle = BattleButtonState.DefaultState;
    Rect FirstPos;
    Rect SecondPos;
    Rect ThirdPos;
    Rect ForthPos;
    StudentInfo Mine = new StudentInfo();
    StudentInfo Enemy = new StudentInfo();
    BattleScene a = new BattleScene();
    string alphahah = "fire blast";
    // Use this for initialization
    void Start()
    {
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
        }
    }

    void OnGUI()
    {
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
                print("pixel width" + cam.pixelWidth);
                print("pixel Height" + cam.pixelHeight);
                Battle = BattleButtonState.RunState;
            }
        }
        if (Battle == BattleButtonState.AttackState)
        {
            int alived = 0;
            if (GUI.Button(FirstPos,alphahah))
            {
                
            }
            if (GUI.Button(SecondPos, Mine.skillList[1].retSkillName()))
            {
                int damage = a.BattleDamageCalculate(Mine.skillList[1], Mine, Enemy);
                alived = Enemy.getDamage(damage);
            }
            if (GUI.Button(ThirdPos, Mine.skillList[2].retSkillName()))
            {
                int damage = a.BattleDamageCalculate(Mine.skillList[2], Mine, Enemy);
                alived = Enemy.getDamage(damage);
            }

            if (GUI.Button(ForthPos, Mine.skillList[3].retSkillName()))
            {
                int damage = a.BattleDamageCalculate(Mine.skillList[3], Mine, Enemy);
                alived = Enemy.getDamage(damage);
            }
            if (alived != 0)    //my student is dead
            {
                //change pokemon;
            }
            else // enemy attacks
            {
                int b = (int)Random.value;  //random으로 0에서 3의 값을 구한다.
                int damage = a.BattleDamageCalculate(Enemy.skillList[b], Enemy, Mine);
                alived = Mine.getDamage(damage);
            }
            if (alived != 0)    // my student is dead.
            {
                //change pokemon;
            }
        }

    }
}
