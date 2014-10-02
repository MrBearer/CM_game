using UnityEngine;
using System.Collections;

public class CharacterMoving : Pathfinding  {
	
	public Camera cam;
	Vector3 endPosition = new Vector3(90, 3, 87);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0))
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit))
			{
				//endPosition = hit.point;
				FindPath(transform.position, endPosition);
				Debug.Log(endPosition);
				if (Path.Count > 0)
				{
					Move();
				}   
			}

		}
	}
}
