using UnityEngine;
using System.Collections;

public class AnimalDetect : MonoBehaviour 
{	
	[SerializeField]
	AnimalMove m_AnimalMove;
	[SerializeField]
	float SIGHT_DIST;
	[SerializeField]
	Transform m_Player;

	void Start() 
	{
		m_AnimalMove = GameObject.FindObjectOfType<AnimalMove>().gameObject.GetComponent<AnimalMove>();
		m_Player = Transform.FindObjectOfType<PlayerMove>().transform;
	}

	void Update () 
	{
		Debug.DrawRay(transform.position, m_AnimalMove.AnimalFacingDirection(), Color.black);
		RaycastHit2D hit = Physics2D.Raycast (transform.position, m_AnimalMove.AnimalFacingDirection(), SIGHT_DIST);
		if (hit.transform == m_Player.transform)
		{
			//I guess, only the player has a rigidbody in our game?
			//So if hit.rigidbody is detected
			m_AnimalMove.RunAway();
		}
	}
}
