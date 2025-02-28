using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour {
	
	RaycastHit hit;
	bool leftClickFlag = true;
	
	public GameObject actor;
	public string floorTag;
    public string characterTag;
    public GameObject characterMenu;

	Actor actorScript;
	
	void Start()
	{
		if (actor != null)
		{
			actorScript = (Actor)actor.GetComponent(typeof(Actor));
		}
	}
	
	void Update () 
	{
		/***Left Click****/
		if (Input.GetKey(KeyCode.Mouse0) && leftClickFlag)
			leftClickFlag = false;
		
		if (!Input.GetKey(KeyCode.Mouse0) && !leftClickFlag)
		{
			leftClickFlag = true;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 100))
			{
				if (hit.transform.tag == floorTag)
				{
					float X = hit.point.x;
					float Z = hit.point.z;
					Vector3 target = new Vector3(X, 0.1f, Z);
					actorScript.MoveOrder(target);
				}
                else if (hit.transform.tag == characterTag)
                {
                    Canvas canvas = (Canvas)characterMenu.GetComponent<Canvas>();
                    canvas.enabled = true;
                    CharacterMenu cm = (CharacterMenu)characterMenu.GetComponent<CharacterMenu>();
                    cm.selected = true;
                    hit.transform.tag = "selectedCharacter";
                }
			}
		}
	}
}
