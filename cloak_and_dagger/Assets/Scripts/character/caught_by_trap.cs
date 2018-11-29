using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class caught_by_trap : sync_behaviour<int> {

	[SerializeField]
	gameplay_config gameplay_Config;
	
	[SerializeField]
	int_float_event to_trigger_trapped_time;

	[SerializeField]
	int_event_object to_trigger_trap_catch;

    [SerializeField]
    Sound_manager Sfx;

	private void OnTriggerEnter2D(Collider2D collision) {
		if (is_local && collision.gameObject.CompareTag("Trap")) {
			placed_trap trap = collision.gameObject.GetComponent<placed_trap>();
			if (trap.catch_player(gameObject_id.val)) {
				local_trap(trap.get_network_id());
				transform.position = trap.transform.position;
			}
		}
	}

	private void local_trap(int trap_id) {
		send_state(trap_id);
		caught_func(trap_id);
	}

	public override void rectify(float t, int state) {
        Sfx.sfx_trigger.Invoke("Trigger_trap");
		caught_func(state);
	}

	private void caught_func(int trap_id) {
		to_trigger_trapped_time.Invoke(gameObject_id.val, gameplay_Config.float_options[gameplay_float_option.trap_hold_duration]);
		to_trigger_trap_catch.Invoke(trap_id);
	}

}
