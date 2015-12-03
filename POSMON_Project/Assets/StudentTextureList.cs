using UnityEngine;
using System.Collections;

public class StudentTextureList : MonoBehaviour {


   Texture[] list = new Texture[44];
   public Texture stu_MATH1;
   public Texture stu_MATH1_1;
   public Texture stu_MATH2;
   public Texture stu_MATH2_1;
   public Texture stu_PHYS1;
   public Texture stu_PHYS1_1;
   public Texture stu_PHYS2;
   public Texture stu_PHYS2_1;
   public Texture stu_CSE1;
   public Texture stu_CSE1_1;
   public Texture stu_CSE2;
   public Texture stu_CSE2_1;
   public Texture stu_IME1;
   public Texture stu_IME1_1;
   public Texture stu_IME2;
   public Texture stu_IME2_1;
   public Texture stu_CITE1;
   public Texture stu_CITE1_1;
   public Texture stu_CITE2;
   public Texture stu_CITE2_1;
   public Texture stu_CHEM1;
   public Texture stu_CHEM1_1;
   public Texture stu_CHEM2;
   public Texture stu_CHEM2_1;
   public Texture stu_CME1;
   public Texture stu_CME1_1;
   public Texture stu_CME2;
   public Texture stu_CME2_1;
   public Texture stu_MSE1;
   public Texture stu_MSE1_1;
   public Texture stu_MSE2;
   public Texture stu_MSE2_1;
   public Texture stu_BIO1;
   public Texture stu_BIO1_1;
   public Texture stu_BIO2;
   public Texture stu_BIO2_1;
   public Texture stu_EE1;
   public Texture stu_EE1_1;
   public Texture stu_EE2;
   public Texture stu_EE2_1;
   public Texture stu_ME1;
   public Texture stu_ME1_1;
   public Texture stu_ME2;
   public Texture stu_ME2_1;


    public StudentTextureList()
    {
     
    }
    void Start()
    {
   list[0] = stu_MATH1;
   list[1] = stu_MATH1_1;
   list[2] = stu_MATH2;
   list[3] = stu_MATH2_1;
   list[4] = stu_PHYS1;
   list[5] = stu_PHYS1_1;
   list[6] = stu_PHYS2;
   list[7] = stu_PHYS2_1;
   list[8] = stu_CSE1;
   list[9] = stu_CSE1_1;
   list[10] = stu_CSE2;
   list[11] = stu_CSE2_1;
   list[12] = stu_IME1;
   list[13] = stu_IME1_1;
   list[14] = stu_IME2;
   list[15] = stu_IME2_1;
   list[16] = stu_CITE1;
   list[17] = stu_CITE1_1;
   list[18] = stu_CITE2;
   list[19] = stu_CITE2_1;
   list[20] = stu_CHEM1;
   list[21] = stu_CHEM1_1;
   list[22] = stu_CHEM2;
   list[23] = stu_CHEM2_1;
   list[24] = stu_CME1;
   list[25] = stu_CME1_1;
   list[26] = stu_CME2;
   list[27] = stu_CME2_1;
   list[28] =stu_IME1;
   list[29] =stu_MSE1_1;
   list[30] = stu_MSE2;
   list[31] = stu_MSE2_1;
   list[32] = stu_BIO1;
   list[33] = stu_BIO1_1;
   list[34] = stu_BIO2;
   list[35] = stu_BIO2_1;
   list[36] = stu_EE1;
   list[37] = stu_EE1_1;
   list[38] = stu_EE2;
   list[39] = stu_EE2_1;
   list[40] = stu_ME1;
   list[41] = stu_ME1_1;
   list[42] = stu_ME2;
   list[43] = stu_ME2_1; 

        for (int i = 0; i < 43; i++)
        {
            if (list[i] == null)
                Debug.Log("no data at list[" + i + "]");
        }
        
    }

    void Update()
    {   
        for (int i = 0; i < 43; i++)
        {
            if (list[i] == null)
                Debug.Log("no data at list[" + i + "]");
        }
    }

    public Texture retTexture(int i)    {
        int j = i % 10 + i / 5 * 2; 
        return list[j];   
    }
    public Texture retUpgradeTexture(int i)
    {
        int j = i % 10 + i / 5 * 2 + 2;
        return list[j];
    }
}

