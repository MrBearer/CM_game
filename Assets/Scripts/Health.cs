using UnityEngine;
using System.Collections;
using Pathfinding;

public class Health : MonoBehaviour {

	public int maxHealth;
	int curHealth;
	Vector3 screenPosition;
	Camera cam;
	public GameObject toDestroy;
	// Use this for initialization
	void Start () {
		curHealth = maxHealth;
		cam = GameObject.FindGameObjectWithTag("MainCamera").camera;
	}
	
	// Update is called once per frame
	void Update () {
		if(curHealth <= 0)
		{
			Destroy(toDestroy);
            Collider[] cl = toDestroy.transform.GetComponentsInChildren<Collider>();
            foreach (Collider ecl in cl)
            {
                AstarPath.active.UpdateGraphs(new GraphUpdateObject(ecl.bounds), 1);
            }
		}
	}

	void OnGUI ()
	{
		screenPosition =
			cam.WorldToScreenPoint(transform.position);
		screenPosition.y = Screen.height - (screenPosition.y + 1);
		Rect rect = new Rect(screenPosition.x - 50,
		                     screenPosition.y - 60, 100, 24);
		GUI.Box(rect, curHealth + "/" + maxHealth);
	}

	public void damage (int damageAmount)
	{
		curHealth -= damageAmount;
	}
}
