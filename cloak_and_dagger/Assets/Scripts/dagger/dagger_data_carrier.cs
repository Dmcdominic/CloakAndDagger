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
	public uint thrown_index;

	public dagger_data(bool collaterals, byte thrower, uint thrown_index) {
		this.collaterals = collaterals;
        this.thrower = thrower;
		this.thrown_index = thrown_index;
	}
}
