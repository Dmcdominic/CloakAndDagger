using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class payload_spawner : MonoBehaviour {

	public GameObject payload_prefab;

	public event_object spawn_payload_trigger;


	private void Awake() {
		if (spawn_payload_trigger) {
			spawn_payload_trigger.e.AddListener(spawn_payload);
		}
	}

	private void spawn_payload() {
		GameObject payload = Instantiate(payload_prefab);
		payload.transform.position = transform.position;
	}

}
