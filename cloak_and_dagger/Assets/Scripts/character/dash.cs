using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

public class dash : NetworkBehaviour {

	[SerializeField]
	Vec2Var _origin;

	[SerializeField]
	Vec2Var _target_dest;

	public gameplay_config gameplay_Config;
	private float max_distance;

	[SerializeField]
	float_event_object trigger;

	[SerializeField]
	light_spawn_event_object light_spawn_trigger;

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		if (isLocalPlayer) {
			trigger.e.AddListener(dash_func);
		}

		max_distance = gameplay_Config.float_options[gameplay_float_option.dash_distance];
	}

	public void dash_func(float cooldown) {
		// Spawn light at origin
		light_spawn_data light_data = new light_spawn_data(_origin.val, 2f);
		light_spawn_trigger.Invoke(light_data);

		// Blink to the target destination
		Vector3 displacement = _target_dest.val - _origin.val;
		if (displacement.magnitude > max_distance) {
			displacement = displacement.normalized * max_distance;
		}
		Cmd_update_pos_on_server(this.transform.position + displacement);
	}

	// Server is told that the player should be moved to the new position
	[Command]
	private void Cmd_update_pos_on_server(Vector2 new_pos) {
		Rpc_update_pos_for_all_clients(new_pos);
	}

	// Server tells all clients to move this player's rb to the new position
	[ClientRpc]
	private void Rpc_update_pos_for_all_clients(Vector2 new_pos) {
		rb.MovePosition(new_pos);
	}

}
