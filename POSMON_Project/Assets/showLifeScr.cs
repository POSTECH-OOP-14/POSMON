using UnityEngine;
using System.Collections;

public class showLifeScr : MonoBehaviour {
    GameObject mineStu;
    GUIText guiTt;
    public Texture lifebar;
    public Texture background;
    double tempCurHp;
    int tempLvl;
    double tempMaxHp;
    BattleButtonManage a;
    Rect block;
    Rect _lifeBar;
    // Use this for initialization
	
    
    void Start () {
        a = GameObject.Find("battleBackground").GetComponent<BattleButtonManage>();
        guiTt = gameObject.GetComponent<GUIText>();
        block = new Rect(gameObject.transform.position.x * Screen.width - Screen.width / 8 - 20, (1 - gameObject.transform.position.y) * Screen.height + 24, Screen.width / 8, Screen.height / 16);
        _lifeBar = new Rect(gameObject.transform.position.x * Screen.width, (1 - gameObject.transform.position.y) * Screen.height, Screen.width / 8, Screen.height / 18);
	}
		// Update is called once per frame
	void Update () {
        tempCurHp =a.CurrentMine.getHP();
        tempLvl = a.CurrentMine.getLevel();
        tempMaxHp =a.CurrentMine.getMAXHP();
        guiTt.text = a.CurrentMine.retStudentName()+"Lv "+tempLvl.ToString()+" "+tempCurHp+"/"+tempMaxHp;
        _lifeBar = new Rect(gameObject.transform.position.x * Screen.width - Screen.width / 8 - 20,
                (1 - gameObject.transform.position.y) * Screen.height + 25, Screen.width / 8 * (float)tempCurHp / (float)tempMaxHp, Screen.height / 18);
    }

    void OnGUI() {
        if (a.Battle != BattleButtonManage.BattleButtonState.OneOutState)
        {
            Graphics.DrawTexture(block, background);
            Graphics.DrawTexture(_lifeBar, lifebar);
        }
    }
}
