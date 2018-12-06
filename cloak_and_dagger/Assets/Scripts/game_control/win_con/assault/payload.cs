using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public class payload : MonoBehaviour {

	[SerializeField]
	gameobject_var global_payload;

	[SerializeField]
	win_con_config win_Con_Config;

	[SerializeField]
	Sound_manager Sfx;

	[HideInInspector]
	public bool carried;
	[HideInInspector]
	public int carrier_id;

	[HideInInspector]
	public float last_pickup_time;

	public SpriteRenderer spriteRenderer { get; set; }

	private new Collider2D collider2D;
	private new Light light;

	private bool carrier_revealed;


	// Initialization
	private void Awake() {
		if (global_payload.val == null) {
			global_payload.val = gameObject;
		}
		last_pickup_time = float.MinValue;
		
		collider2D = GetComponent<Collider2D>();
		light = GetComponentInChildren<Light>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		
		carrier_revealed = win_Con_Config.bool_options[winCon_bool_option.payload_carrier_revealed];
		light.range = win_Con_Config.float_options[winCon_float_option.payload_light_range];
	}

	public void spawn() {
		carrier_id = -1;
		carried = false;
		collider2D.enabled = true;
		light.enabled = true;
		transform.rotation = Quaternion.identity;
		gameObject.SetActive(true);
	}

	public void pick_up(int carrier_Id, float t) {
		carrier_id = carrier_Id;
		carried = true;
		last_pickup_time = t;

		collider2D.enabled = false;
		if (!carrier_revealed) {
			light.enabled = false;
		}
	}

	public void drop() {
		carried = false;
		collider2D.enabled = true;
		light.enabled = true;
	}

	public void deliver() {
		carried = false;
		gameObject.SetActive(false);
		Sfx.sfx_trigger.Invoke("Payload_delivered");
	}
}
