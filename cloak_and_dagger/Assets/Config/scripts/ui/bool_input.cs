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
		set { _value = value; on_value_changed.Invoke(_value); after_value_changed.Invoke(); }
	}
	
	public Toggle toggle;

	[HideInInspector]
	public List<config_input_field> dependents = new List<config_input_field>();

	public bool collapsed { get { return !toggle.isOn /* || !dropdown_arrow.isOn */; } }
	public bool has_depedendents { get { return dependents.Count > 0; } }

	public class bool_event : UnityEvent<bool> { };
	public bool_event on_value_changed = new bool_event();


	public override void set_up_listeners() {
		// UI input listeners
		toggle.onValueChanged.AddListener(on_toggle_value_change());

		// on_value_change listeners
		on_value_changed.AddListener(get_update_dependents_action());
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
