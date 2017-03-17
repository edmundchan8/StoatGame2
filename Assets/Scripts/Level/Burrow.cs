using UnityEngine;
using System.Collections;

public class Burrow : MonoBehaviour 
{
	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject)
		{
			LevelManager.instance.LoadSceneByName("Burrow");
			//I want to set the 'last name holder' to null, so that we instantiate from the burrow, not any of the holes
			GameManager.instance.ResetLastHole();
		}
	}
}
