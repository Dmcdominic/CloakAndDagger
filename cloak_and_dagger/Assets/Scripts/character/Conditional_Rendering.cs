using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Conditional_Rendering : NetworkBehaviour {

	[SerializeField]
	Material local_mat;

	[SerializeField]
	Material non_local_mat;

	//[SerializeField]
	//float_event_object reveal_trigger;

	[SerializeField]
	bool_var spectator_reveal;

	private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer>();
		update_material();
	}

	private void Update() {
		update_material();
	}

	private void update_material() {
		if (!sr) {
			sr = GetComponent<SpriteRenderer>();
		}
		if (isLocalPlayer || spectator_reveal.val) {
			sr.material = local_mat;
		} else {
			sr.material = non_local_mat;
		}
	}

}
