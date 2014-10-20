using UnityEngine;
using System.Collections;

public class StringToVector3 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	static public string makeValidName(string name)
	{
		string validName = "";
		for (int j = 0; j < name.Length; j++)
		{
			if (name[j] != '(' && name[j] != ')')
			{
				validName += name[j];
			}
		}
		return validName;
	}

	static public int string2int(string sID)
	{
		string id = "";
		for (int j = 0; j < sID.Length; j++)
		{
			id += sID[j];
		}
		int k=-1;
		if (int.TryParse(id, out k))
		{
			return k;
		}
		else return -1;
	}
	
	static public Vector3 string2vector3(string s3)
	{
		Vector3 v3 = Vector3.zero;
		int i = 0;
		string num = "";
		for (int j = 1; j < s3.Length && i<3; j++)
		{
			//Debug.Log(s3[j]);

			if (s3[j] == '.')
			{
				if (i<2)
					j+=3;
				int k=-1;
				if (int.TryParse(num, out k))
				{
					if (i==0)
						v3.x = k;
					if (i==1)
						v3.y = k;
					if (i==2)
						v3.z = k;
				}
				i++;
				num = "";
			}
			num += s3[j];
			Debug.Log(v3);
		}
		return v3;
	}
}