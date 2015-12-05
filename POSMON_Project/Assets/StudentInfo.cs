﻿using UnityEngine;
using System.Collections;

public class StudentInfo: MonoBehaviour 
{
    //use for debug mode;
        public Student[] myDebugStuList = new Student[6];
        public Student[] enemyDebugStuList = new Student[6];

        void Awake()
         {
            for (int i = 0; i < 6; i++)
            {
                
                myDebugStuList[i] = GameManager.getBattlePlayerStudent(i);
                enemyDebugStuList[i] = GameManager.getBattleTrainerStudent(i);
            }

            for (int z = 0; z < 6; z++)
            {
                if (myDebugStuList[z] == null)
                    Debug.Log("my debug list " + z + "is null");
                if (enemyDebugStuList[z] == null)
                    Debug.Log("enemy debug list " + z + "is null");
            }
         }     

        void Start()
        {
          
        }

         public Student retStuData(int i, int j)
         {    
             if (i == 0)
                 return myDebugStuList[j];
             else
                 return enemyDebugStuList[j];
         }
}
