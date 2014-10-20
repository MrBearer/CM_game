using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public double anger;
	public int updateSec;
	public int maxDistance;
	public Transform target;
	public double changeAim;
	public Vector3 offset;
	public int hitTimeout;
	public int damage;
	Camera cam;
	Vector3 home;
	bool isAttacking;
	bool wantsNewAim;
	float tS;
	float tSh;
	int tryNum;
	int ID;
	GameObject attackingGO;
	public int spawnID;

	// Use this for initialization
	void Start () {
		ID = ID_generator.generate_ID();
		isAttacking = false;
		tS = Time.time;
		tSh = Time.time;
		wantsNewAim = true;
		GameObject[] homes = GameObject.FindGameObjectsWithTag("Spawn");
		foreach (GameObject h in homes)
		{
			Spawn sp;
			sp = h.GetComponent<Spawn>();
			if (sp.ID == spawnID)
			{
				home = h.transform.position;
				Debug.Log ("home");
			}
		}
		cam = GameObject.FindGameObjectWithTag("MainCamera").camera;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isAttacking)
		{
			if (Time.time - tS > updateSec)
			{
				tS = Time.time;
				double attackProbability = Random.value;
				Debug.Log(attackProbability);
				if (attackProbability < anger)
				{
					isAttacking = true;
				}
				//Debug.Log(isAttacking);
			}
		}
		else
		{
			GameObject[] toAttack = GameObject.FindGameObjectsWithTag("CanAttack");
			tryNum = 0;
			while(wantsNewAim && tryNum < toAttack.Length)
			{
				tryNum ++;
				GameObject go = toAttack[Random.Range(0,toAttack.Length-1)];
				if (Vector3.Distance(go.transform.position, transform.position) < maxDistance && Vector3.Distance(transform.position,target.position) < 2 && wantsNewAim)
				{
					if (Vector3.Distance(go.transform.position,transform.position) >= 2)
					{
						target.transform.position = go.transform.position;
						attackingGO = go;
						wantsNewAim = false;
					}
				}
			}
			if (Vector3.Distance(transform.position,target.position) < 2 && Time.time - tSh > hitTimeout)
			{
				tSh = Time.time;
				Health h;
				h = attackingGO.GetComponent<Health>();
				h.damage (damage);
			}
			if (Time.time - tS > updateSec)
			{
				if (Random.value < changeAim)
					wantsNewAim = true;
				tS = Time.time;
				double attackProbability = Random.value;
				//Debug.Log(wantsNewAim);
				if (attackProbability > anger && Vector3.Distance(transform.position,target.position) < 2)
				{
					isAttacking = false;
					//target.transform.position = home + new Vector3(Random.Range(-offset.x, offset.x), Random.Range(0, offset.y), Random.Range(-offset.z, offset.z));
				}
				//Debug.Log(isAttacking);
			}
		}
	}

	void OnGUI ()
	{
		Vector3 screenPosition =
			cam.WorldToScreenPoint(transform.position);
		screenPosition.y = Screen.height - (screenPosition.y + 1);
		Rect rect = new Rect(screenPosition.x - 50,
		                     screenPosition.y - 80, 100, 24);
		GUI.Box(rect, isAttacking.ToString());
	}
}
