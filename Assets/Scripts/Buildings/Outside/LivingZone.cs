using UnityEngine;
using System.Collections;

public class LivingZone : MonoBehaviour {

	int ID;
	public int energyNeeded;
	public GameObject e_icon;
	bool added;
	Placing pl;
	bool hasEnergy;

	// Use this for initialization
	void Start () {
		added = false;
		pl = gameObject.GetComponent<Placing>();
		ID = ID_generator.generate_ID();
		hasEnergy = false;
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
				MeshRenderer mr = e_icon.GetComponent<MeshRenderer>();
				mr.enabled = false;
			}
			else
			{
				hasEnergy = false;
				MeshRenderer mr = e_icon.GetComponent<MeshRenderer>();
				mr.enabled = true;
			}
		}
	}

}
