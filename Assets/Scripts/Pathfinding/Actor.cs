using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Actor : MonoBehaviour {
	
	enum State
	{
		IDLE,
		MOVING,
	}
	
	float m_speed;
    public float m_speed_multi = 0.1f;
	public bool DebugMode;
	
	bool onNode = true;
	Vector3 m_target = new Vector3(0, 0, 0);
	Vector3 currNode;
	int nodeIndex;
	List<Vector3> path = new List<Vector3>();
	NodeControl control;
	State state = State.IDLE;
	float OldTime = 0;
	float checkTime = 0;
	float elapsedTime = 0;

    Animator animator;
	
	void Awake()
	{
		GameObject cam = GameObject.FindGameObjectWithTag("CameraPivot");
		control = (NodeControl)cam.GetComponent(typeof(NodeControl));
        animator = (Animator)GetComponent<Animator>();
	}
	
	void Update () 
	{
		m_speed = Time.deltaTime * m_speed_multi;
		elapsedTime += Time.deltaTime;
		
		if (elapsedTime > OldTime)
		{
			switch (state)
			{
			case State.IDLE:
                if (animator != null)
                    animator.SetBool("isWalking", false);
				break;

            case State.MOVING:
                if (animator != null)
                    animator.SetBool("isWalking", true);
				OldTime = elapsedTime + 0.01f;

				if (elapsedTime > checkTime)
				{
					checkTime = elapsedTime + 1;
					SetTarget();
				}
				
				if (path != null)
				{
					if (onNode)
					{
						onNode = false;

                        transform.LookAt(path[nodeIndex]);
						if (nodeIndex < path.Count)
							currNode = path[nodeIndex];
					} else
						MoveToward();
				}
				break;
			}
		}
	}
	
	void MoveToward()
	{
		if (DebugMode)
		{
			for (int i=0; i<path.Count-1; ++i)
			{
				Debug.DrawLine((Vector3)path[i], (Vector3)path[i+1], Color.white, 0.01f);
			}
		}
		
		Vector3 newPos = transform.position;

		float Xdistance = newPos.x - currNode.x;
		if (Xdistance < 0) Xdistance -= Xdistance*2;
		float Ydistance = newPos.z - currNode.z;
		if (Ydistance < 0) Ydistance -= Ydistance*2;

		if ((Xdistance < 0.5 && Ydistance < 0.5) && m_target == currNode) //Reached target
		{
			ChangeState(State.IDLE);
		}
		else if (Xdistance < 0.5 && Ydistance < 0.5)
		{
			nodeIndex++;
			onNode = true;
		}
		
		/***Move toward waypoint***/
		Vector3 motion = currNode - newPos;
		motion.Normalize();
		newPos += motion * m_speed;
		
		transform.position = newPos;
	}
	
	private void SetTarget()
    {
		path = control.Path(transform.position, m_target);

        //Debug.Log(path);
		nodeIndex = 0;
		onNode = true;
	}
	
	public void MoveOrder(Vector3 pos)
	{
		m_target = pos;
		SetTarget();
		ChangeState(State.MOVING);
	}
	
	private void ChangeState(State newState)
	{
		state = newState;
	}
}
