using UnityEngine;
using System.Collections;

public class ColliderMessenger : MonoBehaviour {
	
	public GameObject buildingCameraPivot;
	// Use this for initialization
	void Start () {
		buildingCameraPivot = GameObject.FindGameObjectWithTag("CameraPivot");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		buildingCameraPivot.gameObject.SendMessage("Colliding", true);
		Debug.Log(other.name);
	}
	
	void OnTriggerExit(Collider other) {
		buildingCameraPivot.gameObject.SendMessage("Colliding", false);
	}

}
