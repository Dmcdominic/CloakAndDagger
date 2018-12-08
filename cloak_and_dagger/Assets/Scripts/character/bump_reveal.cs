using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(network_id))]
public class bump_reveal : MonoBehaviour {

	[SerializeField]
	gameplay_config gameplay_Config;

    [SerializeField]
    int_float_event trigger;

	[SerializeField]
	int_var local_id;

	[SerializeField]
	Sound_manager Sfx;

	private network_id network_Id;


	private void Awake() {
		network_Id = GetComponent<network_id>();
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
			if (collision.gameObject.GetComponent<network_id>().val == local_id.val) {
				trigger.Invoke(network_Id.val, 1);
				Sfx.sfx_trigger.Invoke("Bump");
			}
		}
	}
}
