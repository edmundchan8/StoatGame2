using UnityEngine;
using System.Collections;

public class AnimalMove : MonoBehaviour 
{
	[SerializeField]
	float VELOCITY;
	[SerializeField]
	float LERP_DURATION;
	[SerializeField]
	float YIELD_DURATION;
	[SerializeField]
	float DEATH_COUNT_DOWN;
	[SerializeField]
	float SPEED;

	[SerializeField]
	Timer m_MoveTimer = new Timer();
	[SerializeField]
	float X_MOVE_DISTANCE;
	[SerializeField]
	float Y_MOVE_DISTANCE;

	[SerializeField]
	Vector3 m_CurrentPos;
	[SerializeField]
	Vector3 m_EndPos;

	[SerializeField]
	bool m_RunAway = false;

	void Start () 
	{
		SetLerp();
	}

	void FixedUpdate() 
	{
		if (m_MoveTimer.Update(Time.deltaTime) && !m_RunAway)
		{
			Invoke("SetLerp", YIELD_DURATION);
		}
		else if (m_RunAway) 
		{
			transform.Translate(AnimalFacingDirection() * -SPEED * Time.deltaTime);
		}

		else
		{
			//1 - (current time / lerp duration) 
			//when percentage = 0, this is the starting vector position
			//when percentage = 1, this is the ending vector position
			float percentage = (1 -(m_MoveTimer.Get() / LERP_DURATION));
			transform.position = Vector3.Lerp(m_CurrentPos, m_EndPos, percentage);
		}
	}

	void SetLerp () 
	{
		//set the start position
		m_CurrentPos = transform.position;
		//set the end position
		m_EndPos = new Vector3(m_CurrentPos.x + X_MOVE_DISTANCE, m_CurrentPos.y + Y_MOVE_DISTANCE, 0f);
		//this = -1 so that when we run this code again, the rabbit should move to its original position;
		X_MOVE_DISTANCE *= -1;
		Y_MOVE_DISTANCE *= -1;
		//restart the timer after a few seconds
		m_MoveTimer.Set(LERP_DURATION);
	}

	public Vector3 AnimalFacingDirection ()
	{
		return new Vector3(X_MOVE_DISTANCE, Y_MOVE_DISTANCE, 0f) * -1;
	}

	public void RunAway () 
	{
		m_RunAway = true;
		m_MoveTimer.Set(0);
		Invoke("SelfDestruct", DEATH_COUNT_DOWN);
	}

	public void SelfDestruct () 
	{
		Destroy(gameObject);
	}
}
