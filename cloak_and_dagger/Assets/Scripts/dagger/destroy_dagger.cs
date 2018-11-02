using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct unit { }

public class destroy_dagger : sync_behaviour<unit> {

	public int_event_object trigger_destroy;


	private void Awake() {
		if (trigger_destroy) {
			trigger_destroy.e.AddListener(on_trigger_destroy);
		}
	}

	private void on_trigger_destroy(int dagger_instanceID) {
		if (gameObject.GetInstanceID() != dagger_instanceID) {
			return;
		}
        
		send_state(new unit());
		Destroy(gameObject);
	}

	public override void rectify(float t, unit state) {
		Destroy(gameObject);
	}

}
