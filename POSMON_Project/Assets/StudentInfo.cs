﻿using UnityEngine;
using System.Collections;

public class StudentInfo: MonoBehaviour 
{
    //use for debug mode;
        public Student[] myDebugStuList = new Student[6];
        public Student[] enemyDebugStuList = new Student[6];

        void Awake()
         {
             myDebugStuList[0] = new Student(stu_no.MATH1);
             myDebugStuList[1] = new Student(stu_no.MATH2);
             myDebugStuList[2] = new Student(stu_no.PHYS1);

             enemyDebugStuList[0] = new Student(stu_no.MATH1);
             enemyDebugStuList[1] = new Student(stu_no.MATH2);

            /*
             if (GameManager.getBattleOpponentNum() != null)
             {
                 for (int i = 0; i < 6; i++)
                 {
                     myDebugStuList[i] = GameManager.getBattlePlayerStudent(i);
                     enemyDebugStuList[i] = GameManager.getBattleTrainerStudent(i);
                 }
             }
            */

             for (int z = 0; z < 3; z++)
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
