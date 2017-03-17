using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour 
{
	[Header ("Accessors")]
	[SerializeField]
	Text m_Win;
	[SerializeField]
	Button m_Restart;

	[Header ("List")]
	[SerializeField]
	List <GameObject> m_FoodPickUpList = new List <GameObject>();
	[SerializeField]
	List <GameObject> m_ListHolder = new List <GameObject>();

	void Start () 
	{
		//a for loop, that puts all the food pick ups in ANOTHER LIST (I know, complicated)
		//Then, it takes them and puts them into the m_FoodPickUpList, but in numbered order
		m_ListHolder.AddRange(GameObject.FindGameObjectsWithTag("FoodPickUp"));
		for (int i = 0; i < m_ListHolder.Count; i++)
		{
			m_FoodPickUpList.Add(GameObject.Find("FoodPickUp" + i));
			m_FoodPickUpList[i].SetActive(false);
		}

		for (int a =0; a < GameManager.instance.GetNumberActiveFoodIcons(); a++)
		{
			m_FoodPickUpList[a].SetActive(true);
		}
	}

	public void ShowWinText()
	{
		m_Win.gameObject.SetActive(true);
		m_Restart.gameObject.SetActive(true);
	}

	public void OnRestartPressed () 
	{	
		LevelManager.instance.Restart();
	}

	public void ReturnNumberFoodIconsActive () 
	{
		for (int i = 0; i < m_FoodPickUpList.Count; i++)
		{
			if (m_FoodPickUpList[i].activeInHierarchy == false)
			{
				i = m_FoodPickUpList.Count;
				//This is set to 1 because at the moment, we need the game to record from an instance of the script that is always in the game
				//how many food icons are active in the game.  This sets a variable in the levelmanager to one when this code is run first time.
				//When this code is run at the first time, it is assumed the player hasn't picked up any food icons before that
				//if we use this elsewhere, it will increase the number of food activated above 4, our max.
				GameManager.instance.SetNumberActiveFoodIcons(1);
			}
		}
	}

	public void MakeFoodIconActive () 
	{
		//currentlyGetFoodIconInt - 1 because, the int goes from 0 - 4, but array is [0] - [3]
		//int 4 relates to [3], etc etc.
		m_FoodPickUpList[GameManager.instance.GetNumberActiveFoodIcons() - 1].SetActive(true);
	}

	public void DeactiveFoodIcon () 
	{
		if (GameManager.instance.GetNumberActiveFoodIcons() > 0)
		{
			m_FoodPickUpList[GameManager.instance.GetNumberActiveFoodIcons() - 1].SetActive(false);
			//Reduce the food icon count by 1
			GameManager.instance.SetNumberActiveFoodIcons(-1);
		}
	}
}