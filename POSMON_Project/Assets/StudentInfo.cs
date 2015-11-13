using UnityEngine;
using System.Collections;

public class StudentInfo : MonoBehaviour {

    int StudentHealth;
    int StudentLevel;
    int StudentAttack;
    int StudentSpecialAttack;
    int StudentDefense;
    int StudentSpecialDefense;
    int StudentSpeed;
    public int type;


	// Use this for initialization
	void Start () {
         StudentHealth = 100;
         StudentLevel = 10;
         StudentAttack = 50;
         StudentSpecialAttack = 50;
         StudentDefense = 50;
         StudentSpecialDefense = 50;
         StudentSpeed = 50;
         StudentAttack = 50;
         type = 3;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

public int retStudentHealth() { return StudentHealth; }
public int retStudentLevel() { return StudentLevel; }
public int retStudentAttack() { return StudentAttack; }
public int retStudentSpecialAttack() { return StudentSpecialAttack; }
public int retStudentDefense() { return StudentDefense; }
public int retStudentSpecialDefense() { return StudentSpecialDefense; }
public int retStudentSpeed() { return StudentSpeed; }

}
