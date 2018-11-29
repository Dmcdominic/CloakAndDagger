using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class caught_by_trap : sync_behaviour<unit> {

	[SerializeField]
	gameplay_config gameplay_Config;
	
	[SerializeField]
	int_float_event to_trigger_trapped_time;


	private void OnTriggerEnter2D(Collider2D collision) {
		if (is_local && collision.gameObject.CompareTag("Trap")) {
			placed_trap trap = collision.gameObject.GetComponent<placed_trap>();
			if (trap.catch_player(gameObject_id.val)) {
				local_trap();
				transform.position = trap.transform.position;
			}
		}
	}

	private void local_trap() {
		send_state(new unit());
		caught_func();
	}

	public override void rectify(float t, unit state) {
		caught_func();
	}

	private void caught_func() {
		to_trigger_trapped_time.Invoke(gameObject_id.val, gameplay_Config.float_options[gameplay_float_option.trap_hold_duration]);
	}

}
