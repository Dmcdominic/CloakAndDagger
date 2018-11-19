using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball_data_carrier : MonoBehaviour {

	[HideInInspector]
	public fireball_data fireball_Data;
	
}

[System.Serializable]
public struct fireball_data {
	// Add more fireball_data properties here, and include them in the constructor
	public bool collaterals;
	public byte thrower;

	public fireball_data(bool collaterals, byte thrower) {
		this.collaterals = collaterals;
        this.thrower = thrower;
	}
}
