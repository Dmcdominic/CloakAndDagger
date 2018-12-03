using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class fireball_light : MonoBehaviour {

	public gameplay_config gameplay_Config;
	public float z_height;

	private new Light light;
	private float target_range;


	private void Awake() {
		transform.position = new Vector3(transform.position.x, transform.position.y, z_height);
		light = GetComponent<Light>();
		target_range = gameplay_Config.float_options[gameplay_float_option.fireball_light_range];
		light.range = target_range / 10f;
	}

	private void Update() {
		light.range = Mathf.Lerp(light.range, target_range, Time.deltaTime * 3f);
	}

}
