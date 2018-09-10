using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

public class dash : NetworkBehaviour {

	[SerializeField]
	Vec2Var _origin;

	[SerializeField]
	Vec2Var _target_dest;

	[SerializeField]
	float distance = 2f;

	[SerializeField]
	event_object trigger;

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		if (isLocalPlayer) {
			trigger.e.AddListener(dash_func);
		}
	}

	public void dash_func() {
		Vector3 direction = (_target_dest.val - _origin.val).normalized;
		Vector3 displacement = direction * distance;
		rb.MovePosition(this.transform.position + displacement);
	}

}
