using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placed_torch : MonoBehaviour {

	public gameplay_config gameplay_Config;

	private float time_remaining;


	// Initialization
	private void Awake () {
		Light light = GetComponentInChildren<Light>();
		light.range = gameplay_Config.float_options[gameplay_float_option.torch_light_range];
		time_remaining = gameplay_Config.float_options[gameplay_float_option.torch_duration];
	}
	
	// Update is called once per frame
	void Update () {
		time_remaining -= Time.deltaTime;
		if (time_remaining <= 0) {
			destroy_torch();
		}
	}

	private void destroy_torch() {
		Destroy(gameObject);
	}
}
