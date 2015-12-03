using UnityEngine;
using System.Collections;

public class StoryEffector : MonoBehaviour {

    public AudioClip SnoringSound;
    public AudioClip ShockSound;
    private static bool shaken = false;
    private float shaking_time = 0;
    private GameObject maincam = null;
    private GameObject warp;

	// Use this for initialization
	void Start () {
        warp = GameObject.Find("Warp");
        warp.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (shaking_time > 0 && maincam != null)
        {
            maincam.transform.localPosition = GameManager.pl_stored.transform.localPosition +  Random.insideUnitSphere * 0.7f;
            shaking_time -= Time.deltaTime;
        }
	
	}

    public void ShakeScreen(float second)
    {
        maincam = GameObject.Find("Main Camera");
        if (maincam != null)
        {
            shaking_time = second;
            shaken = true;
        }
    }

    void OnLevelWasLoaded()
    {
        if (shaken != true)
        {
            GameManager.pl_stored.GetComponent<CharacterStatus>().setBlocked(true);
            maincam = GameObject.Find("Main Camera");
            maincam.GetComponent<Fading>().fadeSpeed = 0.02f;
            maincam.GetComponent<Fading>().BeginFade(-1);
            StartCoroutine(StartDialogue());
            StartCoroutine(PickStudent());
        }
    }

    IEnumerator StartDialogue()
    {
        int dialogue_count = 2;

        GetComponent<AudioSource>().PlayOneShot(SnoringSound);

        yield return new WaitForSeconds(2.5f);
        while (true)
        {
            if (GameObject.Find("학생처장").GetComponent<Dialogue>().getDialogueOnOff() == false)
            {
                GameObject.Find("학생처장").GetComponent<NPCStatus>().interaction(GameManager.pl_stored);

                if (dialogue_count == 1)
                {
                    GetComponent<AudioSource>().Stop();
                    ShakeScreen(0.8f);
                }

                dialogue_count--;
                /* if no dialogue left, break */
                if (dialogue_count == 0)
                    break;
            }
            yield return new WaitForSeconds(0.4f);
        }
    }

    /* wait until talking with Student 3 */
    IEnumerator PickStudent()
    {
        yield return new WaitForSeconds(2.5f);
        while (true)
        {
            /* When talking with student3, make special effect */
            if (GameObject.Find("학생3").GetComponent<Dialogue>().getDialogueOnOff() == true)
            {
                int i;
                GetComponent<AudioSource>().PlayOneShot(ShockSound);
                maincam.GetComponent<Camera>().orthographicSize = 3;

                do {
                i = Random.Range(1, 101);
                } while (i % 10 != 0 && i % 10 != 1);

                Student stu = new Student((stu_no)i);
                GameManager.pl_stored.GetComponent<CharacterStatus>().setStudent(stu);
                stu.levelUP();
                stu.levelUP();
                warp.SetActive(true);
                break;
            }
            yield return new WaitForSeconds(3.5f);
        }

        /* Call Dialogue of Additional picking conversation */
        while (true)
        {
            if (GameObject.Find("학생3").GetComponent<Dialogue>().getDialogueOnOff() == false)
            {
                GameObject.Find("StoryDirector").GetComponent<NPCStatus>().interaction(GameManager.pl_stored);
                break;
            }
            yield return new WaitForSeconds(0.5f);
        }

        /* Zoom Out when dialogue fininshed */
        while (true)
        {
            if (GameObject.Find("StoryDirector").GetComponent<Dialogue>().getDialogueOnOff() == false)
            {
                maincam.GetComponent<Camera>().orthographicSize = 7;
                break;
            }
            yield return new WaitForSeconds(0.5f);
        }

        float fade = maincam.GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fade);
        GameManager.ChangeMap(1);
    }
}
