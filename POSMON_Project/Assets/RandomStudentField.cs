using UnityEngine;
using System.Collections;

public class RandomStudentField : MonoBehaviour {

    public AudioClip WarningSound;
    public int probability_million = 15;
    public int leveling_limit = 5;
    public float leveling_probability = 0.2f;
    public float level_multiplier = 0.5f;
    private bool inProgress = false;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && inProgress == false)
        {
            Debug.Log("field collision occured");
            float level_sum = 0;

            /* Randomly Emgerge Student */
            if (Random.Range(1, 1000000) < probability_million)
            {
                bool playerValidity = false;
                int type;
                Student wild_student;

                /* setting player battle info */
                GameManager.resetBattleStudents();
                for (int i = 0; i < 6; i++)
                    GameManager.setBattlePlayerStudent(i, GameManager.pl_stored.GetComponent<CharacterStatus>().getStudent(i));

                /* check validity */
                for (int i = 0; i < 6; i++)
                {
                    Student player_stu = GameManager.pl_stored.GetComponent<CharacterStatus>().getStudent(i);
                    if (player_stu != null && player_stu.getHP() >= 1)
                        playerValidity = true;
                }

                /* setting wild student and application level */
                if (playerValidity)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        Student stu = GameManager.pl_stored.GetComponent<CharacterStatus>().getStudent(i);
                        if (stu != null)
                            level_sum += stu.getLevel();
                    }

                    do
                    {
                        type = Random.Range(1, 101);
                    } while (type % 10 != 0 && type % 10 != 1);

                    /* setting wild student */
                    wild_student = new Student((stu_no)type);
                    float target_level = (level_sum / GameManager.pl_stored.GetComponent<CharacterStatus>().getStudentCount());
                    for (int i = 0; i < leveling_limit; i++)
                    {
                        if (Random.value < leveling_probability)
                            target_level += 1;
                    }
                    target_level *= level_multiplier;

                    for (int i = 0; i < target_level; i++)
                        wild_student.levelUP();

                    GameManager.setBattleTrainerStudent(0, wild_student);
                    GameManager.setBattleHostNum(0);

                    StartCoroutine(LoadLevel());
                }
            }
        }
    }

    IEnumerator LoadLevel()
    {
        GameManager.pl_stored.GetComponent<CharacterStatus>().setBlocked(true);
        inProgress = true;
        GetComponent<AudioSource>().PlayOneShot(WarningSound);
        yield return new WaitForSeconds(2);
        /* load level */
        GameManager.pl_stored.SetActive(false);
        GameManager.PrevSceneNumber = Application.loadedLevel;
        Application.LoadLevel(9);
        GameManager.pl_stored.GetComponent<CharacterStatus>().setBlocked(false);
        inProgress = false;
    }
}
