using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GUIbuttonsho : MonoBehaviour
{
    public Camera cam;

    public AudioClip WarningSound;
    public AudioClip SuccessSound;

    private List<Item> myList = new List<Item>();
    private GUIText guiConsole;
    private int index = -1;
    private bool isClicked=false;
    private int selected = 0;
    private string text = "아이템 설명";
    private string text2;
    
    private bool ShopActivated = false;

    public void makeList()
    {
        myList.Add(new Item(item_no.SKILL1));
        index++;

        myList.Add(new Item(item_no.SKILL3));
        index++;
        myList.Add(new Item(item_no.SKILL4));
        index++;
        myList.Add(new Item(item_no.CURE1));
        index++;
        myList.Add(new Item(item_no.CURE2));
        index++;
        myList.Add(new Item(item_no.CURE6));
        index++;
        myList.Add(new Item(item_no.CURE11));
        index++;
        myList.Add(new Item(item_no.CURE8));
        index++;
        myList.Add(new Item(item_no.CAPTURE1));
        index++;
        myList.Add(new Item(item_no.CAPTURE4));
        index++;
        myList.Add(new Item(item_no.CAPTURE2));
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
        /* Turn off shop */
        if (ShopActivated)
            if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Escape))
                setShopActivation(false);
    }

    public void setShopActivation(bool act)
    {
        ShopActivated = act;
        GameManager.pl_stored.GetComponent<CharacterStatus>().setBlocked(act);
    }

    void OnGUI()
    {
        if (ShopActivated)
        {
            GUI.skin.button.fontSize = 13;
            /*
            GUIStyle style = new GUIStyle(GUI.skin.box);
            Texture2D texture = new Texture2D(cam.pixelWidth / 3, cam.pixelHeight - 20);
            for (int x = 0; x < cam.pixelWidth / 3; x++)
            {
                for (int y = 0; y < cam.pixelHeight - 20; y++)
                {
                    texture.SetPixel(x + 45 + cam.pixelWidth / 4, y + 10, Color.yellow);
                }
            }
            texture.Apply();
            style.normal.background = texture;
            */
            GUIStyle myStyle = new GUIStyle(GUI.skin.box);
            myStyle.fontSize = 13;
            myStyle.font = (Font)Resources.Load("NANUMBARUNGOTHICBOLD", typeof(Font));
            myStyle.alignment = TextAnchor.UpperLeft;
            myStyle.normal.textColor = Color.white;

            Rect info = new Rect(45 + cam.pixelWidth / 4, 10, cam.pixelWidth / 3, cam.pixelHeight - 20);
            Rect purchase = new Rect(60 + cam.pixelWidth / 4 + cam.pixelWidth / 3, cam.pixelHeight / 2, cam.pixelWidth / 3, cam.pixelHeight / 2 - 10);

            if (isClicked)
            {
                GUI.Box(info, text, myStyle);
                GUI.Box(purchase, text2, myStyle);
                if (GUI.Button(new Rect(90 + cam.pixelWidth / 12 * 7, cam.pixelHeight / 4 * 3, cam.pixelWidth / 8, cam.pixelHeight / 8), "YES"))
                {
                    // player inventory와 소지하고 있는 돈 업데이트
                    if (GameManager.pl_stored.GetComponent<CharacterStatus>().getMoney() >= myList[selected].getPrice())
                    {
                        if (GameManager.pl_stored.GetComponent<CharacterStatus>().addItemToInventory(new Item((item_no)myList[selected].getIndex())))
                        {
                            GetComponent<AudioSource>().PlayOneShot(SuccessSound);
                            GameManager.pl_stored.GetComponent<CharacterStatus>().addMoney(-1 * myList[selected].getPrice());
                        }
                        else
                        {
                            GetComponent<AudioSource>().PlayOneShot(WarningSound);
                        }
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(WarningSound);
                    }
                }
                else if (GUI.Button(new Rect(115 + cam.pixelWidth / 24 * 17, cam.pixelHeight / 4 * 3, cam.pixelWidth / 8, cam.pixelHeight / 8), "NO"))
                {
                    isClicked = !isClicked;
                }
            }

            for (int i = 0; i < index; i++)
            {
                if (GUI.Button(new Rect(15, 10 + cam.pixelWidth / 30 * i, cam.pixelWidth / 6, cam.pixelHeight / 20), myList[i].getName()))
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
}

