﻿using UnityEngine;
using System.Collections;

public class StudentInfo: MonoBehaviour 
{
    //use for debug mode;
         public Student[] myDebugStuList = new Student[3];
         public Student[] enemyDebugStuList = new Student[2];

         void Start()
         {
            myDebugStuList[0] = new Student(new int[4]{1,2,3,4},"Math",new int[6]{20,20,20,20,20,20},10, 200, (Texture)Resources.Load("CAKE845_tmb.jpg"));
            myDebugStuList[1] = new Student(new int[4]{0,2,3,4},"Math",new int[6]{20,20,20,20,20,20},10, 200, (Texture)Resources.Load("_6XY_385.gif"));
            myDebugStuList[2] = new Student(new int[4]{0,1,2,3},"Math",new int[6]{20,20,20,20,20,20},10, 200, (Texture)Resources.Load("213danStu.png"));   
            
             
            enemyDebugStuList[0] = new Student(new int[4]{1,2,3,4},"Math",new int[6]{20,20,20,20,20,20},10, 200, (Texture)Resources.Load("385.jrachi.png"));
            enemyDebugStuList[1] = new Student(new int[4]{0,2,3,4},"Math",new int[6]{20,20,20,20,20,20},10, 200, (Texture)Resources.Load("flygon.gif"));
          
         }

         public Student retStuData(int i, int j)
         {
             if (i == 0)
                 return myDebugStuList[j];
             else
                 return enemyDebugStuList[j];
         }



}
