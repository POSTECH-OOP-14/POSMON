﻿using UnityEngine;
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
        if (cam != null && cam.activeSelf)
            gameObject.transform.position = new Vector3(cam.GetComponent<Transform>().position.x,
                                                        cam.GetComponent<Transform>().position.y,
                                                        -0.5f);
    }
}
