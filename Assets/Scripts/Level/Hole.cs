using UnityEngine;
using System.Collections;

public class Hole : MonoBehaviour 
{
	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject)
		{
			GameManager.instance.SetLastHole(name);
			LevelManager.instance.LoadSceneByName(gameObject.name);
		}
	}
}
