using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light_spawner : MonoBehaviour {

	public spawned_light light_prefab;
	public light_spawn_event_object light_Spawn_Event_Object;

	private void Awake() {
		if (light_Spawn_Event_Object) {
			light_Spawn_Event_Object.e.AddListener(spawn_light);
		}
	}

	private void spawn_light(light_spawn_data light_Spawn_Data) {
		spawned_light new_light = Instantiate(light_prefab);
		new_light.light_Spawn_Data = light_Spawn_Data;
	}

}

public struct light_spawn_data {
	public Vector2 pos;
	public float duration;
	public float spot_angle;
	public light_spawn_data(Vector2 _pos, float _duration) { // Use default spot_angle
		this.pos = _pos;
		this.duration = _duration;
		this.spot_angle = 0;
	}
	public light_spawn_data(Vector2 _pos, float _duration, float _spot_angle) { // Override spot_angle
		this.pos = _pos;
		this.duration = _duration;
		this.spot_angle = _spot_angle;
	}
}
