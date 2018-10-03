﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class config_fields_controller<T0, T1, T2> : MonoBehaviour where T0 : struct, System.IConvertible
																			where T1 : struct, System.IConvertible
																			where T2 : struct, System.IConvertible {

	// This category must be set in each config controller subclass.
	public abstract config_category config_Category { get; }
	public config_object<T0, T1, T2> config;

	// Event to trigger population of the fields if the int arg is equal to this category, or to clear all fields otherwise.
	// The arg should be cast between int and config_categories enum as needed.
	// Event can be invoked with -1 to open the save/load presets menu.
	// Event can be invoked with -2 to clear ALL config fields.
	public int_event_object update_fields_trigger;

	// UI field prefabs
	public bool_input bool_input_prefab;
	public float_input float_input_prefab;
	public int_input int_input_prefab;

	// Dicts to store the parameters for each option within the editor.
	// Should be set within the constructor of the particular fields_controller.
	// See "UI info structs" below for details.
	public OrderedDictionary ui_parameters_ordered = new OrderedDictionary();
	public Dictionary<T0, OrderedDictionary> ui_dependents = new Dictionary<T0, OrderedDictionary>();

	// Default UI info structs
	public ui_bool_info<T0> default_bool_info = new ui_bool_info<T0>("");
	public ui_float_info<T0> default_float_info = new ui_float_info<T0>(0.01f, 20.0f, 0f, 100f, "");
	public ui_int_info<T0> default_int_info = new ui_int_info<T0>(1, 20, 0, 100, "");

	protected bool interactable;
	protected bool currently_open = false;


	// Initialization
	protected void Awake() {
		interactable = true; // TODO - Determine here whether the player is the host or not
		if (update_fields_trigger) {
			update_fields_trigger.e.AddListener(update_fields);
		}
	}

	public void refresh_all_fields_if_currently_open() {
		if (currently_open) {
			clear_fields();
			create_all_fields();
		}
	}

	// Wrapper to create all UI input fields for config customization
	public virtual void create_all_fields() {
		create_fields(ui_parameters_ordered, new List<bool_input>());
		currently_open = true;
	}

	// Helper function for instantiating all the fields and dependent fields of a particular ui_parameters dictionary.
	protected void create_fields(OrderedDictionary ui_parameters, List<bool_input> dependencies) {

		// TODO - STILL NEED TO IMPLEMENT:
		// Map selection
			// Map specific options, dependent on current map
		// Win condition selection
			// Win condition specific options, dependent on current win condition

		foreach (object option in ui_parameters.Keys) {
			if (option is T0) {
				T0 T0_option = (T0)option;
				bool_input new_toggle = create_toggle(T0_option, dependencies);
				if (ui_dependents != null && ui_dependents.ContainsKey(T0_option)) {
					dependencies.Add(new_toggle);
					create_fields(ui_dependents[T0_option], dependencies);
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
		if (ui_parameters_ordered.Contains(option)) {
			ui_info = (ui_bool_info<T0>)ui_parameters_ordered[option];
		} else {
			ui_info = default_bool_info;
		}
		bool value;
		if (!config.bool_options.TryGetValue(option, out value)) {
			// Set default here?
		}

		bool_input new_input_object = Instantiate(bool_input_prefab.gameObject).GetComponent<bool_input>();
		new_input_object.transform.SetParent(this.transform);

		new_input_object.title.text = option_title(option); // Could instead add a Title property to each ui_(type)_info struct
		new_input_object.toggle.interactable = interactable;

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
		if (ui_parameters_ordered.Contains(option)) {
			ui_info = (ui_float_info<T0>)ui_parameters_ordered[option];
		} else {
			ui_info = default_float_info;
		}
		float value;
		if (!config.float_options.TryGetValue(option, out value)) {
			// Set default here?
		}
		value = (config.float_options[option] = Mathf.Clamp(value, ui_info.min, ui_info.max));

		float_input new_input_object = Instantiate(float_input_prefab.gameObject).GetComponent<float_input>();
		new_input_object.transform.SetParent(this.transform);

		new_input_object.title.text = option_title(option); // Could instead add a Title property to each ui_(type)_info struct
		new_input_object.min = ui_info.min;
		new_input_object.max = ui_info.max;
		new_input_object.slider.minValue = ui_info.slider_min;
		new_input_object.slider.maxValue = ui_info.slider_max;

		new_input_object.input_field.interactable = interactable;
		new_input_object.slider.interactable = interactable;

		new_input_object.set_up_listeners();
		new_input_object.on_value_changed.AddListener(edit_float(config.float_options, option));
		foreach (bool_input parent in dependencies) {
			parent.dependents.Add(new_input_object);
			new_input_object.toggle_dependencies.Add(parent);
		}

		new_input_object.input_field.text = value.ToString();
		new_input_object.slider.value = value;
	}

	private void create_int_slider(T2 option, List<bool_input> dependencies) {
		ui_int_info<T0> ui_info;
		if (ui_parameters_ordered.Contains(option)) {
			ui_info = (ui_int_info<T0>)ui_parameters_ordered[option];
		} else {
			ui_info = default_int_info;
		}
		int value;
		if (!config.int_options.TryGetValue(option, out value)) {
			// Set default here?
		}
		value = (config.int_options[option] = Mathf.Clamp(value, ui_info.min, ui_info.max));

		int_input new_input_object = Instantiate(int_input_prefab.gameObject).GetComponent<int_input>();
		new_input_object.transform.SetParent(this.transform);

		new_input_object.title.text = option_title(option); // Could instead add a Title property to each ui_(type)_info struct
		new_input_object.min = ui_info.min;
		new_input_object.max = ui_info.max;
		new_input_object.slider.minValue = ui_info.slider_min;
		new_input_object.slider.maxValue = ui_info.slider_max;

		new_input_object.slider.interactable = interactable;
		new_input_object.input_field.interactable = interactable;

		new_input_object.set_up_listeners();
		new_input_object.on_value_changed.AddListener(edit_int(config.int_options, option));
		foreach (bool_input parent in dependencies) {
			parent.dependents.Add(new_input_object);
			new_input_object.toggle_dependencies.Add(parent);
		}

		new_input_object.input_field.text = value.ToString();
		new_input_object.value = value;
	}

	// Take an enum option and turn it into a presentable string (capitalized, no underscores)
	protected static string option_title(object option) {
		string[] words = option.ToString().Split(new char[] { '_' }, System.StringSplitOptions.RemoveEmptyEntries);
		for (int i=0; i < words.Length; i++) {
			string word = words[i];
			string first_char = word.Substring(0, 1).ToUpper();
			words[i] = first_char + word.Substring(1, word.Length - 1);
		}
		return string.Join(" ", words);
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
		//Debug.Log("Destroying ALL children of this fields controller");
		foreach (Transform child in transform) {
			Destroy(child.gameObject);
		}
		currently_open = false;
	}


	// ============== UI Parameter Dicts Management ================
	// Checks every option in ui_parameters_ordered for dependencies,
	// adding them to ui_dependents and removing them from ui_parameters_ordered if so.
	protected void populate_ui_dependents() {
		Stack to_remove = new Stack();
		foreach (object option in ui_parameters_ordered.Keys) {
			object ui_info = ui_parameters_ordered[option];
			if (ui_info is ui_bool_info<T0>) {
				ui_bool_info<T0> ui_typed_info = (ui_bool_info<T0>)ui_info;
				if (ui_typed_info.has_dependency) {
					to_remove.Push(option);
					add_dependent(ui_typed_info.dependency, option, ui_info);
				}
			} else if (ui_info is ui_float_info<T0>) {
				ui_float_info<T0> ui_typed_info = (ui_float_info<T0>)ui_info;
				if (ui_typed_info.has_dependency) {
					to_remove.Push(option);
					add_dependent(ui_typed_info.dependency, option, ui_info);
				}
			} else if (ui_info is ui_int_info<T0>) {
				ui_int_info<T0> ui_typed_info = (ui_int_info<T0>)ui_info;
				if (ui_typed_info.has_dependency) {
					to_remove.Push(option);
					add_dependent(ui_typed_info.dependency, option, ui_info);
				}
			}
		}
		while (to_remove.Count > 0) {
			object remove = to_remove.Pop();
			ui_parameters_ordered.Remove(remove);
		}
	}

	// Makes sure that an ordered dictionary is initialized in ui_dependents for the respective parent,
	// and then adds option and ui_info to that dictionary.
	private void add_dependent(T0 parent, object option, object ui_info) {
		OrderedDictionary dependents;
		if (!ui_dependents.ContainsKey(parent)) {
			dependents = new OrderedDictionary();
			ui_dependents.Add(parent, dependents);
		} else {
			dependents = ui_dependents[parent];
		}
		dependents[option] = ui_info;
	}

}


// UI info structs
public struct ui_bool_info<T0> {
	public bool has_dependency; // Bool just to store whether or not the dependency should be used, since enums are non-nullable.
	public T0 dependency; // Put this field directly under the field for (dependency), and hide it if (dependency) is toggled to false or collapsed.
	public string description; // Describe the option here, to be used for its tooltip.

	public ui_bool_info(string _description) {
		this.has_dependency = false;
		this.dependency = default(T0);
		this.description = _description;
	}
	public ui_bool_info(T0 _dependency, string _description) {
		this.has_dependency = true;
		this.dependency = _dependency;
		this.description = _description;
	}
}

public struct ui_float_info<T0> {
	public bool has_dependency;
	public T0 dependency;
	public float min; // Strict minimum value, inclusive.
	public float max; // Strict maximum value, inclusive.
	public float slider_min; // Bottom value on the slider.
	public float slider_max; // Top value on the slider.
	public string description;

	public ui_float_info(float _slider_min, float _slider_max, float _min, float _max, string _description) {
		this.has_dependency = false;
		this.dependency = default(T0);
		this.slider_min = _slider_min;
		this.slider_max = _slider_max;
		this.min = _min;
		this.max = _max;
		this.description = _description;
	}
	public ui_float_info(T0 _dependency, float _slider_min, float _slider_max, float _min, float _max, string _description) {
		this.has_dependency = true;
		this.dependency = _dependency;
		this.slider_min = _slider_min;
		this.slider_max = _slider_max;
		this.min = _min;
		this.max = _max;
		this.description = _description;
	}
}

public struct ui_int_info<T0> {
	public bool has_dependency;
	public T0 dependency;
	public int min;
	public int max;
	public int slider_min;
	public int slider_max;
	public string description;

	public ui_int_info(int _slider_min, int _slider_max, int _min, int _max, string _description) {
		this.has_dependency = false;
		this.dependency = default(T0);
		this.slider_min = _slider_min;
		this.slider_max = _slider_max;
		this.min = _min;
		this.max = _max;
		this.description = _description;
	}
	public ui_int_info(T0 _dependency, int _slider_min, int _slider_max, int _min, int _max, string _description) {
		this.has_dependency = true;
		this.dependency = _dependency;
		this.slider_min = _slider_min;
		this.slider_max = _slider_max;
		this.min = _min;
		this.max = _max;
		this.description = _description;
	}
}
