using UnityEngine;
using System.Collections;
using Pathfinding;

public class Placing : MonoBehaviour {

	public bool direct;
	public bool isGhost;
	public bool updated;

	// Use this for initialization
	void Start () {
		isGhost = true;
		updated = false;
	}
	
	// Update is called once per frame
	void Update () {/*
		if(!updated && !isGhost)
		{
			Collider[] cl = transform.GetComponentsInChildren<Collider>();
			foreach (Collider ecl in cl)
			{
				AstarPath.active.UpdateGraphs(new GraphUpdateObject(ecl.bounds));
			}
			updated = true;
		}*/
	}

    void SelfDestroy()
    {
        Destroy(gameObject);/*
        Collider[] cl = transform.GetComponentsInChildren<Collider>();
        foreach (Collider ecl in cl)
        {
            AstarPath.active.UpdateGraphs(new GraphUpdateObject(ecl.bounds),1);
        }*/
        updated = true;
    }
}
