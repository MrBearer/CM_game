using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Electricity : MonoBehaviour {

	int supply;
	int income;
	private static Dictionary<int,int> needs = new Dictionary<int,int>();
	private static Dictionary<int,int> incoming = new Dictionary<int,int>();
	private static Dictionary<int,bool> approved = new Dictionary<int,bool>();
	private static bool needsUpdate;

	// Use this for initialization
	void Start () {
		supply = 0;
		needsUpdate = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (needsUpdate)
		{
			supply = 0;

			foreach (var item in incoming.OrderBy(key => key.Value))
			{
				supply += item.Value;
			}

			income = supply;

			foreach (var item in needs.OrderByDescending(key => key.Value))
			{
				Debug.Log(item.Value);
				if (supply >= item.Value)
				{
					supply -= item.Value;
					approved[item.Key] = true;
				}
				else
					approved[item.Key] = false;
			}
			needsUpdate = false;
		}
	}

	public static void AddConsumer (int eAmount, int id)
	{
		needs[id] = eAmount;
		approved[id] = false;
		needsUpdate = true;
	}

	public static void DeleteConsumer (int id)
	{
		needs.Remove(id);
		approved.Remove(id);
		needsUpdate = true;
	}

	public static void AddEnergySource (int eAmount, int id)
	{
		incoming[id] = eAmount;
		needsUpdate = true;
	}

	public static void RemoveEnergySource (int id)
	{
		incoming.Remove(id);
		needsUpdate = true;
	}
	
	public static bool GetStatus (int id)
	{
		return approved[id];
	}

	void OnGUI() {
		GUI.Box(new Rect(10, Screen.height - 50, 200, 30), "Energy: " + supply + "/" + income);
	}
}
