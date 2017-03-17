using UnityEngine;
using System.Collections;

public class NameTrimmer : MonoBehaviour 
{
	void Start () 
	{
		transform.name = transform.name.Replace("(Clone)", "").Trim();
	}
}
