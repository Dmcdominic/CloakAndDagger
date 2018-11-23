using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct torch_state {
	public serializable_vec2 position;
	public torch_state(Vector2 _position) {
		position = _position;
	}
}

[RequireComponent(typeof(network_id))]
public class place_torch : sync_behaviour<torch_state> {

	[SerializeField]
	Vec2Var _origin;

	[SerializeField]
	GameObject torch_prefab;

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

		torch_state torch_State = new torch_state(_origin.val);
		send_state(torch_State);
		place_func(torch_State);
	}

	public override void rectify(float t, torch_state state) {
		place_func(state);
	}

	private void place_func(torch_state torch_State) {
		GameObject new_torch = Instantiate(torch_prefab);
		new_torch.transform.position = torch_State.position;
	}

}
