using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dagger_collision : MonoBehaviour {

	private void OnCollisionEnter2D(Collision2D collision) {
		GameObject collidingObject = collision.collider.gameObject;
		if (collidingObject.CompareTag("Dagger")) {
			// We will want a death script/event, but for now you are just destroyed
			Destroy(this.gameObject);
		}
	}

}
