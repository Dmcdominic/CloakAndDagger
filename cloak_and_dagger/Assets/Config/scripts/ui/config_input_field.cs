using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class config_input_field : MonoBehaviour {

	public string_event_object tooltip_update;
	public config_option_event_object after_value_changed;
	public config_option_event_object update_field_to;
	public Text title;

	[HideInInspector]
	public List<bool_input> toggle_dependencies = new List<bool_input>();
	[HideInInspector]
	public string description;

	[HideInInspector]
	public bool values_prepopulated = false;
	[HideInInspector]
	public int encoded_enum, config_cat;


	private void Start() {
		update_dependency_styling();
		update_visibility();
		update_field_to.e.AddListener(on_update_field_to);
	}

	// This will check all toggle_depedencies and hide or show if necessary
	public void update_visibility() {
		foreach (bool_input bool_Input in toggle_dependencies) {
			if (bool_Input.collapsed) {
				this.gameObject.SetActive(false);
				return;
			}
		}
		this.gameObject.SetActive(true);
	}
	
	// This is how we can adjust the styling of each dependent child option
	public void update_dependency_styling() {
		//HLG.padding.left += 20 * toggle_dependencies.Count;
	}

	// Trigger the tooltip_update event with this item's description
	public void update_tooltip() {
		tooltip_update.Invoke(description);
	}

	protected void after_val_changed(object value) {
		if (values_prepopulated) {
			after_value_changed.Invoke(encoded_enum, value, config_cat);
		}
	}

	protected void on_update_field_to(int inc_encoded_enum, object inc_value, int inc_config_cat) {
		if (values_prepopulated && (inc_encoded_enum == encoded_enum) && (inc_config_cat == config_cat)) {
			update_this_field_to(inc_value);
		}
	}

	public abstract void update_this_field_to(object new_val_obj);

	public abstract void set_up_listeners();

}
