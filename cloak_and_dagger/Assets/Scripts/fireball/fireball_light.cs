using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class fireball_light : MonoBehaviour {

	public gameplay_config gameplay_Config;
	public float z_height;


	private void Awake() {
		transform.position = new Vector3(transform.position.x, transform.position.y, z_height);
		Light light = GetComponent<Light>();
		light.range = gameplay_Config.float_options[gameplay_float_option.fireball_light_range];
	}

}
