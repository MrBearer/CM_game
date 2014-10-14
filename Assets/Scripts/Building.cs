using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {

	public GameObject[] buildings;
	public Vector3[] offset;
	public Camera cam;
	private int item;
	private bool isSelected;
	private GameObject ghost;
	private Vector3 oldPos;
	private bool grid;
	private int colliding;
	public static int bMode;

	// Use this for initialization
	void Start () {
		isSelected = false;
		grid = true;
		colliding = 0;
		bMode = 0;
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log(colliding);
		if (isSelected)
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit))
			{
				if (hit.transform.tag == "Terrain")
				{
					Vector3 newPos = hit.point + offset[item];
					if (grid)
						newPos = new Vector3 (Mathf.Round(newPos.x), newPos.y, Mathf.Round(newPos.z));
					ghost.transform.position = newPos;
				}
			}
			if(colliding == 0 && Input.GetMouseButton(0))
			{
				Placing pg = ghost.GetComponent<Placing>();
				pg.isGhost = false;
				bMode = 0;
				isSelected = false;
			}
			if(Input.GetMouseButton(1))
			{
				isSelected = false;
				Destroy(ghost);
				bMode = 0;
			}
		}
		if (bMode == 2)
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit))
			{
				if (hit.transform.tag == "CanDestroy" && Input.GetMouseButton(0))
				{
					hit.transform.gameObject.SendMessage("SelfDestroy");
				}
			}
		}
		if (Input.GetKeyUp (KeyCode.G))
			grid = !grid;
	}

	void OnGUI ()
	{
		int i = 0;
		foreach (GameObject bld in buildings)
		{
			if (GUI.Button(new Rect(10+i*90, 10, 80, 30), bld.gameObject.name.ToString()))
			{
				item = i;
				isSelected = true;
				bMode = 1;
				Ray ray = cam.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if(Physics.Raycast(ray,out hit))
				{
					ghost = (GameObject)Instantiate(buildings[item],hit.point + offset[item],Quaternion.identity);
				}
			}
			i++;
		}
		if (GUI.Button(new Rect(10, Screen.height-80, 80, 30), "Remove"))
		{
			bMode = 2;
			if (isSelected)
			{
				isSelected = false;
				Destroy(ghost);
			}
		}
	}

	void Colliding (bool enter)
	{
		if (enter)
			colliding++;
		else
			colliding--;
		//Debug.Log(colliding);
	}
}
