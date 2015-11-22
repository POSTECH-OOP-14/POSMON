using UnityEngine;
using System.Collections;

public class showLifeScr : MonoBehaviour {
    GameObject mineStu;
    GUIText guiTt;
    public Texture alpha;
    // Use this for initialization
	
    
    void Start () {
        mineStu = GameObject.Find("battleBackground");
        guiTt = gameObject.GetComponent<GUIText>();
	}
		// Update is called once per frame
	void Update () {
        int tempCurHp = mineStu.GetComponent<BattleButtonManage>().Mine.retStudentCurHealth();
        int tempLvl = mineStu.GetComponent<BattleButtonManage>().Mine.retStudentLevel();
        int tempMaxHp = mineStu.GetComponent<BattleButtonManage>().Mine.retStudentMaxHealth();
        guiTt.text = "Lv "+tempLvl.ToString()+" "+tempCurHp+"/"+tempMaxHp;
    }

    void OnGUI() { 
        


    }
}
