using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reflect : sync_behaviour<unit> {

	[SerializeField]
	int_float_event trigger;

	[SerializeField]
	gameplay_config gameplay_Config;

	[SerializeField]
	int_float_event to_trigger_reflect_cooldown;
	[SerializeField]
	int_float_event to_trigger_reflect_time;

    [SerializeField]
    Sound_manager Sfx;

	public override void Start() {
		base.Start();
		if (is_local) {
			trigger.e.AddListener(local_reflect);
		}
	}

	private void local_reflect(int id, float cooldown) {
		if (id != gameObject_id.val) return;

		send_state(new unit());
		reflect_func();
	}

	public override void rectify(float t, unit state) {
		reflect_func();
	}

	private void reflect_func() {
		to_trigger_reflect_cooldown.Invoke(gameObject_id.val, gameplay_Config.float_options[gameplay_float_option.reflect_cooldown]);
		to_trigger_reflect_time.Invoke(gameObject_id.val, gameplay_Config.float_options[gameplay_float_option.reflect_time]);
		Sfx.sfx_trigger.Invoke("Shield");
	}

}
