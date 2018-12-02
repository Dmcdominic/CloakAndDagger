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
	Vec2Var _target_dest;

	[SerializeField]
	GameObject torch_prefab;

	[SerializeField]
	int_float_event trigger;

	[SerializeField]
	gameplay_config gameplay_Config;

    [SerializeField]
    Sound_manager Sfx;

	float max_distance;


	public override void Start() {
		base.Start();
		if (is_local) {
			trigger.e.AddListener(local_place);
		}
		max_distance = gameplay_Config.float_options[gameplay_float_option.torch_placement_range];
	}

	private void local_place(int id, float cooldown) {
		if (id != gameObject_id.val) return;

		Vector3 displacement = _target_dest.val - _origin.val;
		if (displacement.magnitude > max_distance) {
			displacement = displacement.normalized * max_distance;
		}

		GameObject rbcast_torch = Instantiate(torch_prefab);
		Vector2 position = get_placement_position(_origin, displacement, rbcast_torch);
		Destroy(rbcast_torch);

		torch_state torch_State = new torch_state(position);
		send_state(torch_State);
		place_func(torch_State);
	}

	public override void rectify(float t, torch_state state) {
		place_func(state);
	}

	private void place_func(torch_state torch_State) {
		GameObject new_torch = Instantiate(torch_prefab);
		new_torch.transform.position = torch_State.position;
		Sfx.sfx_trigger.Invoke("Place_torch");
	}

	private Vector2 get_placement_position(Vector2 origin, Vector2 delta, GameObject torch) {
		RaycastHit2D[] hits = new RaycastHit2D[1];

		Rigidbody2D rb = torch.GetComponent<Rigidbody2D>();
		torch.transform.position = origin + delta;
		while (rb.Cast(Vector3.zero, hits, 0) > 0) {
			torch.transform.position -= (Vector3)delta.normalized * .1f;
		}
		
		return rb.transform.position;
	}

}
