using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class range_cap : MonoBehaviour {

	public float absolute_max_dist_from_origin;

	private void Update() {
		if (transform.position.magnitude > absolute_max_dist_from_origin) {
			Destroy(gameObject);
		}
	}

}
