using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
		create_fields(config.ui_parameters_ordered, 0, null);
	}
	
	// Create all UI input fields for config customization
	public void create_fields(OrderedDictionary ui_parameters, int dependency_depth, Toggle depends_on, bool enabled = true) {

		// STILL NEED TO IMPLEMENT:
		// Map specific options and win-condition specific options

		foreach (object option in ui_parameters.Keys) {
			if (option is T0) {
				T0 T0_option = (T0)option;
				Toggle new_toggle = create_toggle(T0_option, dependency_depth, depends_on, enabled);
				if (config.ui_dependents != null && config.ui_dependents.ContainsKey(T0_option)) {
					create_fields(config.ui_dependents[T0_option], dependency_depth + 1, new_toggle, new_toggle.isOn);
				}
			} else if (option is T1) {
				create_float_slider((T1)option, dependency_depth, depends_on, enabled);
			} else if (option is T2) {
				create_int_slider((T2)option, dependency_depth, depends_on, enabled);
			} else {
				Debug.LogError("Config option: " + option + " is not a valid type!");
			}
		}

	}

	// Methods for instantiating UI fields
	private Toggle create_toggle(T0 option, int dependency_depth, Toggle depends_on, bool enabled = true) {
		ui_bool_info<T0> ui_info;
		if (config.ui_parameters_ordered.Contains(option)) {
			ui_info = (ui_bool_info<T0>)config.ui_parameters_ordered[option];
		} else {
			ui_info = config.default_bool_info;
		}
		bool value;
		if (!config.bool_options.TryGetValue(option, out value)) {
			// Set default here?
		}

		bool_input new_input_object = Instantiate(bool_input_prefab.gameObject).GetComponent<bool_input>();
		new_input_object.transform.SetParent(this.transform);
		// Adjust some properties here based on the dependency_depth

		new_input_object.title.text = option.ToString(); // Could instead add a Title property to each ui_(type)_info struct
		new_input_object.toggle.isOn = value;
		new_input_object.toggle.onValueChanged.AddListener(edit_bool(config.bool_options, option));
		if (depends_on) {
			depends_on.onValueChanged.AddListener(toggle_off(new_input_object.toggle));
			depends_on.onValueChanged.AddListener(gameObject_setActive(new_input_object.gameObject));
		}
		new_input_object.gameObject.SetActive(enabled);
		return new_input_object.toggle;
	}

	private void create_float_slider(T1 option, int dependency_depth, Toggle depends_on, bool enabled = true) {
		ui_float_info<T0> ui_info;
		if (config.ui_parameters_ordered.Contains(option)) {
			ui_info = (ui_float_info<T0>)config.ui_parameters_ordered[option];
		} else {
			ui_info = config.default_float_info;
		}
		float value;
		if (!config.float_options.TryGetValue(option, out value)) {
			// Set default here?
		}

		float_input new_input_object = Instantiate(float_input_prefab.gameObject).GetComponent<float_input>();
		new_input_object.transform.SetParent(this.transform);
		// Adjust some properties here based on the dependency_depth

		new_input_object.title.text = option.ToString(); // Could instead add a Title property to each ui_(type)_info struct
		new_input_object.slider.minValue = ui_info.min;
		new_input_object.slider.maxValue = ui_info.max;
		new_input_object.slider.value = value;
		new_input_object.slider.onValueChanged.AddListener(edit_float(config.float_options, option));
		if (depends_on) {
			depends_on.onValueChanged.AddListener(gameObject_setActive(new_input_object.gameObject));
		}
		new_input_object.gameObject.SetActive(enabled);
	}

	private void create_int_slider(T2 option, int dependency_depth, Toggle depends_on, bool enabled = true) {
		ui_int_info<T0> ui_info;
		if (config.ui_parameters_ordered.Contains(option)) {
			ui_info = (ui_int_info<T0>)config.ui_parameters_ordered[option];
		} else {
			ui_info = config.default_int_info;
		}
		int value;
		if (!config.int_options.TryGetValue(option, out value)) {
			// Set default here?
		}

		int_input new_input_object = Instantiate(int_input_prefab.gameObject).GetComponent<int_input>();
		new_input_object.transform.SetParent(this.transform);
		// Adjust some properties here based on the dependency_depth

		new_input_object.title.text = option.ToString(); // Could instead add a Title property to each ui_(type)_info struct
		new_input_object.slider.minValue = ui_info.min;
		new_input_object.slider.maxValue = ui_info.max;
		new_input_object.slider.value = value;
		new_input_object.slider.onValueChanged.AddListener(edit_int(config.int_options, option));
		if (depends_on) {
			depends_on.onValueChanged.AddListener(gameObject_setActive(new_input_object.gameObject));
		}
		new_input_object.gameObject.SetActive(enabled);
	}


	// Remove all the fields
	public void clear_fields() {
		Debug.Log("Destroying ALL children of this fields controller");
		foreach (Transform child in transform) {
			Destroy(child.gameObject);
		}
	}


	// ========== Toggle & input field event listener higher-order-functions ==========

	// Returns a function that will update the dictionary value for you.
	public static UnityAction<bool> edit_bool(Dictionary<T0, bool> dict, T0 option) {
		return val => { dict[option] = val; };
	}
	public static UnityAction<float> edit_float(Dictionary<T1, float> dict, T1 option) {
		return val => { dict[option] = val; };
	}
	public static UnityAction<float> edit_int(Dictionary<T2, int> dict, T2 option) {
		return val => { dict[option] = (int)val; };
	}

	// Returns a function that will switch a toggle off if the input is false.
	public static UnityAction<bool> toggle_off(Toggle to_toggle) {
		return val => { if (!val) { to_toggle.isOn = false; } };
	}

	// Returns a function that will enable/disable a gameObject depending on the input.
	public static UnityAction<bool> gameObject_setActive(GameObject gameObject) {
		return val => { gameObject.SetActive(val); };
	}

}
