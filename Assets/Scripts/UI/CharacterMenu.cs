using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterMenu : MonoBehaviour {

    public GameObject[] texts;
    GameObject selectedCharacter;
    public bool selected;
    public GameObject Avatar;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (selected)
        {
            selectedCharacter = GameObject.FindGameObjectWithTag("selectedCharacter");
            float[] mood = selectedCharacter.GetComponent<Character>().GetMood();
            int i = 0;
            foreach (float value in mood)
            {
                texts[i].GetComponent<Text>().text = Mathf.RoundToInt(value).ToString();
                i++;
            }
            Avatar.GetComponent<Image>().sprite = selectedCharacter.GetComponent<Character>().avatar;
        }
	}
}
