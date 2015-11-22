﻿using UnityEngine;
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
    public StudentInfo Mine;
    public StudentInfo Enemy;
    BattleScene a = new BattleScene();
    string alphahah = "fire blast";
    // Use this for initialization
    void Start()
    {
        Mine = new StudentInfo();
        Enemy = new StudentInfo();
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
        int damage = 0;
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
        if (Battle == BattleButtonState.NextState)
        {
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