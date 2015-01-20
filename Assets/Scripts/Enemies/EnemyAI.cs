using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public double anger;
	public int updateSec;
	public int maxDistance;
	private Vector3 target;
	public double changeAim;
	public Vector3 offset;
	public Vector3 walking;
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
	public int mySpawnID;
	bool foundSpawn;
    Actor actorScript;

    public void SetTarget(Vector3 pos)
    {
        target = pos;
        Debug.Log(target);
        actorScript = (Actor)transform.GetComponent(typeof(Actor));
        actorScript.MoveOrder(target);
    }

    public Vector3 GetTarget()
    {
        return target;
    }

	// Use this for initialization
	void Start () {
        actorScript = (Actor)transform.GetComponent(typeof(Actor));
		foundSpawn = false;
		ID = ID_generator.generate_ID();
		isAttacking = false;
		tS = Time.time;
		tSh = Time.time;
		wantsNewAim = true;
		cam = GameObject.FindGameObjectWithTag("MainCamera").camera;
	}
	
	// Update is called once per frame
	void Update () {
		if (!foundSpawn)
			findHome ();

		if (!isAttacking)
		{
			if (Time.time - tS > updateSec)
			{
				tryToAttack ();
				walk ();
			}
		}
		else
		{
			if (wantsNewAim)
				chooseAim ();

			if (Time.time - tSh > hitTimeout)
				damageAim ();

			if (Time.time - tS > updateSec)
			{
				if (Random.value < changeAim)
					wantsNewAim = true;
				tS = Time.time;
				double attackProbability = Random.value;
				if (attackProbability > anger && Vector3.Distance(transform.position,GetTarget()) < 2)
				{
					goHome ();
				}
			}
			if (attackingGO == null)
			{
				goHome ();
			}
		}
	}

	void findHome ()
	{
		GameObject[] homes = GameObject.FindGameObjectsWithTag("Spawn");

		foreach (GameObject h in homes)
		{
			Spawn sp;
			sp = h.GetComponent<Spawn>();
			if (sp != null && sp.ID == mySpawnID)
            {
				home = h.transform.position;
				foundSpawn = true;
			}
		}
	}

	void tryToAttack ()
	{
		tS = Time.time;
		double attackProbability = Random.value;
		if (attackProbability < anger)
		{
			isAttacking = true;
		}
	}

	void chooseAim ()
	{
		GameObject[] toAttack = GameObject.FindGameObjectsWithTag("CanAttack");
		tryNum = 0;
		while(tryNum < toAttack.Length)
		{
			tryNum ++;
			GameObject go = toAttack[Random.Range(0,toAttack.Length-1)];
			if (Vector3.Distance(go.transform.position, transform.position) < maxDistance && Vector3.Distance(transform.position,GetTarget()) < 2 && wantsNewAim)
			{
				if (Vector3.Distance(go.transform.position,transform.position) >= 2)
				{
                    SetTarget(Vector3.Scale(new Vector3(1, 0, 1), go.transform.position) + new Vector3(0,0.5f,0));
					attackingGO = go;
					wantsNewAim = false;
				}
			}
		}
	}

	void damageAim ()
	{
		if (Vector3.Distance(transform.position,GetTarget()) < 2)
		{
			tSh = Time.time;
			Health h;
			h = attackingGO.GetComponent<Health>();
			h.damage (damage);
		}
	}

	void goHome ()
	{
		isAttacking = false;
        SetTarget(home + new Vector3(Random.Range(-offset.x, offset.x), 0.5f, Random.Range(-offset.z, offset.z)));
	}

	void walk ()
	{
        SetTarget(transform.position + new Vector3(Random.Range(-walking.x, walking.x), 0.5f, Random.Range(-walking.z, walking.z)));
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
