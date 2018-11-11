using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class bool_input : config_input_field {

	[HideInInspector]
	private bool _value;
	public bool value {
		get { return _value; }
		set { _value = value; on_value_changed.Invoke(_value); after_val_changed(_value); }
	}
	
	public Toggle toggle;

	[HideInInspector]
	public List<config_input_field> dependents = new List<config_input_field>();

	public bool collapsed { get { return !toggle.isOn /* || !dropdown_arrow.isOn */; } }
	public bool has_depedendents { get { return dependents.Count > 0; } }

	public class bool_event : UnityEvent<bool> { };
	public bool_event on_value_changed = new bool_event();


	public override void set_up_listeners() {
		// UI input listener
		toggle.onValueChanged.AddListener(on_toggle_value_change());

		// on_value_change listener
		on_value_changed.AddListener(get_update_dependents_action());

		// Start the coroutine which will listen for value changes to send
		StartCoroutine(waiting_to_send_val());
	}

	public override void update_this_field_to(object new_val_obj) {
		bool new_val = (bool)new_val_obj;
		value = new_val;
		toggle.isOn = new_val;
	}

	private UnityAction<bool> on_toggle_value_change() {
		return input => {
			value = input;
		};
	}

	// Tell all dependent fields to update their visibility if necessary.
	private void update_dependents() {
		foreach (config_input_field config_field in dependents) {
			config_field.update_visibility();
		}
	}
	private UnityAction<bool> get_update_dependents_action() {
		return x => { update_dependents(); };
	}

	private UnityAction<bool> update_toggle() {
		return input => {
			toggle.isOn = input;
		};
	}

}
