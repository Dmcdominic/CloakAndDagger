using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class incineration_light : MonoBehaviour {

	public gameplay_config gameplay_Config;
	public float max_light_range;

	private bool growing = true;

	private new Light light;


	private void Awake() {
		light = GetComponent<Light>();
		light.range = max_light_range / 10f;
	}

	private void Update() {
		if (growing) {
			light.range = Mathf.Lerp(light.range, max_light_range, Time.deltaTime * 4f);
			if (light.range >= max_light_range * 0.9f) {
				growing = false;
			}
		} else {
			light.range = Mathf.Lerp(light.range, 0, Time.deltaTime * 4.5f);
		}
	}

}
