using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
//This makes user can choose menu mode or play mode


public class GUIObject : MonoBehaviour {
    [System.Serializable]//class pixel_position is serializable
    public class Pixel_Position//This determines position of GUI Object
    {
        public float left_pos;
        public float right_pos;
        public float top_pos;
        public float bottom_pos;
    }// This class specifies the amount of padding to be added onto an anchor to offset the GUI onto the screen into its desired position.


    public Pixel_Position pos;

    public enum HALIGN { left = 0, right = 1 };
    public enum VALIGN { top = 0, bottom = 1 };

    public HALIGN horizontal_align = HALIGN.left;
    public VALIGN vertical_align = VALIGN.top;// These two member variables are added to indicate the anchoring for the GUI control on both the horizontal and vertical axes.

    private GUICam cam = null;
    private Transform trans = null;

    public GUICam Cam
    {
        get
        {
            return cam;
        }

        set
        {
            cam = value;
        }
    }

    // Use this for initialization
    void Start () {
        trans = transform;
	
	}
	
	// Update is called once per frame
	void Update () {// this function calculates the position of the control, based on its anchoring and padding values , as well as the size of GUI camera.
        Vector3 final_pos = new Vector3(horizontal_align == HALIGN.left ? 0.0f : Screen.width, vertical_align == VALIGN.top ? 0.0f : -Screen.height, trans.localPosition.z);

        

        final_pos = new Vector3(final_pos.x + (pos.left_pos * Screen.width) - (pos.right_pos * Screen.width), final_pos.y - (pos.top_pos * Screen.height) + (pos.bottom_pos * Screen.height), final_pos.z);

        final_pos = new Vector3(final_pos.x / Cam.pixel_scale, final_pos.y / Cam.pixel_scale, final_pos.z);

        trans.localPosition = final_pos;
	}
   
   
  


}
