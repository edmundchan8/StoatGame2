using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour 
{
	[SerializeField]
	Vector3 offset;
	[SerializeField]
	GameObject m_Player;

	void Start () 
	{
		m_Player = GameObject.FindObjectOfType<PlayerMove>().gameObject;
		offset = transform.position - m_Player.transform.position;
	}
	void LateUpdate ()
	{
		transform.position = m_Player.transform.position + offset;
	}

	public void ResetCameraPos(Vector3 pos) 
	{
		Vector3 posHolder = pos;
		transform.position = new Vector3(posHolder.x, posHolder.y, transform.position.z);
	}
}
