using UnityEngine;
using System.Collections;

public class MouseOverUI : MonoBehaviour {

    GameObject pivot;
    Moba_Camera cam;

	// Use this for initialization
	void Start () {
        pivot = GameObject.FindGameObjectWithTag("CameraPivot");
        cam = (Moba_Camera)pivot.GetComponent<Moba_Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void OverUI(bool over)
    {
        cam.enabled = !over;
    }
}
