using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class payload : MonoBehaviour {

	[SerializeField]
	private int_var local_id;

	[SerializeField]
	win_con_config win_Con_Config;
	
	private new Collider2D collider2D;


	// Initialization
	private void Awake() {
		collider2D = GetComponent<Collider2D>();
	}

	public void pick_up() {
		collider2D.enabled = false;
	}

	public void drop() {
		collider2D.enabled = true;
	}
}
