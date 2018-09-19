using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class config_fields_controller<T0, T1, T2> : MonoBehaviour	where T0 : struct, System.IConvertible
																	where T1 : struct, System.IConvertible
																	where T2 : struct, System.IConvertible {

	public config_object<T0, T1, T2> config;

	// Clear fields event
	public event_object clear_fields_trigger;

	// UI field prefabs
	public bool_input bool_input_prefab;
	public float_input float_input_prefab;
	public int_input int_input_prefab;

	// Use this for initialization
	protected void Start () {
		if (clear_fields_trigger) {
			clear_fields_trigger.e.AddListener(clear_fields);
		}
		create_fields();
	}
	
	// Create all UI input fields for config customization
	public void create_fields() {
		// Create bool toggles
		foreach (T0 option in System.Enum.GetValues(typeof(T0))) {
			ui_bool_info<T0> ui_info;
			config.ui_bool_parameters.TryGetValue(option, out ui_info);
			bool value;
			if (!config.bool_options.TryGetValue(option, out value)) {
				// Set default here?
			}

			bool_input new_bool_input = Instantiate(bool_input_prefab.gameObject).GetComponent<bool_input>();
			new_bool_input.transform.SetParent(this.transform);

			new_bool_input.title.text = option.ToString(); // Could instead add a Title property to each ui_(type)_info struct
			new_bool_input.toggle.isOn = value;
			new_bool_input.toggle.onValueChanged.AddListener(edit_bool(config.bool_options, option));
		}

		// Create float sliders
		foreach (T1 option in System.Enum.GetValues(typeof(T1))) {
			ui_float_info<T0> ui_info;
			config.ui_float_parameters.TryGetValue(option, out ui_info);
			float value;
			if (!config.float_options.TryGetValue(option, out value)) {
				// Set default here?
			}

			float_input new_bool_input = Instantiate(float_input_prefab.gameObject).GetComponent<float_input>();
			new_bool_input.transform.SetParent(this.transform);

			new_bool_input.title.text = option.ToString(); // Could instead add a Title property to each ui_(type)_info struct
			new_bool_input.slider.minValue = ui_info.min;
			new_bool_input.slider.maxValue = ui_info.max;
			new_bool_input.slider.value = value;
			new_bool_input.slider.onValueChanged.AddListener(edit_float(config.float_options, option));
		}

		// Create int sliders
		foreach (T2 option in System.Enum.GetValues(typeof(T2))) {
			ui_int_info<T0> ui_info;
			config.ui_int_parameters.TryGetValue(option, out ui_info);
			int value;
			if (!config.int_options.TryGetValue(option, out value)) {
				// Set default here?
			}

			int_input new_bool_input = Instantiate(int_input_prefab.gameObject).GetComponent<int_input>();
			new_bool_input.transform.SetParent(this.transform);

			new_bool_input.title.text = option.ToString(); // Could instead add a Title property to each ui_(type)_info struct
			new_bool_input.slider.minValue = ui_info.min;
			new_bool_input.slider.maxValue = ui_info.max;
			new_bool_input.slider.value = value;
			new_bool_input.slider.onValueChanged.AddListener(edit_int(config.int_options, option));
		}
	}

	// Remove all the 
	public void clear_fields() {
		foreach (Transform child in transform) {
			Destroy(child.gameObject);
		}
	}

	// Returns a function that will update the dictionary value for you.
	// This is for creating toggle & input field event listeners.
	public static UnityAction<bool> edit_bool(Dictionary<T0, bool> dict, T0 option) {
		return val => { dict[option] = val; };
	}
	public static UnityAction<float> edit_float(Dictionary<T1, float> dict, T1 option) {
		return val => { dict[option] = val; };
	}
	public static UnityAction<float> edit_int(Dictionary<T2, int> dict, T2 option) {
		return val => { dict[option] = (int)val; };
	}

}
