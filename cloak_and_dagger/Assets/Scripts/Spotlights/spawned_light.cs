using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawned_light : MonoBehaviour {

	public light_spawn_data light_Spawn_Data;
	

	void Start () {
		// TODO - Use the light_Spawn_Data here to initialize the light
		transform.position = light_Spawn_Data.pos;
	}
	
	void Update () {
		// TODO - count down the timer and adjust the light accordingly
	}
}
