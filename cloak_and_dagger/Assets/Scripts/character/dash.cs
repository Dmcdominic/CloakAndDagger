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

	[SerializeField]
	float distance = 2f;

	[SerializeField]
	float_event_object trigger;

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		if (isLocalPlayer) {
			trigger.e.AddListener(dash_func);
		}
	}

	public void dash_func(float cooldown) {
		Vector3 direction = (_target_dest.val - _origin.val).normalized;
		Vector3 displacement = direction * distance;
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
