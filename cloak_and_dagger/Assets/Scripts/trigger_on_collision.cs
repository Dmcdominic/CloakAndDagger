using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_on_collision : MonoBehaviour {

	[SerializeField]
	collision_event_object to_trigger_on_collision;

	private void OnCollisionEnter2D(Collision2D collision) {
		to_trigger_on_collision.Invoke(this.gameObject, collision);
	}

}
