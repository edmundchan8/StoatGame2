using UnityEngine;
using System.Collections;

public class PushOut : MonoBehaviour 
{
	[Header ("Accessor")]
	[SerializeField]
	PlayerMove m_PlayerMove;

	void Start () 
	{
		m_PlayerMove = GameObject.FindObjectOfType<PlayerMove>().gameObject.GetComponent<PlayerMove>();
	}

	void Update () 
	{
	}

	public void OnTriggerStay2D(Collider2D other) 
	{
		if (other.gameObject)
		{
			m_PlayerMove.ReturnPlayerToLastPos();
		}
	}
}
