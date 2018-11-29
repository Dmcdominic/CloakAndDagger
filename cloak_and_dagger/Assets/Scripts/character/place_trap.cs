using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct trap_state {
	public serializable_vec2 position;
	public trap_state(Vector2 _position) {
		position = _position;
	}
}

[RequireComponent(typeof(network_id))]
public class place_trap : sync_behaviour<trap_state> {

	[SerializeField]
	Vec2Var _origin;

	[SerializeField]
	GameObject trap_prefab;

	[SerializeField]
	int_float_event trigger;

	[SerializeField]
	gameplay_config gameplay_Config;


	public override void Start() {
		base.Start();
		if (is_local) {
			trigger.e.AddListener(local_place);
		}
	}

	private void local_place(int id, float cooldown) {
		if (id != gameObject_id.val) return;

		trap_state trap_State = new trap_state(_origin.val);
		send_state(trap_State);
		place_func(trap_State);
	}

	public override void rectify(float t, trap_state state) {
		place_func(state);
	}

	private void place_func(trap_state trap_State) {
		placed_trap new_trap = Instantiate(trap_prefab).GetComponent<placed_trap>();
		new_trap.transform.position = trap_State.position;
		new_trap.placer_id = gameObject_id.val;

		// todo - sound effect for placing trap here
	}

}
