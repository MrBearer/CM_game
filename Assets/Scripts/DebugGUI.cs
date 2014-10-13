using UnityEngine;
using System.Collections;

public class DebugGUI : MonoBehaviour {

	GameObject cameraPivot;

	// Use this for initialization
	void Start () {
		cameraPivot = GameObject.FindGameObjectWithTag("CameraPivot");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.F1))
		{
			Moba_Camera mc = cameraPivot.GetComponent<Moba_Camera>();
			mc.enabled = !mc.enabled;
		}
	}


}
