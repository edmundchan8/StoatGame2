using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour 
{
	[Header ("Variables")]
	[SerializeField]
	string LEVEL_NAME = "Ground0";

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject)
		{
			LevelManager.instance.LoadSceneByName(LEVEL_NAME);
		}
	}
}