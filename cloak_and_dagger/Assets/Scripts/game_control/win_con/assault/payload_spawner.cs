using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class payload_spawner : MonoBehaviour {

	public GameObject payload_prefab;

	public gameobject_var global_payload;
	private payload Payload;

	public event_object spawn_payload_trigger;


	private void Awake() {
		instantiate_payload();
		if (spawn_payload_trigger) {
			spawn_payload_trigger.e.AddListener(spawn_payload);
		}
	}

	private void instantiate_payload() {
		if (global_payload.val == null) {
			global_payload.val = Instantiate(payload_prefab);
			global_payload.val.SetActive(false);
			Payload = global_payload.val.GetComponent<payload>();
		}
	}

	private void spawn_payload() {
		global_payload.val.transform.position = transform.position;
		global_payload.val.SetActive(true);
		Payload.spawn();
	}

}
