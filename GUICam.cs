using UnityEngine;
using System.Collections;

[ExecuteInEditMode]


public class GUICam : MonoBehaviour {
    private Camera cam = null;

    public float pixel_scale = 200.0f;
    private Transform trans = null;

	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
        trans = transform;

      
    }
	
	// Update is called once per frame
	void Update () {
        cam.orthographicSize = Screen.height / 2 / pixel_scale;

        trans.localPosition = new Vector3(Screen.width / 2 / pixel_scale, -(Screen.height / 2 / pixel_scale), trans.localPosition.z);
	
	}
}
