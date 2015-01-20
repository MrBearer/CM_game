using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	public GameObject enemy;
	public int maxEnemyNum;
	public double prob;
	public float updateSec;
	public Vector3 offset;
	float tS;
	int numOfEnemies;
	public int ID;
	Placing pl;

	// Use this for initialization
	void Start () {
		ID = ID_generator.generate_ID();
		tS = Time.time;
		numOfEnemies = 0;
		pl = gameObject.GetComponent<Placing>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!pl.isGhost)
		{
			if (Time.time - tS > updateSec)
			{
				tS = Time.time;
				if (Random.value < prob && numOfEnemies < maxEnemyNum)
				{
					GameObject newEnemy = (GameObject)Instantiate(enemy,new Vector3(transform.position.x,transform.position.y + 5,transform.position.z),Quaternion.identity);
					EnemyAI ai;
					ai = newEnemy.GetComponent<EnemyAI>();
					ai.mySpawnID = ID;
					ai.SetTarget(transform.position + new Vector3(Random.Range(-offset.x, offset.x), Random.Range(0, offset.y), Random.Range(-offset.z, offset.z)));
					numOfEnemies++;
				}
			}
		}
	}
}
