using UnityEngine;
using System.Collections;

public class ChangeCharacter : MonoBehaviour {
    
    public Camera cam;
    public Texture Bunchi;
    public Texture Assin;
    Texture myTexture;
    Texture opponentTexture;
    GameObject mineStu;
    Student myStudent;
    Student enemyStudent;
    StudentTextureList textureList;
    int i = 0;
    Texture[] frame = new Texture[2];
    public Texture fire_0;
    public Texture fire_1;

    Rect OpponentStudentImage;
    Rect OurStudentImage;
    // Use this for initialization
	void Start () {
        textureList = GameObject.Find("Main Camera").GetComponent<StudentTextureList>();
        mineStu = GameObject.Find("battleBackground");
        myStudent = GameObject.Find("battleBackground").GetComponent<BattleButtonManage>().CurrentMine;
        enemyStudent = GameObject.Find("battleBackground").GetComponent<BattleButtonManage>().CurrentEnemy;
        
        if (myStudent == null)
            Debug.Log("myStu is null");

        if (myStudent.retSkillList() == null)
            Debug.Log("mySkillList is null");
     
	}
    
    void Update () {
        myStudent = GameObject.Find("battleBackground").GetComponent<BattleButtonManage>().CurrentMine;
        enemyStudent = GameObject.Find("battleBackground").GetComponent<BattleButtonManage>().CurrentEnemy;
       
        myTexture = textureList.retTexture(myStudent.retStuIndex());
        opponentTexture = textureList.retTexture(enemyStudent.retStuIndex());

        frame[0] = fire_0;
        frame[1] = fire_1;
        i = (int)(Time.time * framesecond) % frame.Length;
  /*
  //the code will save index of posmon. set texture to current index student.
         
  */
    }

    void moveGameObject(GameObject alpha, Vector2 vector)
    {
        alpha.transform.position += (Vector3)vector;
    }

    void ChangeGameImage(GameObject alpha, Texture ImageName)
    {
        Graphics.DrawTexture(OpponentStudentImage, opponentTexture);
    }
    
    private double framesecond = 10;
    
    void OnGUI()
    {
      OpponentStudentImage = new Rect(cam.pixelWidth * 3 / 4, cam.pixelHeight * 1 / 8, cam.pixelWidth / 8, cam.pixelWidth / 8);
      OurStudentImage = new Rect(cam.pixelWidth * 1 / 8, cam.pixelHeight * 2 / 4, cam.pixelWidth / 8, cam.pixelWidth / 8);

      /*frame[0] = fire_0;
      frame[1] = fire_1;
      for (i = 0; i < frame.Length; )
      {
          i = (int)(Time.time * framesecond) % frame.Length;
          Graphics.DrawTexture(new Rect(cam.pixelWidth * 3 / 4, cam.pixelHeight * 1 / 8, cam.pixelWidth / 8, cam.pixelWidth / 8), frame[i]);
      }*/
        if (myTexture != null && opponentTexture != null)
      {
          Graphics.DrawTexture(OpponentStudentImage, opponentTexture);
          Graphics.DrawTexture(OurStudentImage, myTexture);
          
              
          
      }


        Graphics.DrawTexture(OpponentStudentImage, frame[i]);
    }
}
