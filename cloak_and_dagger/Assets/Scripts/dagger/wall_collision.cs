using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall_collision : MonoBehaviour {

	[SerializeField]
	bool destroyOnWallCollision = false;


	private void OnCollisionEnter2D(Collision2D collision) {
		GameObject collidingObject = collision.collider.gameObject;
		if (collidingObject.CompareTag("Wall")) {
			if (destroyOnWallCollision) {
				Destroy(this.gameObject);
			}
		}
	}
}
