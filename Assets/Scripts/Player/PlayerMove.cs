using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayerMove : MonoBehaviour 
{
	[Header ("Constants")]
	[SerializeField]
	float PAUSE_DURATION;

	[Header ("Variables")]
	[SerializeField]
	bool m_CanLerp = true;
	[SerializeField]
	Vector3 m_StartPos;
	[SerializeField]
	Vector3 m_EndPos;
	[SerializeField]
	float WALK_ENERGY_EXPENDITURE = 0.5f;
	[SerializeField]
	Vector3 m_LastPos;

	[Header ("Accessors")]
	[SerializeField]
	PlayerEnergy m_PlayerEnergy;
	[SerializeField]
	Animator m_PlayerAnimator;

	Timer m_MoveTimer = new Timer();
		
	void Start () 
	{
		m_PlayerEnergy = GameObject.FindObjectOfType<PlayerEnergy>().GetComponent<PlayerEnergy>();
	}
		
	void Update () 
	{
		Move();
		if (!m_CanLerp)
		{
			CheckLerp();
		}
	}

	void Move()
	{
		m_MoveTimer.Update(Time.deltaTime);
		if (Input.GetKey(KeyCode.UpArrow) && m_MoveTimer.HasCompleted() && m_CanLerp)
		{
			m_MoveTimer.Set(PAUSE_DURATION);
			SetYLerp(1);
		}

		if (Input.GetKey(KeyCode.DownArrow) && m_MoveTimer.HasCompleted() && m_CanLerp)
		{
			m_MoveTimer.Set(PAUSE_DURATION);
			SetYLerp(-1);
		}

		if (Input.GetKey(KeyCode.RightArrow))
		{
			Vector2 facingDirection = transform.localScale;
			facingDirection.x = 1;
			transform.localScale = facingDirection;
			if (m_MoveTimer.HasCompleted() && m_CanLerp)
			{
				m_MoveTimer.Set(PAUSE_DURATION);
				SetXLerp(1);
			}
		} 
			
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			Vector2 facingDirection = transform.localScale;
			facingDirection.x = -1;
			transform.localScale = facingDirection;
			if (m_MoveTimer.HasCompleted() && m_CanLerp)
			{
				m_MoveTimer.Set(PAUSE_DURATION);
				SetXLerp(-1);
			}
		} 
		if (m_MoveTimer.HasCompleted())
		{
			m_PlayerAnimator.SetBool("isWalking", false);
		}
	}

	void SetYLerp (int y) 
	{
		if (m_CanLerp)
		{
			SetLastPos();
			m_PlayerEnergy.AddReduceEnergy(-WALK_ENERGY_EXPENDITURE);
			m_StartPos = transform.position;
			Vector3 posHolder = transform.position;
			posHolder.y = transform.position.y + y;
			m_EndPos = posHolder;
			m_CanLerp = false;
		}
	}

	void SetXLerp (int x) 
	{
		if (m_CanLerp)
		{
			SetLastPos();
			m_PlayerEnergy.AddReduceEnergy(-WALK_ENERGY_EXPENDITURE);
			m_StartPos = transform.position;
			Vector3 posHolder = transform.position;
			posHolder.x = transform.position.x + x;
			m_EndPos = posHolder;
			m_CanLerp = false;
		}
	}

	void CheckLerp () 
	{
		float percentage = ((PAUSE_DURATION - m_MoveTimer.Get()) / PAUSE_DURATION);
		transform.position = Vector3.Lerp(m_StartPos, m_EndPos, percentage);
		m_PlayerAnimator.SetBool("isWalking", true);
		if (percentage >= 1f)
		{
			m_CanLerp = true;
		}
	}

	public void SetLastPos()
	{
		m_LastPos = transform.position;
	}

	public Vector3 ReturnLastPos()
	{
		return m_LastPos;
	}

	public void ReturnPlayerToLastPos() 
	{
		m_CanLerp = true;
		transform.position = new Vector3 (m_LastPos.x, m_LastPos.y, 0f);
	}

	public void ReturnPlayerToGameBoard(float min, float max) 
	{
		//This will put the player back into the game board if they move out of the wall
		//the groundboardmanager currently controls this code and calls it when the player enters a wall.
		transform.position = new Vector3 ((Mathf.Clamp(transform.position.x, min, max)),(Mathf.Clamp(transform.position.y, min, max)),0f);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.GetComponent<Hole>())
		{
			SetLastPos();
		}
	}
}
