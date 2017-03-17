using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerEnergy : MonoBehaviour 
{
	[SerializeField]
	Slider m_Slider;
	[SerializeField]
	Text m_SliderText;

	void Start () 
	{
		m_Slider = GetComponent<Slider>();
		m_Slider.value = GameManager.instance.GetPlayerEnergy();
		m_SliderText.text = GameManager.instance.GetPlayerEnergy() + "";
	}

	public void AddReduceEnergy (float amount) 
	{
		GameManager.instance.SetPlayerEnergy(amount);
		//Stops player energy exceeding 100
		if (GameManager.instance.GetPlayerEnergy() > 100)
		{
			GameManager.instance.ResetPlayerEnergy();
			GameManager.instance.SetPlayerEnergy(100);
		}
		m_SliderText.text = GameManager.instance.GetPlayerEnergy() + "";
	}
}
