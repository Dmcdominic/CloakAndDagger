using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class update_disabled_icons : MonoBehaviour {

	[SerializeField]
	win_con_config win_Con_Config;

	[SerializeField]
	int_var local_id;

	[SerializeField]
	player_bool currently_trapped;

	[SerializeField]
	player_bool is_dead;

	public bool_var dagger_disabled;
	public bool_var fireball_disabled;
	public bool_var blink_disabled;
	public bool_var reflect_disabled;
	public bool_var torch_disabled;
	public bool_var trap_disabled;

	private network_id network_Id;
	private payload_carrier payload_Carrier;
	private On_Action_Button on_Action_Button;


	// Initialization
	private void Awake() {
		network_Id = GetComponent<network_id>();
		payload_Carrier = GetComponent<payload_carrier>();
		on_Action_Button = GetComponent<On_Action_Button>();
	}

	// While you are dead, nothing should be covered by disabled overlays
	private void OnDisable() {
		set_all_enabled();
	}

	// Regularly update the disabled status of each ability icon
	void Update() {
		if (network_Id.val != local_id.val) {
			return;
		}

		if (is_dead[local_id.val]) {
			set_all_enabled();
			return;
		}

		// Dagger
		dagger_disabled.val = !(on_Action_Button.check_carrier_pass(winCon_bool_option.carrier_dagger_disabled));

		// Fireball
		fireball_disabled.val = !(on_Action_Button.check_carrier_pass(winCon_bool_option.carrier_fireball_disabled));

		// Dash/blink
		//	Trapped players can't blink/dash
		blink_disabled.val = !(on_Action_Button.check_carrier_pass(winCon_bool_option.carrier_blink_disabled) && !currently_trapped[network_Id.val]);

		// Reflect
		reflect_disabled.val = !(on_Action_Button.check_carrier_pass(winCon_bool_option.carrier_reflect_disabled));

		// Torch
		torch_disabled.val = !(on_Action_Button.check_carrier_pass(winCon_bool_option.carrier_torch_disabled));

		// Trap
		trap_disabled.val = !(on_Action_Button.check_carrier_pass(winCon_bool_option.carrier_trap_disabled));
	}

	// Set all of the disabled vars to false
	private void set_all_enabled() {
		dagger_disabled.val = false;
		fireball_disabled.val = false;
		blink_disabled.val = false;
		reflect_disabled.val = false;
		torch_disabled.val = false;
		trap_disabled.val = false;
	}
}
