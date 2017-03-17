using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour 
{
	public static LevelManager instance = null;

	void Awake ()
	{
		if (instance)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	public void Restart ()
	{
		int currentLevel = SceneManager.GetActiveScene().buildIndex;
		GameManager.instance.ResetLastHole();
		SceneManager.LoadScene(currentLevel);
	}

	public void LoadSceneByName (string levelName)
	{
		SceneManager.LoadScene(levelName);
	}

	public int GetLevelNumber()
	{
	 	int currentLevel = SceneManager.GetActiveScene().buildIndex;
		return currentLevel;
	}
}