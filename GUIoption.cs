using UnityEngine;
using System.Collections;

public class GUIoption : MonoBehaviour {
    private SpriteRenderer sr = null;//sprite로 메인 메뉴를 그린다

    private BoxCollider[] colliders = null;//메뉴버튼을 클릭 가능하도록 만듬. 버튼을 collider로 어레이 형태로 만들 것이다.

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();

        colliders = GetComponentsInChildren<BoxCollider>();

        Game_Manager.note.add(this, "Show Options");
        Game_Manager.note.add(this, "Hide Options");

        hide_option(null);
	
	}

    public void hide_option(Component send)
    {
        set_visible(false);
    }

    public void show_option(Component send)
    {
        set_visible();
    }
	
    private void set_visible(bool bshow = true)// 메인 메뉴를 보이게 할 것인지 안보이게 할 것인지 정하도록 만들어 준다.
    {
        Time.timeScale = (bshow) ? 0.0f : 1.0f;//게임 정지 기능을 추가. 1은 보통 속도를 의미하며, 0은 아예 멈추는 것을 말한다.

        Game_Manager.Instance.input_allow = !bshow;

        sr.enabled = bshow;

        foreach (BoxCollider b in colliders)
            b.enabled = bshow;
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            set_visible(!sr.enabled);
	
	}
}
