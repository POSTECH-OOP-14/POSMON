using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    /* For the purpose of Global Variables */
    static public GameObject pl_stored = null;
    static public int PrevSceneNumber = 8;
    static public bool[] QuestGiven = new bool[100];

    /* [BATTLE] to store battle result, use GameManager variables */
    public enum BattleResult
    {
        NOTHING,
        WIN,
        LOSE
    };

    static private int BattleHostNumber = 0;
    static private Student[] BattlePlayerStudents = new Student[6];
    static private Student[] BattleTrainerStudents = new Student[6];

    public static void resetBattleStudents()
    {
        for (int i = 0; i < 6; i++)
        {
            BattlePlayerStudents[i] = null;
            BattleTrainerStudents[i] = null;
        }
    }

    public static Student getBattlePlayerStudent(int i)
    {
        return BattlePlayerStudents[i];
    }

    public static Student getBattleTrainerStudent(int i)
    {
        return BattleTrainerStudents[i];
    }

    public static void setBattlePlayerStudent(int i, Student stu)
    {
        BattlePlayerStudents[i] = stu;
    }

    public static void setBattleTrainerStudent(int i, Student stu)
    {
        BattleTrainerStudents[i] = stu;
    }

    public static int getBattleHostNum()
    {
        return BattleHostNumber;
    }

    public static void setBattleHostNum(int npc_number)
    {
        BattleHostNumber = npc_number;
    }

    /* ----------------------------------------------------------- */


    public GameObject player;

	// Use this for initialization
	void Start ()
    {
        /* Only one time it would be done */
        if (!pl_stored)
        {
            pl_stored = Instantiate(player, new Vector3(-3.22f, -5.88f, 0f), Quaternion.identity) as GameObject;
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
        /* frame limit. if computer's frame soar abut 120fps, collision broke */
#if UNITY_EDITOR
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 45;
#endif
        pl_stored.SetActive(true);
        Application.LoadLevel(GameManager.PrevSceneNumber);
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
