﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dagger_data_carrier : MonoBehaviour {

	public dagger_data dagger_Data;
	
}

public struct dagger_data {

	public dagger_data(bool collaterals) {
		this.collaterals = collaterals;
	}

	// Add more dagger_data properties here, and include them in the constructor
	public bool collaterals;

}