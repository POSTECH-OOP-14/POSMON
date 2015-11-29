using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    /* For the purpose of Global Variables */
    static public GameObject pl_stored = null;
    static public int PrevSceneNumber = 0;
    static public bool[] QuestGiven = new bool[100];

    public GameObject player;

	// Use this for initialization
	void Start ()
    {
        if (!pl_stored)
        {
            pl_stored = Instantiate(player, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
            pl_stored.SetActive(false);
            DontDestroyOnLoad(pl_stored);
            for (int i = 0; i < 100; i++)
                QuestGiven[i] = false;
        }
	}

	// Update is called once per frame
	void Update ()
    {
	}

    /* Start Game, for mainScene's GameLoader only */
    public void StartGame()
    {
        pl_stored.SetActive(true);
        PrevSceneNumber = Application.loadedLevel;
        Application.LoadLevel(1);
    }

    /* Exit Game, for mainScene's GameLoader only */
    public void ExitGame()
    {
        Application.Quit();
    }

    public static void ChangeMap(int scene_number)
    {
        if (scene_number > 0)
        {
            PrevSceneNumber = Application.loadedLevel;
            Application.LoadLevel(scene_number);
        }
    }

    public static void WarpCharacter(float x, float y)
    {
        pl_stored.transform.position = new Vector3(x, y, 0f);
    }
}
