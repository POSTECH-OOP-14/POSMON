using UnityEngine;
using System.Collections;

public class walk_wait : MonoBehaviour {
    protected Animator anim;
    private float x = 0;
    private float y = -0.1f;
    private bool w = false;

    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<CharacterStatus>().getBlocked() != true)
        {
            w = true;
            if (anim && Input.GetKey(KeyCode.LeftArrow))
            {
                //gameObject.transform.position -= new Vector3(0.1f, 0f, 0f);
                //w = true;
                x = -0.1f;
                y = 0;


            }
            else if (anim && Input.GetKey(KeyCode.RightArrow))
            {
                //gameObject.transform.position += new Vector3(0.1f, 0f, 0f);
                //w = true;
                x = 0.1f;
                y = 0;


            }
            else if (anim && Input.GetKey(KeyCode.UpArrow))
            {
                //gameObject.transform.position += new Vector3(0f, 0.1f, 0f);
                //w = true;
                x = 0;
                y = 0.1f;

            }
            else if (anim && Input.GetKey(KeyCode.DownArrow))
            {
                //gameObject.transform.position -= new Vector3(0f, 0.1f, 0f);
                //w = true;
                x = 0;
                y = -0.1f;


            }
            else
            {
                w = false;
                if (x > 0 || y > 0)
                {
                    x += 0.05f;
                    y += 0.05f;
                }
                else if (x < 0 || y < 0)
                {
                    x -= 0.05f;
                    y -= 0.05f;
                }
            }
            if (w)
            {
                //transform.Translate(new Vector3(10*x, 10*y, 0f)*Time.deltaTime*0.5f);
            }
            anim.SetFloat("x", x);
            anim.SetFloat("y", y);
            anim.SetBool("walk", w);

        }
    }


}
