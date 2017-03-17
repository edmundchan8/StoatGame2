using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class HoleBoardManager : MonoBehaviour 
{
	// Using Serializable allows us to embed a class with sub properties in the inspector.
	//we make a class called Count
	[Serializable]
	public class Count
	{
		//This class Count has 2 public variables, minimum and maximum/
		public int minimum;
		public int maximum;

		//This public function, of type Count?) called Count takes two parameters (min and max)
		public Count (int min, int max)
		{
			//This function then sets the public minimum and maximum to whatever 2 parameters you set to this function ( )
			minimum = min;
			maximum = max;
		}
	}

	[Header ("Board variables")]
	[SerializeField]
	int m_MaxColumns;
	[SerializeField]
	int m_MaxRows;

	[Header ("Item Variables")]
	//using the Count ( ) from about, we can make more Count classes, names them and give them new minimum's and maximums.
	//In this case, it is just food, but we can do this with walls or hazards as well.
	[SerializeField]
	public Count m_FoodNumber = new Count (3,6);

	[Header ("GameObjects in game")]
	[SerializeField]
	GameObject m_Wall;
	[SerializeField]
	GameObject m_Exit;
	[SerializeField]
	GameObject m_Floor;
	[SerializeField]
	GameObject m_Food;

	[Header ("Holders")]
	[SerializeField]
	GameObject m_BoardHolder;

	[Header ("Lists")]
	[SerializeField]
	List <Vector3> m_GridPositions = new List <Vector3>();

	[Header ("Accessors")]
	[SerializeField]
	GameObject m_Player;

	void Start () 
	{
		m_Player = GameObject.FindObjectOfType<PlayerMove>().gameObject;
		//TODO Later on, this should add the current level of the game, so that when you go to the next level in the game, it will create a new level.
		Random.InitState(Random.Range(0,100) + LevelManager.instance.GetLevelNumber());
		SetupLevel();
	}

	void Update () 
	{
		if (m_Player.transform.position.x < -0.2f || m_Player.transform.position.x >= m_MaxRows - 0.8f 
			|| m_Player.transform.position.y < -0.2f || m_Player.transform.position.y >= m_MaxColumns - 0.8f)
		{
			//in my game, the max rows and columns are always going to be the same
			m_Player.GetComponent<PlayerMove>().ReturnPlayerToGameBoard(0, m_MaxColumns-1);
		}
	}

	void TileSetup () 
	{
		//Clear the grid positions in the list for each level
		m_GridPositions.Clear();

		if (m_BoardHolder == null)
		{
			m_BoardHolder = new GameObject("BoardHolder");
		}

		// for each xPos, if it is less than the maximum rows we are allowed, run code and increase xPos by 1
		for (int xPos = 1; xPos < m_MaxRows-1; xPos++)
		{
			//as above, but with yPos
			for (int yPos = 1; yPos < m_MaxColumns-1; yPos++)
			{
				//as the vector 3s of each positions into our list
				m_GridPositions.Add(new Vector3 (xPos, yPos, 0));
			}
		}
	}

	void WallFloorSetup() 
	{
		for (int xPos = -1; xPos < m_MaxRows + 1; xPos++)
		{
			for (int yPos = -1; yPos < m_MaxColumns + 1; yPos++)
			{
				if (xPos == -1 || xPos == m_MaxRows || yPos == -1 || yPos == m_MaxColumns)
				{
					//TODO - different m_Walls to choose from using random.range
					GameObject wallToInstantiate = m_Wall;
					Vector3 wallVector3 = new Vector3(xPos, yPos, 0);
					GameObject wall = Instantiate(wallToInstantiate, wallVector3, Quaternion.identity) as GameObject;
					wall.transform.parent = m_BoardHolder.transform;
				}
				else
				{
					GameObject tileToInstantiate = m_Floor;
					Vector3 tileVector3 = new Vector3(xPos, yPos, 0f);
					GameObject tile = Instantiate(tileToInstantiate, tileVector3, Quaternion.identity) as GameObject;
					tile.transform.parent = m_BoardHolder.transform;
				}
			}
		}
	}

	void LayoutObjectAtRandom (GameObject theObject, int minimum, int maximum) 
	{
		int maxGameObjectInstantiate = Random.Range(minimum, maximum);
		for (int i = 0; i < maxGameObjectInstantiate; i++)
		{
			//hold the original name, then create a new prefab with an 'i' integer at the end of the name
			string nameHolder = theObject.name;
			theObject.name += i;
			Vector3 objectInstantiatePos = RandomPosition();
			Instantiate(theObject, objectInstantiatePos, Quaternion.identity);
			//reset the name of the object
			theObject.name = nameHolder;
		}
	}

	//This function will create a random number base on the size of the list that holds the vector3s
	//then sets it, removes that position from the list and returns it
	Vector3 RandomPosition () 
	{
		//create a random index for the List position, random from 0 to size of list - m_GridPositions
		int randomIndex = Random.Range(0, m_GridPositions.Count);
		//create and give a vector 3 variable the random vector3 position from the list
		Vector3 randomPosition = m_GridPositions[randomIndex];
		//make sure to remove this index, so that we don't use it again, no duplicate vector3 positions
		m_GridPositions.RemoveAt(randomIndex);
		//return the random position
		return randomPosition;
	}

	//to set up the level, take in the methods that we made before and run this once to set up a level
	void SetupLevel () 
	{
		TileSetup();
		WallFloorSetup();
		LayoutObjectAtRandom(m_Food, m_FoodNumber.minimum, m_FoodNumber.maximum);
		Instantiate(m_Exit, new Vector3(0, 0, 0f), Quaternion.identity);
	}
}