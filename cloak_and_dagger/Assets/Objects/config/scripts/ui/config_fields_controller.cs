using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum config_category { gameplay }

public abstract class config_fields_controller<T0, T1, T2> : MonoBehaviour	where T0 : struct, System.IConvertible
																			where T1 : struct, System.IConvertible
																			where T2 : struct, System.IConvertible {

	// This category must be set in each config controller subclass.
	public abstract config_category config_Category { get; }
	public config_object<T0, T1, T2> config;

	// Event to trigger population of the fields if the int arg is equal to this category, or to clear all fields otherwise.
	// The arg should be cast between int and config_categories enum as needed.
	// Event can be invoked with -1 to clear ALL config fields.
	public int_event_object update_fields_trigger;

	// UI field prefabs
	public bool_input bool_input_prefab;
	public float_input float_input_prefab;
	public int_input int_input_prefab;

	// Initialization
	protected void Awake() {
		if (update_fields_trigger) {
			update_fields_trigger.e.AddListener(update_fields);
		}
	}
	protected void Start () {
		// TEMPORARY - TODO: REMOVE
		create_all_fields();
	}

	// Wrapper to create all UI input fields for config customization
	public void create_all_fields() {
		create_fields(config.ui_parameters_ordered, new List<bool_input>());
	}

	// Helper function for instantiating all the fields and dependent fields of a particular ui_parameters dictionary.
	private void create_fields(OrderedDictionary ui_parameters, List<bool_input> dependencies) {

		// STILL NEED TO IMPLEMENT:
		// Map selection
			// Map specific options, dependent on current map
		// Win condition selection
			// Win condition specific options, dependent on current win condition

		foreach (object option in ui_parameters.Keys) {
			if (option is T0) {
				T0 T0_option = (T0)option;
				bool_input new_toggle = create_toggle(T0_option, dependencies);
				if (config.ui_dependents != null && config.ui_dependents.ContainsKey(T0_option)) {
					dependencies.Add(new_toggle);
					create_fields(config.ui_dependents[T0_option], dependencies);
					dependencies.Remove(new_toggle);
				}
			} else if (option is T1) {
				create_float_slider((T1)option, dependencies);
			} else if (option is T2) {
				create_int_slider((T2)option, dependencies);
			} else {
				Debug.LogError("Config option: " + option + " is not a valid type!");
			}
		}
	}

	// Methods for instantiating UI fields
	private bool_input create_toggle(T0 option, List<bool_input> dependencies) {
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

		new_input_object.title.text = option.ToString(); // Could instead add a Title property to each ui_(type)_info struct
		new_input_object.set_up_listeners();
		new_input_object.on_value_changed.AddListener(edit_bool(config.bool_options, option));
		foreach (bool_input parent in dependencies) {
			parent.dependents.Add(new_input_object);
			new_input_object.toggle_dependencies.Add(parent);
		}
		new_input_object.toggle.isOn = value;
		return new_input_object;
	}

	private void create_float_slider(T1 option, List<bool_input> dependencies) {
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

		new_input_object.title.text = option.ToString(); // Could instead add a Title property to each ui_(type)_info struct
		new_input_object.min = ui_info.min;
		new_input_object.max = ui_info.max;
		new_input_object.slider.minValue = ui_info.slider_min;
		new_input_object.slider.maxValue = ui_info.slider_max;
		new_input_object.set_up_listeners();
		new_input_object.on_value_changed.AddListener(edit_float(config.float_options, option));
		foreach (bool_input parent in dependencies) {
			parent.dependents.Add(new_input_object);
			new_input_object.toggle_dependencies.Add(parent);
		}
		new_input_object.slider.value = value;
	}

	private void create_int_slider(T2 option, List<bool_input> dependencies) {
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

		new_input_object.title.text = option.ToString(); // Could instead add a Title property to each ui_(type)_info struct
		new_input_object.min = ui_info.min;
		new_input_object.max = ui_info.max;
		new_input_object.slider.minValue = ui_info.slider_min;
		new_input_object.slider.maxValue = ui_info.slider_max;
		new_input_object.set_up_listeners();
		new_input_object.on_value_changed.AddListener(edit_int(config.int_options, option));
		foreach (bool_input parent in dependencies) {
			parent.dependents.Add(new_input_object);
			new_input_object.toggle_dependencies.Add(parent);
		}
		new_input_object.value = value;
	}


	// ========== Toggle & input field event listener higher-order-functions ==========

	// Returns a function that will update the dictionary value for you.
	protected static UnityAction<bool> edit_bool(Dictionary<T0, bool> dict, T0 option) {
		return val => { dict[option] = val; };
	}
	protected static UnityAction<float> edit_float(Dictionary<T1, float> dict, T1 option) {
		return val => { dict[option] = val; };
	}
	protected static UnityAction<int> edit_int(Dictionary<T2, int> dict, T2 option) {
		return val => { dict[option] = val; };
	}

	// Event listener for updating the fields
	public void update_fields(int category_index) {
		config_category category = (config_category)category_index;
		clear_fields();
		if (category == config_Category) {
			create_all_fields();
		}
	}

	// Remove all the fields
	public void clear_fields() {
		Debug.Log("Destroying ALL children of this fields controller");
		foreach (Transform child in transform) {
			Destroy(child.gameObject);
		}
	}

}
