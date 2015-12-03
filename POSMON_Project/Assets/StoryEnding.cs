using UnityEngine;
using System.Collections;

public class StoryEnding : MonoBehaviour {
    public AudioClip EmergeSound;
    public AudioClip ClearSound;
    static private bool emerged = false;
    static private bool clear = false;
    private float shaking_time = 0;
    private GameObject maincam;

	// Use this for initialization
	void Start () {
        maincam = GameObject.Find("Main Camera");
        if (emerged == true)
        {
            GameObject.Find("넙교수").transform.position = new Vector3(-22f, 4f, 0.1f);
            GameObject.Find("아인슈타인").transform.position = new Vector3(-0.5f, 4f, 0.1f);
            GameObject.Find("퀴리 부인").transform.position = new Vector3(1f, 3f, 0.1f);
            GameObject.Find("라이너스 폴링").transform.position = new Vector3(-2f, 3f, 0.1f);
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (shaking_time > 0 && maincam != null)
        {
            maincam.transform.localPosition = GameManager.pl_stored.transform.localPosition + Random.insideUnitSphere * 0.7f;
            shaking_time -= Time.deltaTime;
        }

        if (emerged == false)
        {
            if (GameManager.pl_stored.GetComponent<CharacterStatus>().getQuest(2) == null && GameManager.QuestGiven[2] == true)
            {
                emerged = true;
                StartCoroutine(ThreeIllusion());
            }
        }
        /* game clear */
        else if (GameManager.pl_stored.GetComponent<CharacterStatus>().getQuest(4) == null && GameManager.QuestGiven[4] == true &&
                 GameManager.pl_stored.GetComponent<CharacterStatus>().getQuest(5) == null && GameManager.QuestGiven[5] == true &&
                 GameManager.pl_stored.GetComponent<CharacterStatus>().getQuest(6) == null && GameManager.QuestGiven[6] == true && clear == false)
        {
            clear = true;
            StartCoroutine(ClearGame());
        }
	}

    public void ShakeScreen(float second)
    {
        maincam = GameObject.Find("Main Camera");
        if (maincam != null)
        {
            shaking_time = second;
        }
    }

    IEnumerator ThreeIllusion()
    {
        while (true)
        {
            if (GameObject.Find("넙교수").GetComponent<Dialogue>().getDialogueOnOff() == false)
                break;
            yield return new WaitForSeconds(0.3f);
        }

        GetComponent<AudioSource>().PlayOneShot(EmergeSound);
        GameObject.Find("넙교수").transform.position = new Vector3(-22f, 4f, 0.1f);

        Camera cam = maincam.GetComponent<Camera>();
        cam.orthographicSize = 3;
        GameManager.pl_stored.GetComponent<CharacterStatus>().setBlocked(true);

        GameObject.Find("StoryDirector").GetComponent<NPCStatus>().interaction(GameManager.pl_stored);

        while (true)
        {
            if (GameObject.Find("StoryDirector").GetComponent<Dialogue>().getDialogueOnOff() == false)
                break;
            yield return new WaitForSeconds(0.3f);
        }

        ShakeScreen(3);
        yield return new WaitForSeconds(3);

        GameObject.Find("넙교수").transform.position = new Vector3(-22f, 4f, 0.1f);
        GameObject.Find("아인슈타인").transform.position = new Vector3(-0.5f, 4f, 0.1f);
        GameObject.Find("퀴리 부인").transform.position = new Vector3(1f, 3f, 0.1f);
        GameObject.Find("라이너스 폴링").transform.position = new Vector3(-2f, 3f, 0.1f);

        GameManager.pl_stored.GetComponent<CharacterStatus>().setBlocked(false);
        cam.orthographicSize = 7;

        GetComponent<AudioSource>().Stop();
    }

    IEnumerator ClearGame()
    {

        while (true)
        {
            if (GameObject.Find("아인슈타인").GetComponent<Dialogue>().getDialogueOnOff() == false &&
                GameObject.Find("퀴리 부인").GetComponent<Dialogue>().getDialogueOnOff() == false &&
                GameObject.Find("라이너스 폴링").GetComponent<Dialogue>().getDialogueOnOff() == false)
                break;
            yield return new WaitForSeconds(0.5f);
        }

        GameManager.pl_stored.GetComponent<CharacterStatus>().setBlocked(true);
        GetComponent<AudioSource>().PlayOneShot(ClearSound);
        GameObject director = GameObject.Find("StoryDirector");
        director.GetComponent<NPCStatus>().interaction(GameManager.pl_stored);

        while (true)
        {
            if (director.GetComponent<Dialogue>().getDialogueOnOff() == false)
                break;
            yield return new WaitForSeconds(0.3f);
        }

        float fade = gameObject.GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fade);

        yield return new WaitForSeconds(5f);
        GameManager.pl_stored.GetComponent<CharacterStatus>().setBlocked(false);
        GameManager.pl_stored.SetActive(false);
        Application.LoadLevel(0);
    }
}
