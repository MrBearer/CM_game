using UnityEngine;
using System.Collections;

public class Lighter : MonoBehaviour {

	public Light lamp;
	public float maxIntensity;
	public GameObject e_icon;
	public Texture2D e_icon2d;
	public int energyNeeded;
	public bool hasEnergy;
	Camera cam;
	CurTime ct;
	Placing pl;
	int ID;
	bool added;
	bool saved;
	SaveXML save;

	// Use this for initialization
	void Start () {
		ct = GameObject.FindGameObjectWithTag("Sun").GetComponent<CurTime>();
		pl = gameObject.GetComponent<Placing>();
		ID = ID_generator.generate_ID();
		lamp.enabled = false;
		hasEnergy = false;
		added = false;
		saved = false;
		cam = GameObject.FindGameObjectWithTag("MainCamera").camera;
		save = gameObject.GetComponent<SaveXML>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!pl.isGhost)
		{
			if (!saved)
			{
				save.save("outB",ID,transform.position);
				saved = true;
			}
			if (!added && ct.twilight)
			{
				Electricity.AddConsumer(energyNeeded,ID);
				added = true;
			}
			if (added && !ct.twilight)
			{
				Electricity.DeleteConsumer(ID);
				added = false;
			}
			if (added && Electricity.GetStatus(ID))
			{
				hasEnergy = true;
				MeshRenderer mr = e_icon.GetComponent<MeshRenderer>();
				mr.enabled = false;
			}
			else if (added)
			{
				hasEnergy = false;
				MeshRenderer mr = e_icon.GetComponent<MeshRenderer>();
				mr.enabled = true;
			}
			if (hasEnergy && ct.twilight && !lamp.enabled)
				lamp.enabled = true;
			if (!hasEnergy && ct.twilight && lamp.enabled)
				lamp.enabled = false;
			if (!ct.twilight && lamp.enabled)
				lamp.enabled = false;
		}
	}

	void OnGUI()
	{/*
		GUI.Label(new Rect(cam.WorldToScreenPoint(transform.position).x,cam,0,30,30),e_icon2d);
		if (!pl.isGhost)
		{
			if (added && Electricity.GetStatus(ID))
			{
				hasEnergy = true;
			}
			else if (added)
			{
				hasEnergy = false;
			}
		}*/
	}

	void SelfDestroy ()
	{
		pl.isGhost = true;
		if (added)
		{
			Electricity.DeleteConsumer(ID);
			Destroy(transform.gameObject);
		}
	}
}
