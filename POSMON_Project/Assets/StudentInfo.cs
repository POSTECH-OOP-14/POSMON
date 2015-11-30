﻿using UnityEngine;
using System.Collections;

public class StudentInfo: MonoBehaviour 
{
    //use for debug mode;
         public Student[] myDebugStuList = new Student[6];
         public Student[] enemyDebugStuList = new Student[6];
    StudentTextureList textList= new StudentTextureList();

        void Awake()
         {
             myDebugStuList[0] = new Student(stu_no.MATH1, new int[4] { 1, 2, 3, 4 }, "Math", new int[6] { 20, 20, 20, 20, 20, 20 }, 10, 100);
             myDebugStuList[1] = new Student(stu_no.MATH2, new int[4] { 0, 2, 3, 4 }, "Math", new int[6] { 20, 20, 20, 20, 20, 20 }, 10, 100);
             myDebugStuList[2] = new Student(stu_no.MATH3, new int[4] { 0, 1, 2, 3 }, "Math", new int[6] { 20, 20, 20, 20, 20, 20 }, 10, 100);

             enemyDebugStuList[0] = new Student(stu_no.BIO1, new int[4] { 1, 2, 3, 4 }, "Math", new int[6] { 20, 20, 20, 20, 20, 20 }, 10, 100);
             enemyDebugStuList[1] = new Student(stu_no.BIO1, new int[4] { 0, 2, 3, 4 }, "Math", new int[6] { 20, 20, 20, 20, 20, 20 }, 10, 100);
         }     

        void Start()
         {
             myDebugStuList[0] = new Student(stu_no.MATH1, new int[4] { 1, 2, 3, 4 }, "Math", new int[6] { 20, 20, 20, 20, 20, 20 }, 10, 100);
             myDebugStuList[1] = new Student(stu_no.MATH2, new int[4] { 0, 2, 3, 4 }, "Math", new int[6] { 20, 20, 20, 20, 20, 20 }, 10, 100);
             myDebugStuList[2] = new Student(stu_no.MATH3, new int[4] { 0, 1, 2, 3 }, "Math", new int[6] { 20, 20, 20, 20, 20, 20 }, 10, 100);

             enemyDebugStuList[0] = new Student(stu_no.BIO1, new int[4] { 1, 2, 3, 4 }, "Math", new int[6] { 20, 20, 20, 20, 20, 20 }, 10, 100);
             enemyDebugStuList[1] = new Student(stu_no.BIO2, new int[4] { 0, 2, 3, 4 }, "Math", new int[6] { 20, 20, 20, 20, 20, 20 }, 10, 100);
    
         }

         public Student retStuData(int i, int j)
         {
             if (i == 0)
                 return myDebugStuList[j];
             else
                 return enemyDebugStuList[j];
         }



}
