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
	public int encoded_enum, config_cat;

	private object val_to_send;


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
		val_to_send = value;
	}

	protected IEnumerator waiting_to_send_val() {
		object prev_val;
		while (true) {
			prev_val = val_to_send;
			yield return new WaitUntil(() => val_to_send != prev_val);
			while (val_to_send != prev_val) {
				prev_val = val_to_send;
				yield return new WaitForSeconds(0.1f);
			}
			after_value_changed.Invoke(encoded_enum, val_to_send, config_cat);
		}
	}

	protected void on_update_field_to(int inc_encoded_enum, object inc_value, int inc_config_cat) {
		if ((inc_encoded_enum == encoded_enum) && (inc_config_cat == config_cat)) {
			update_this_field_to(inc_value);
		}
	}

	public abstract void update_this_field_to(object new_val_obj);

	public abstract void set_up_listeners();

}
