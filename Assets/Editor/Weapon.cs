using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu]
public class Weapon : Item {

	public int		damage;

	void OnEnable()
	{
		Debug.Log("Enable Weapon");
	}

	void OnDisable()
	{
		Debug.Log("Disable Weapon");
	}
}
