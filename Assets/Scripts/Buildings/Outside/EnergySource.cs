using UnityEngine;
using System.Collections;

public class EnergySource : MonoBehaviour {

	public int energyGenerated;
	bool added;
	int ID;
	Placing pl;

	// Use this for initialization
	void Start () {
		added = false;
		pl = gameObject.GetComponent<Placing>();
		ID = ID_generator.generate_ID();
	}
	
	// Update is called once per frame
	void Update () {
		if (!pl.isGhost && !added)
		{
			added = true;
			Electricity.AddEnergySource(energyGenerated,ID);
		}
	}
}
