using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy_dagger : sync_behaviour<dagger_data> {

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
		send_state(dagger_Data_Carrier.dagger_Data);
		Destroy(gameObject);
	}

	public override void rectify(float t, dagger_data state) {
        print("dagger destoryed");
		Destroy(gameObject);
	}

}
