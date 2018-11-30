using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(network_id))]
public class bump_reveal : MonoBehaviour {

	[SerializeField]
	gameplay_config gameplay_Config;

    [SerializeField]
    int_float_event trigger;

	private network_id network_Id;


	private void Awake() {
		network_Id = GetComponent<network_id>();
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
            trigger.Invoke(network_Id.val, 1);
		}
	}
}
