using UnityEngine;
using System.Collections;

public class LivingZone : MonoBehaviour {

	int ID;
	public int energyNeeded;
	public GameObject e_icon;
	public Texture2D e_icon2d;
	bool added;
	Placing pl;
	bool hasEnergy;
	Camera cam;

	// Use this for initialization
	void Start () {
		added = false;
		pl = gameObject.GetComponent<Placing>();
		ID = ID_generator.generate_ID();
		hasEnergy = false;
		cam = GameObject.FindGameObjectWithTag("MainCamera").camera;
	}
	
	// Update is called once per frame
	void Update () {
		if (!pl.isGhost)
		{
			if (!added)
			{
				Electricity.AddConsumer(energyNeeded,ID);
				added = true;
			}
			if (Electricity.GetStatus(ID))
			{
				hasEnergy = true;
			//	MeshRenderer mr = e_icon.GetComponent<MeshRenderer>();
			//	mr.enabled = false;
			}
			else
			{
				hasEnergy = false;
				Vector3 screenPosition = cam.WorldToScreenPoint(transform.position);
				screenPosition.y = Screen.height - (screenPosition.y + 1);
				Rect rect = new Rect(screenPosition.x - 50,
				                     screenPosition.y - 12, 30, 30);
				GUI.Label(rect, e_icon2d);
				//MeshRenderer mr = e_icon.GetComponent<MeshRenderer>();
				//mr.enabled = true;
			}
		}
	}

}
