using UnityEngine;
using System.Collections;

public class PlayerContact : MonoBehaviour {

	[Header ("Accessor")]
	[SerializeField]
	CanvasManager m_Canvas;

	[Header ("Variables")]
	//TODO Way too hardcoded, basically, this should be on the Food and Animal and any other gameobject.
	//each food source has a different number
	//depending on this number, this will run the Set the number of food icons to be activated, higher number, more food icons active, therefore
	//player holds more food.
	//The idea is that bigger animals provide more food.
	int m_NumberIconsToActivate;


	void Start () 
	{
		m_Canvas = GameObject.FindObjectOfType<CanvasManager>().transform.GetComponent<CanvasManager>();
	}

	public void OnTriggerEnter2D (Collider2D other) 
	{
		if (other.gameObject.GetComponent<Food>())
		{
			//TODO
			m_NumberIconsToActivate = 1;
			while (m_NumberIconsToActivate > 0)
			{
				SetNumberFoodIconsActive();
			}
			Destroy(other.gameObject);
		}
		else if (other.gameObject.GetComponent<AnimalMove>())
		{
			//TODO
			m_NumberIconsToActivate = 2;
			while (m_NumberIconsToActivate > 0)
			{
				SetNumberFoodIconsActive();
			}
			Destroy(other.gameObject);
		}
	}

	public void SetNumberFoodIconsActive ()
	{
		m_Canvas.ReturnNumberFoodIconsActive();
		m_Canvas.MakeFoodIconActive();
		m_NumberIconsToActivate--;
	}
}
