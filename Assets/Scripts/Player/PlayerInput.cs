using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

	[Header ("Accessor")]
	[SerializeField]
	CanvasManager m_Canvas;
	[SerializeField]
	PlayerEnergy m_PlayerEnergy;

	void Start () 
	{
		m_Canvas = GameObject.FindObjectOfType<CanvasManager>().transform.GetComponent<CanvasManager>();
		m_PlayerEnergy = GameObject.FindObjectOfType<PlayerEnergy>().GetComponent<PlayerEnergy>();
	}

	void Update() 
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			EatFood();
		}

		if (Input.GetKeyUp(KeyCode.R))
		{
			GameManager.instance.ResetEverything();
			print("Reset everything");
		}
	}

	//Eat food and recover energy
	void EatFood () 
	{
		m_Canvas.DeactiveFoodIcon();
		//TODO - set food energy value else where?
		m_PlayerEnergy.AddReduceEnergy(30);
	}
}
