using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GUIbuttonsbat : MonoBehaviour
{
    public Camera cam;
    bool menuToggle = false;
    int toggleDelay = 0;

//  private Item[] inventory;
//  private Student[] student_list;
    public GUIText guiConsole;
    private int index;
    private int stu_index;
    private bool isClicked = false;
    private bool isSelected = false;
    private int selected;
    private string text = "인벤토리";
    private string text2 = "아이템을 사용할 포스몬을 선택하세요";
    private string text3;

    public Item getItem(int i){
        Item ItemInInventory = GameObject.Find("GameObject (3)").GetComponent<CharacterStatus>().getInventory(i);
        return ItemInInventory;
    }

    public Student getStudent(int j)
    {
        Student StudentInList = GameObject.Find("GameObject (3)").GetComponent<CharacterStatus>().getStudent(j);
        return StudentInList;
    }

    public void setMenuToggle()
    {
        menuToggle = !menuToggle;
    }

    // Use this for initialization
    void Start()
    {
        //guiConsole 초기화

        guiConsole.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        GUI.skin.button.fontSize = 16;
        GUI.skin.box.fontSize = 20;
        Rect itemlist = new Rect(15, 10, cam.pixelWidth / 3+15, cam.pixelHeight - 20);
        Rect info = new Rect(45 + cam.pixelWidth / 4, 10, cam.pixelWidth / 3, cam.pixelHeight - 20);
        Rect stulist = new Rect(60 + cam.pixelWidth / 4 + cam.pixelWidth / 3, 10, cam.pixelWidth / 3, cam.pixelHeight / 2 - 10);      
        
        if (menuToggle)
        {
            GUI.Box(itemlist, text);//플레이어의 인벤토리 창 띄워준다
            guiConsole.transform.position = new Vector2((float)0.9, (float)0.1);

            if (isSelected)
            {
                GUI.Box(stulist, text2);

                for (int j = 0; j < stu_index; j++)
                {//아이템을 사용할 포스몬을 고를 수 있는 창을 띄운다. 
                    if (GUI.Button(new Rect(75 + cam.pixelWidth / 4 + cam.pixelWidth / 3, 20, cam.pixelWidth / 3 - 30, 20), getStudent(j).getDept()))
                    {
                        if (getItem(selected).useItem(getStudent(j)))
                        {//아이템 사용에 성공했을 경우
                            guiConsole.text = "아이템 사용에 성공했습니다!";
                        }
                        else
                        {//아이템 사용에 실패했을 경우
                            guiConsole.text = "아이템 사용에 실패했습니다!";
                        }
                    }
                }
            }

            if (isClicked)
            {
                GUI.Box(info, text3);

                if (GUI.Button(new Rect(45, cam.pixelHeight / 4 * 3, cam.pixelWidth / 8, cam.pixelHeight / 8), "YES"))
                {
                    isSelected = !isSelected;
                }
                else if (GUI.Button(new Rect(55 + cam.pixelWidth / 8, cam.pixelHeight / 4 * 3, cam.pixelWidth / 8, cam.pixelHeight / 8), "NO"))
                {
                    isClicked = !isClicked;
                }
            }

            for (int i = 0; i < index; i++)
            {
                if (GUI.Button(new Rect(30, 10 + cam.pixelWidth / 30 * i, cam.pixelWidth / 8, cam.pixelHeight / 20),getItem(i).getName()))
                {
                    text3 = "아이템 설명";
                    isClicked = !isClicked;
                    selected = i;
                    text3 = text3 + "\n\n 아이템 이름 : " + getItem(selected).getName();
                    text3 = text3 + "\n\n 아이템 종류 : ";
                    if (getItem(selected).getType() == type.skill)
                        text3 = text3 + "기술 머신";
                    else if (getItem(selected).getType() == type.cure)
                        text3 = text3 + "치료 아이템";
                    else if (getItem(selected).getType() == type.capture)
                        text3 = text3 + "포획 아이템";
                    text3 = text3 + "\n\n 설명 : " + getItem(selected).getInfo();
                    text3 = text3 + "\n\n 가격: " + getItem(selected).getPrice();
                    text3 = text3 + "\n\n아이템  \"" + getItem(selected).getName() + "\" 을/를 선택하셨습니다. ";
                    text3 = text3 + "\n\n 사용하시겠습니까? ";
                }

            }

        }
    }
}
