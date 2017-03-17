using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class GroundBoardManager : MonoBehaviour 
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
	public Count m_TreeNumber = new Count (3,6);
	[SerializeField]
	public Count m_HoleNumber = new Count (2,5);

	[Header ("GameObjects in game")]
	[SerializeField]
	List <GameObject> m_Grass = new List<GameObject>();
	[SerializeField]
	GameObject m_Wall;
	[SerializeField]
	GameObject m_Tree;
	[SerializeField]
	GameObject m_Burrow;

	[Header ("Holders")]
	[SerializeField]
	GameObject m_BoardHolder;

	[Header ("Lists")]
	[SerializeField]
	List <Vector3> m_GridPositions = new List <Vector3>();
	//Make a list of where all the trees are and make sure player can't move there
	[SerializeField]
	List <Vector3> m_RestrictedPos = new List <Vector3>();

	[Header ("Accessors")]
	[SerializeField]
	GameObject m_Player;
	[SerializeField]
	FollowPlayer m_FollowPlayer;

	[Header ("Variables")]
	[SerializeField]
	int m_Seed;

	[Header ("List of Gameobjects with tag 'Hole' ")]
	[SerializeField]
	List<GameObject> m_Holes = new List <GameObject>();

	void Start ()
	{
		m_Player = GameObject.FindObjectOfType<PlayerMove>().gameObject;
		m_FollowPlayer = GameObject.FindObjectOfType<FollowPlayer>().gameObject.GetComponent<FollowPlayer>();
		//TODO Later on, this should add the current level of the game, so that when you go to the next level in the game, it will create a new level.
		Random.InitState(m_Seed + LevelManager.instance.GetLevelNumber());
		SetupLevel();

		//TODO find all the Gameobjects with the tag "Hole" and add them to our list
		m_Holes.AddRange(GameObject.FindGameObjectsWithTag("Hole"));
		if (GameManager.instance.GetLastHoleName() != null)
		{
			m_Player.GetComponent<PlayerMove>().ReturnLastPos();
			for (int i = 0; i < m_Holes.Count; i++) 
			{
				if (m_Holes[i].name == GameManager.instance.GetLastHoleName())
				{
					GameObject currentListGameObject = m_Holes[i];
					m_Player.transform.position = new Vector3(currentListGameObject.transform.position.x - 1, currentListGameObject.transform.position.y, 0f);
					//send the player's transform to the resetCameraPos function, to use the player transform to help reset the camera's position.
					m_FollowPlayer.ResetCameraPos(m_Player.transform.position);
				}
			}
		}

	}

	void Update () 
	{
		if (m_Player.transform.position.x < -0.2f || m_Player.transform.position.x >= m_MaxRows - 0.2f || m_Player.transform.position.y < -0.2f || m_Player.transform.position.y >= m_MaxColumns- 0.8f)
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
		for (int xWallPos = -1; xWallPos < m_MaxRows + 1; xWallPos++)
		{
			for (int yWallPos = -1; yWallPos < m_MaxColumns + 1; yWallPos++)
			{
				if (xWallPos == -1 || xWallPos == m_MaxRows || yWallPos == -1 || yWallPos == m_MaxColumns)
				{
					//TODO - different m_Walls to choose from using random.range
					GameObject wallToInstantiate = m_Wall;
					Vector3 wallVector3 = new Vector3(xWallPos, yWallPos, 0);
					GameObject wall = Instantiate(wallToInstantiate, wallVector3, Quaternion.identity) as GameObject;
					wall.transform.parent = m_BoardHolder.transform;
				}
				else
				{
					GameObject tileToInstantiate = m_Grass[Random.Range (0, m_Grass.Count)];
					Vector3 tileVector3 = new Vector3(xWallPos, yWallPos, 0f);
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

		//I want to add this position to let the player know he can't move here.
		m_RestrictedPos.Add(randomPosition);

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
		LayoutObjectAtRandom(m_Tree, m_TreeNumber.minimum, m_TreeNumber.maximum);
		Instantiate(m_Burrow, new Vector3(0, 0, 0f), Quaternion.identity);
	}
}