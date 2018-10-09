using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawned_light : MonoBehaviour {

	public Light attached_light;
	public light_spawn_data light_Spawn_Data;

	private float initial_intensity;

	[SerializeField]
	private float fade_duration = 1;
	private float timer;


	void Start () {
		// TODO - Use the light_Spawn_Data here to initialize the light
		transform.position = (Vector3)light_Spawn_Data.pos + (Vector3.forward * transform.position.z);
		timer = light_Spawn_Data.duration;
		
		if (light_Spawn_Data.spot_angle > 0) {
			attached_light.spotAngle = light_Spawn_Data.spot_angle;
		}
		initial_intensity = attached_light.intensity;
	}
	
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0) {
			Destroy(gameObject);
		}
		attached_light.intensity = initial_intensity * intensity_mult_at_time(timer, light_Spawn_Data.duration);
	}

	private float intensity_mult_at_time(float time, float duration) {
		if (time > fade_duration) {
			return 1;
		} else {
			return time / Mathf.Min(fade_duration, duration);
		}
	}

}
