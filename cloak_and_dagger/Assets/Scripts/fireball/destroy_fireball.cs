using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(network_id))]
public class destroy_fireball : sync_behaviour<unit> {

	public int_event_object trigger_destroy;


	private void Awake() {
		if (trigger_destroy) {
			trigger_destroy.e.AddListener(on_trigger_destroy);
		}
	}

	private void on_trigger_destroy(int daggerID) {
		network_id network_Id = GetComponent<network_id>();
		if (network_Id.val != daggerID) {
			return;
		}
        
		send_state(new unit());
		Destroy(gameObject);
	}

	public override void rectify(float t, unit state) {
		Destroy(gameObject);
	}

	private void OnDestroy() {
		// Todo - some interesting particle and lighting effects?
	}

}
