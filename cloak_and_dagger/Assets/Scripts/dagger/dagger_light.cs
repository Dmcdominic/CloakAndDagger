using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class dagger_light : MonoBehaviour {

	public gameplay_config gameplay_Config;


	private void Awake() {
		Light light = GetComponent<Light>();
		light.spotAngle = gameplay_Config.float_options[gameplay_float_option.dagger_light_radius];
	}

}
