using UnityEngine;
using System.Collections;

public class showLifeScr : MonoBehaviour {
    GameObject mineStu;
    GUIText guiTt;
    public Texture lifebar;
    public Texture background;
    int tempCurHp;
    int tempLvl;
    int tempMaxHp;
    Student a;
    // Use this for initialization
	
    
    void Start () {
        mineStu = GameObject.Find("battleBackground");
        a = GameObject.Find("battleBackground").GetComponent<BattleButtonManage>().CurrentMine;
        guiTt = gameObject.GetComponent<GUIText>();
	}
		// Update is called once per frame
	void Update () {
        tempCurHp = mineStu.GetComponent<BattleButtonManage>().CurrentMine.retHp();
        tempLvl = mineStu.GetComponent<BattleButtonManage>().CurrentMine.retLevel();
        tempMaxHp = mineStu.GetComponent<BattleButtonManage>().CurrentMine.retMaxHp();
        guiTt.text = "Lv "+tempLvl.ToString()+" "+tempCurHp+"/"+tempMaxHp;
    }

    void OnGUI() {

       // Rect block = new Rect(gameObject.transform.position.x,gameObject.transform.position.y, 100,50);

    }
}
