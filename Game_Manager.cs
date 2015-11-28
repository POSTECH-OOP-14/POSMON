using UnityEngine;
using System.Collections;
[RequireComponent (typeof(NotificationManager))]
public class Game_Manager : MonoBehaviour {

    private static Game_Manager instance =null;

    public static Game_Manager Instance
    {
       
        
        get
        {
            if (instance == null) instance = new GameObject("Game_Manager").AddComponent<Game_Manager>();

            return instance;
        }
    }

    void awake()
    {
        if ((instance) && (instance.GetInstanceID() != GetInstanceID()))
            DestroyImmediate(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private static NotificationManager Note;
    public static NotificationManager note
    {
        get
        {
            if (Note == null) Note = instance.GetComponent<NotificationManager>();
            return Note;
        }
    }
    private bool binput_allow = true;

    public bool input_allow
    {
        get { return binput_allow; }

        set
        {
            binput_allow = value;

            note.post(this, "Input Changed");
        }
    }


        
	// Use this for initialization
	void Start () {
        note.add(this, "RestatGame");
        note.add(this, "ExitGame");
        
	
	}
	public void restart()
    {
        Application.LoadLevel(0);
    }

    public void exit()
    {
        Application.Quit();
    }
	// Update is called once per frame
	void Update () {
	
	}
}
