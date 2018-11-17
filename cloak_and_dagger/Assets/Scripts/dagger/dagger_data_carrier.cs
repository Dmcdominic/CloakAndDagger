using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dagger_data_carrier : MonoBehaviour {

	[HideInInspector]
	public dagger_data dagger_Data;
	
}

[System.Serializable]
public struct dagger_data {
	// Add more dagger_data properties here, and include them in the constructor
	public bool collaterals;
	public byte thrower;

	public dagger_data(bool collaterals, byte thrower) {
		this.collaterals = collaterals;
        this.thrower = thrower;
	}
}
