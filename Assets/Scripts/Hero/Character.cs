using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    public float mood;

    bool sex;
    bool drunk;

    public float[] koef;

    public Sprite avatar;

    public float speed = 0.01f;
    
    float bladder;
    float hungry;
    float health;
    float energy;

    float work;
    float social;
    float fun;

    float max;

    Health healthS;

    public float[] GetMood()
    {
        float[] curMood = new float[8];
        
        curMood[0] = bladder;
        curMood[1] = hungry;
        curMood[2] = health;
        curMood[3] = energy;

        curMood[4] = work;
        curMood[5] = social;
        curMood[6] = fun;

        curMood[7] = mood;

        return curMood;
    }

	// Use this for initialization
	void Start () {
        healthS = (Health)GetComponent<Health>();

        bladder = 80;
        hungry = 80;
        health = healthS.maxHealth;
        energy = 80;

        work = 80;
        social = 80;
        fun = 80;
	}
	
	// Update is called once per frame
	void Update () {
        max = (100 * koef[0] + 100 * koef[1] + 100 * koef[2] + 100 * koef[3]) * 0.3f + (100 * koef[4] + 100 * koef[5] + 100 * koef[6]) * 0.7f;
        mood = (((bladder * koef[0] + hungry * koef[1] + health * koef[2] + energy * koef[3]) * 0.3f + (work * koef[4] + social * koef[5] + fun * koef[6]) * 0.7f)/max)*100;

        hungry -= speed * koef[1] * Time.deltaTime;
	}
}
