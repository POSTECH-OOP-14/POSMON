using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GUIbuttonsho : MonoBehaviour
{
    public Camera cam;

    private List<Item> myList = new List<Item>();
    private GUIText guiConsole;
    private int index = 0;
    private bool isClicked=false;
    private int selected = 0;
    private string text = "아이템 설명";
    private string text2;

    public void makeList()
    {
        Item it = new Item(item_no.SKILL1);
        myList.Add(it);
        index++;
        it = new Item(item_no.SKILL3);
        myList.Add(it);
        index++;
        it = new Item(item_no.SKILL4);
        myList.Add(it);
        index++;
        it = new Item(item_no.CURE1);
        myList.Add(it);
        index++;
        it = new Item(item_no.CURE2);
        myList.Add(it);
        index++;
    }

    // Use this for initialization
    void Start()
    {
        makeList();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        Rect info = new Rect(45 + cam.pixelWidth / 4, 10, cam.pixelWidth / 3, cam.pixelHeight - 20);
        Rect purchase = new Rect(60+cam.pixelWidth/4+cam.pixelWidth/3, cam.pixelHeight/2, cam.pixelWidth/3, cam.pixelHeight/2-10);

        if (isClicked)
        {
            GUI.Box(info, text);
            GUI.Box(purchase, text2);
            if(GUI.Button(new Rect(90+cam.pixelWidth/12*7, cam.pixelHeight/4*3, cam.pixelWidth/8, cam.pixelHeight/8), "YES")){
                //player inventory와 소지하고 있는 돈 업데이트
            }
            else if (GUI.Button(new Rect(115 + cam.pixelWidth / 24 * 17, cam.pixelHeight / 4 * 3, cam.pixelWidth / 8, cam.pixelHeight / 8), "NO"))
            {
                isClicked = !isClicked;
            }
        }

        for (int i = 0; i < index; i++)
        {
            if (GUI.Button(new Rect(15, 10 + cam.pixelWidth / 30 * i, cam.pixelWidth / 8, cam.pixelHeight / 20), myList[i].getName()))
            {
                text = "아이템 설명";
                text2 = "\n";
                isClicked = !isClicked;
                selected = i;
                text = text + "\n\n 아이템 이름 : " + myList[selected].getName();
                text = text + "\n\n 아이템 종류 : ";
                if (myList[selected].getType() == type.skill)
                    text = text + "기술 머신";
                else if (myList[selected].getType() == type.cure)
                    text = text + "치료 아이템";
                else if (myList[selected].getType() == type.capture)
                    text = text + "포획 아이템";
                text = text + "\n\n 설명 : " + myList[selected].getInfo();
                text = text + "\n\n 가격: " + myList[selected].getPrice();
                text2 = text2 + "아이템  \"" + myList[selected].getName() + "\" 을/를 선택하셨습니다. ";
                text2 = text2 + "\n\n 구매하시겠습니까? "; 
            }

        }
    }
}
