using UnityEngine;
using System.Collections;

public class StudentTextureList : MonoBehaviour {


   public Texture[] list = new Texture[6];
    public Texture stu1;
    public Texture stu2;
    public Texture stu3;
    public Texture stu4;
    public Texture stu5;
    public Texture stu6;

    public StudentTextureList()
    {
     
    }
    void Start()
    {
        list[0] = stu1;
        list[1] = stu2;
        list[2] = stu3;
        list[3] = stu4;
        list[4] = stu5;
        list[5] = stu6;
        for (int i = 0; i < 6; i++)
        {
            if (list[i] == null)
                Debug.Log("no data at list[" + i + "]");
        }
        
    }

    void Update()
    {
        list[0] = stu1;
        list[1] = stu2;
        list[2] = stu3;
        list[3] = stu4;
        list[4] = stu5;
        list[5] = stu6;
        for (int i = 0; i < 6; i++)
        {
            if (list[i] == null)
                Debug.Log("no data at list[" + i + "]");
        }
    }

    public Texture retTexture(int i)    {
        int j = i % 10 + i / 5; 
        return list[j];   
    }
}

