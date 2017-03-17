using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	void Awake() 
	{
		if (instance)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
			//When game starts, reset player energy to make sure energy is set to 0, then set player energy to 100
			ResetPlayerEnergy();
			SetPlayerEnergy(100);
		}
	}

	// Food icons values - PlayerPrefs name is " FoodIcon "
	public void SetNumberActiveFoodIcons (int amount)
	{
		int currentFoodIconAmount = GetNumberActiveFoodIcons();
		PlayerPrefs.SetInt("FoodIcon", currentFoodIconAmount + amount);
	}

	public int GetNumberActiveFoodIcons() 
	{
		return PlayerPrefs.GetInt("FoodIcon");
	}

	public void ResetActiveFoodIcons() 
	{
		PlayerPrefs.DeleteKey("FoodIcon");
	}

	// Last Hole name - PlayerPrefs name is " LastHole"
	//Notes from before - This should be able to save the last hole that the player went into (because this script has the static instance
	//Then when the player moves back to the ground again, read what the last hole was and make sure that the player moves to just outside the hole when they
	//return to the ground again.
	public void SetLastHole (string name)
	{
		PlayerPrefs.SetString("LastHole", name);
	}

	public string GetLastHoleName() 
	{
		return PlayerPrefs.GetString("LastHole");
	}

	public void ResetLastHole()
	{
		PlayerPrefs.DeleteKey("LastHole");
	}

	// PlayerEnergy - PlayerPrefs name is "PlayerEnergy"

	public void SetPlayerEnergy(float amount)
	{
		float currentEnergy = GetPlayerEnergy();
		PlayerPrefs.SetFloat("PlayerEnergy", currentEnergy + amount);
	}

	public float GetPlayerEnergy()
	{
		return PlayerPrefs.GetFloat("PlayerEnergy");
	}

	public void ResetPlayerEnergy()
	{
		PlayerPrefs.DeleteKey("PlayerEnergy");
	}

	//WARNING - Resets all values
	public void ResetEverything() 
	{
		PlayerPrefs.DeleteAll();
	}
}
