using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(network_id))]
public class Conditional_Rendering : MonoBehaviour {

    [SerializeField]
    int_var local_id;

	[SerializeField]
	Material local_mat;

	[SerializeField]
	Material non_local_mat;

	//[SerializeField]
	//float_event_object reveal_trigger;

	[SerializeField]
	bool_var spectator_reveal;

	private SpriteRenderer[] srs;

	// Use this for initialization
	void Start () {
		srs = GetComponentsInChildren<SpriteRenderer>();
		update_material();
	}

	private void Update() {
		update_material();
	}

	private void update_material() {
		if (srs == null) {
			srs = GetComponentsInChildren<SpriteRenderer>();
		}
		if ((GetComponent<network_id>().val == local_id.val) || spectator_reveal.val) {
			foreach (SpriteRenderer sr in srs) {
				sr.material = local_mat;
			}
		} else {
			foreach (SpriteRenderer sr in srs) {
				sr.material = non_local_mat;
			}
		}
	}

}
