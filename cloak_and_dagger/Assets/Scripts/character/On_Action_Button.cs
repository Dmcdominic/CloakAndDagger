using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;


[RequireComponent(typeof(network_id))]
public class On_Action_Button : MonoBehaviour {

	[SerializeField]
	input_config input_Config;

	[SerializeField]
	gameplay_config gameplay_Config;
	[SerializeField]
	win_con_config win_Con_Config;

	// Dagger
	[SerializeField]
	int_float_event to_trigger_dagger;
	[SerializeField]
	event_object to_trigger_dagger_pulse;
	[SerializeField]
	player_bool dagger_on_cooldown;

	// Fireball
	[SerializeField]
	int_float_event to_trigger_fireball;
	[SerializeField]
	event_object to_trigger_fireball_pulse;
	[SerializeField]
	player_bool fireball_on_cooldown;

	// Blink/dash
	[SerializeField]
	int_float_event to_trigger_dash;
	[SerializeField]
	event_object to_trigger_dash_pulse;
	[SerializeField]
	player_bool dash_on_cooldown;

	// Reflect
	[SerializeField]
	int_float_event to_trigger_reflect_cooldown;
	[SerializeField]
	int_float_event to_trigger_reflect_time;
	[SerializeField]
	event_object to_trigger_reflect_pulse;
	[SerializeField]
	player_bool reflect_on_cooldown;

	[SerializeField]
	bool_var ingame_state;

    [SerializeField]
    int_var local_id;

	private network_id network_Id;
	private payload_carrier payload_Carrier;


	private void Awake() {
		network_Id = GetComponent<network_id>();
		payload_Carrier = GetComponent<payload_carrier>();
	}

	// Update is called once per frame.
	// If the corresponding input button is pressed for a given action, then:
	//		- If that action is allowed, the corresponding event(s) will be triggered.
	//		- If that action is not allowed (e.g. on cooldown, or payload-carrier-disabled), the indicator pulse event will be triggered.
	// Thus, additional action conditions should be included in the inner-most if statement.
	void Update () {
		if (network_Id.val == local_id.val && ingame_state.val && input_Config) {
			// Dagger
			if (input_Config.dagger) {
				if (!dagger_on_cooldown[network_Id.val] && check_carrier_pass(winCon_bool_option.carrier_dagger_disabled)) {
					to_trigger_dagger.Invoke(network_Id.val, gameplay_Config.float_options[gameplay_float_option.dagger_cooldown]);
				} else {
					to_trigger_dagger_pulse.Invoke();
				}
			}

			// Fireball
			if (input_Config.fireball) {
				if (!fireball_on_cooldown[network_Id.val] && check_carrier_pass(winCon_bool_option.carrier_fireball_disabled)) {
					to_trigger_fireball.Invoke(network_Id.val, gameplay_Config.float_options[gameplay_float_option.fireball_cooldown]);
				} else {
					to_trigger_fireball_pulse.Invoke();
				}
			}

			// Dash/blink
			if (input_Config.dash) {
				if (!dash_on_cooldown[network_Id.val] && check_carrier_pass(winCon_bool_option.carrier_blink_disabled)) {
					to_trigger_dash.Invoke(network_Id.val, gameplay_Config.float_options[gameplay_float_option.blink_cooldown]);
				} else {
					to_trigger_dash_pulse.Invoke();
				}
			}

			// Reflect
			if (input_Config.reflect) {
				if (!reflect_on_cooldown[network_Id.val] && check_carrier_pass(winCon_bool_option.carrier_reflect_disabled)) {
					to_trigger_reflect_cooldown.Invoke(network_Id.val, gameplay_Config.float_options[gameplay_float_option.reflect_cooldown]);
					to_trigger_reflect_time.Invoke(network_Id.val, gameplay_Config.float_options[gameplay_float_option.reflect_time]);
				} else {
					to_trigger_reflect_pulse.Invoke();
				}
			}
		}
	}

	// Returns false iff this player is carrying the payload AND payload carriers are prevented from the given action
	private bool check_carrier_pass(winCon_bool_option winCon_Bool_Option) {
		return !(payload_Carrier.carrying && win_Con_Config.bool_options[winCon_Bool_Option]);
	}
}
