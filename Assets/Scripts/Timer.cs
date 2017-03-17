using UnityEngine;
using System.Collections;

public class Timer
{
	float m_Timer;

	public void Set (float time)
	{
		m_Timer = time;
	}

	public bool Update (float tick) 
	{
		if (m_Timer > 0.0f) 
		{
			m_Timer = Mathf.Max(m_Timer - tick, 0f);
			if (m_Timer <= 0.0f)
			{
				return true;
			}
		}
		return false;
	}

	public float Get ()
	{
		return m_Timer;
	}

	public bool HasCompleted () 
	{
		return m_Timer <= 0.0f;
	}
}
