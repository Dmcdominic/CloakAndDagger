using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SerializeField]
public struct unit { }

public class destroy_dagger : sync_behaviour<unit> {

	public int_event_object trigger_destroy;
	private dagger_data_carrier dagger_Data_Carrier;


	private void Awake() {
		dagger_Data_Carrier = GetComponent<dagger_data_carrier>();
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
        print($"dagger destoryed");
		Destroy(gameObject);
	}

}
