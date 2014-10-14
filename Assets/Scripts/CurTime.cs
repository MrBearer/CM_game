using UnityEngine;
using System.Collections;

public class CurTime : MonoBehaviour {

	public Light sun;
	public double maxSunHeight = 60;
	public double dayLength = 30;
	public double tfLengthSec = 60;
	public double midnightSunRotation = 180;
	public double maxSunIntensity = 0.35;
	public bool twilight;
	double startTime;
	double curSunHeight;
	double curSunRotation;
	double curDayTime;
	double curDayLength;
	bool isDay;

	// Use this for initialization
	void Start () {
		twilight = false;
		maxSunHeight *= 2;
		isDay = true;
		curSunHeight = maxSunHeight;
		curSunRotation = midnightSunRotation/2;
		curDayTime = dayLength/2;
		curDayLength = dayLength;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (curDayTime/dayLength);
		if (curDayTime < curDayLength)
		{
			if (isDay)
			{
				if (curDayTime/dayLength <= 0.5)
				{
					curSunHeight = maxSunHeight*(curDayTime/dayLength);
					if (curDayTime/dayLength <= 0.1)
						sun.intensity = (float)((curDayTime/dayLength)*10*maxSunIntensity);
					else
						twilight = false;
				}	
				else
				{
					curSunHeight = maxSunHeight*(1 - curDayTime/dayLength);
					if (curDayTime/dayLength >= 0.9)
					{
						twilight = true;
						sun.intensity = (float)((1 - curDayTime/dayLength)*10*maxSunIntensity);
					}
				}
			}
			curDayTime += Time.deltaTime;
		}
		else
		{
			isDay = !isDay;
			sun.enabled = !sun.enabled;
			curDayTime = 0;
			if(isDay)
				curDayLength = dayLength;
			else
				curDayLength = tfLengthSec - dayLength;
		}
		curSunRotation = midnightSunRotation * (curDayTime/dayLength);
		sun.transform.localEulerAngles = new Vector3 ((float)curSunHeight, (float)curSunRotation, sun.transform.rotation.z);
	}
}
