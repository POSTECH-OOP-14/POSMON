using UnityEngine;
using System.Collections;

public class ShowLifeState : MonoBehaviour
{
    GameObject _mineStu;
    GUIText _guiTt;
    public Texture _lifebar;
    public Texture _background;
    double _tempCurHp;
    int _tempLvl;
    double _tempMaxHp;
    BattleButtonManage _a;
    Rect _block;
    Rect __lifeBar;
    // Use this for initialization


    void Start()
    {
        _a = GameObject.Find("battleBackground").GetComponent<BattleButtonManage>();
        _guiTt = gameObject.GetComponent<GUIText>();
        _block = new Rect(gameObject.transform.position.x * Screen.width, (1 - gameObject.transform.position.y) * Screen.height + 24, Screen.width / 8, Screen.height / 16);
        __lifeBar = new Rect(gameObject.transform.position.x * Screen.width, (1 - gameObject.transform.position.y) * Screen.height, Screen.width / 8, Screen.height / 18);
    }
    // Update is called once per frame
    void Update()
    {
        _tempCurHp = _a.CurrentEnemy.getHP();
        _tempLvl = _a.CurrentEnemy.getLevel();
        _tempMaxHp = _a.CurrentEnemy.getMAXHP();
        _guiTt.text = _a.CurrentEnemy.retStudentName() + "Lv " + _tempLvl.ToString() + " " + _tempCurHp + "/" + _tempMaxHp;
        __lifeBar = new Rect(gameObject.transform.position.x * Screen.width ,
                (1 - gameObject.transform.position.y) * Screen.height + 25, Screen.width / 8 * (float)_tempCurHp / (float)_tempMaxHp, Screen.height / 18);
    }

    void OnGUI(){
         if (!(_a.Battle == BattleButtonManage.BattleButtonState.OneOutState || _a.Battle == BattleButtonManage.BattleButtonState.ExchangeState))        {
            Graphics.DrawTexture(_block, _background);
            Graphics.DrawTexture(__lifeBar, _lifebar);
        }
    }
}
