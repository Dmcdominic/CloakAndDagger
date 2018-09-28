using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

// See the "Code Notes" doc in Google Drive for usage details.
// T0, T1, T2 should be enums specific to the config object, thus the generic type restrictions.
public class config_object<T0, T1, T2> : ScriptableObject	where T0 : struct, System.IConvertible
															where T1 : struct, System.IConvertible
															where T2 : struct, System.IConvertible {
	
	// Dicts to freely edit and read the option values.
	public Dictionary<T0, bool> bool_options;
	public Dictionary<T1, float> float_options;
	public Dictionary<T2, int> int_options;

	// Dicts to store the parameters for each option within the editor.
	// Should be set within the constructor of the particular config_object.
	// See "UI info structs" below for details.
	public OrderedDictionary ui_parameters_ordered = new OrderedDictionary();
	public Dictionary<T0, OrderedDictionary> ui_dependents = new Dictionary<T0, OrderedDictionary>();

	// Default UI info structs
	public ui_bool_info<T0> default_bool_info = new ui_bool_info<T0>("");
	public ui_float_info<T0> default_float_info = new ui_float_info<T0>(0.01f, 20.0f, 0f, 100f, "");
	public ui_int_info<T0> default_int_info = new ui_int_info<T0>(1, 20, 0, 100, "");

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

	// Makes sure that an ordered dictionary is in ui_dependents for the respective parent,
	// and then adds option and ui_info.
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

	// Copies the values of the first config_object to the second
	public static void copy_values(config_object<T0, T1, T2> obj1, config_object<T0, T1, T2> obj2) {
		// TODO
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


// Serializable dictionary declarations
[System.Serializable]
public class Option_Bool_Dict<T> : SerializableDictionary<T, bool> { }
[System.Serializable]
public class Option_Float_Dict<T> : SerializableDictionary<T, float> { }
[System.Serializable]
public class Option_Int_Dict<T> : SerializableDictionary<T, int> { }
