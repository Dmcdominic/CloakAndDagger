using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class payload : MonoBehaviour {

	[SerializeField]
	win_con_config win_Con_Config;
	
	private new Collider2D collider2D;
	private new Light light;

	private bool carrier_revealed;


	// Initialization
	private void Awake() {
		collider2D = GetComponent<Collider2D>();
		light = GetComponentInChildren<Light>();
		carrier_revealed = win_Con_Config.bool_options[winCon_bool_option.payload_carrier_revealed];
		light.range = win_Con_Config.float_options[winCon_float_option.payload_light_range];
	}

	public void pick_up() {
		collider2D.enabled = false;
		if (!carrier_revealed) {
			light.enabled = false;
		}
	}

	public void drop() {
		collider2D.enabled = true;
		light.enabled = true;
	}
}
