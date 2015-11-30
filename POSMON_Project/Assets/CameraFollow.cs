using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GameObject cam = GameManager.pl_stored;
        if (GameManager.pl_stored != null)
        {
            if (GameManager.pl_stored.activeSelf)
            {
                gameObject.transform.position = new Vector3(cam.GetComponent<Transform>().position.x,
                                                            cam.GetComponent<Transform>().position.y,
                                                            -0.5f);
            }
        }
    }
}
