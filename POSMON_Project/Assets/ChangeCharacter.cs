using UnityEngine;
using System.Collections;

public class ChangeCharacter : MonoBehaviour {
    
    public Camera cam;
    public Texture Bunchi;
    public Texture Assin;
    public Texture Statement;
    Texture myTexture;
    Texture opponentTexture;

    int i=0;

    Rect OpponentStudentImage;
    Rect OurStudentImage;
    // Use this for initialization
	void Start () {
        myTexture = Bunchi;
        opponentTexture = Assin;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.UpArrow))
        {   
            if (i == 0){
                i = 1;
                opponentTexture = Bunchi;
                myTexture = Assin;
            }
            else{
                i=0;
                opponentTexture = Assin;
                myTexture = Bunchi;
            }
        }
    }

    void moveGameObject(GameObject alpha, Vector2 vector)
    {
        alpha.transform.position += (Vector3)vector;
    }

    void ChangeGameImage(GameObject alpha, Texture ImageName)
    {
        Graphics.DrawTexture(OpponentStudentImage, opponentTexture);
    }

    void OnGUI()
    {
      OpponentStudentImage = new Rect(cam.pixelWidth *3/4, cam.pixelHeight*1/4, cam.pixelWidth/8, cam.pixelHeight/8);
      OurStudentImage = new Rect(cam.pixelWidth * 1/4, cam.pixelHeight * 3/4, cam.pixelWidth/8, cam.pixelHeight/8);
      Rect OpponentStudentState = new Rect(cam.pixelWidth * 1 / 4, cam.pixelHeight * 1 / 4,400, 200);
      Rect OurStudentState = new Rect(cam.pixelWidth * 2 / 4, cam.pixelHeight * 3 / 4, 400,200);


      Graphics.DrawTexture(OpponentStudentImage, opponentTexture);
      Graphics.DrawTexture(OpponentStudentState, Statement);
      Graphics.DrawTexture(OurStudentImage, myTexture);
      Graphics.DrawTexture(OurStudentState, Statement);
      
    }
}
