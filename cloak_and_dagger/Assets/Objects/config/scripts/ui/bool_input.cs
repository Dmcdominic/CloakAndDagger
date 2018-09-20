using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class bool_input : config_input_field {

	public Text title;
	public Toggle toggle;

	public List<config_input_field> dependents = new List<config_input_field>();

	public bool collapsed { get { return !toggle.isOn /* || !dropdown_arrow.isOn */; } }
	public bool has_depedendents { get { return dependents.Count > 0; } }

	private void Awake() {
		toggle.onValueChanged.AddListener(get_update_dependents_action());
	}

	private void update_dependents() {
		foreach (config_input_field config_field in dependents) {
			config_field.update_visibility();
		}
	}

	private UnityAction<bool> get_update_dependents_action() {
		return x => { update_dependents(); };
	}

}
