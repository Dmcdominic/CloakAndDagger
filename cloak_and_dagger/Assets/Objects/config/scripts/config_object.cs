using System.Collections;
using System.Collections.Generic;
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
	public Dictionary<T0, ui_bool_info<T0>> ui_bool_parameters { get; protected set; }
	public Dictionary<T1, ui_float_info<T0>> ui_float_parameters { get; protected set; }
	public Dictionary<T2, ui_int_info<T0>> ui_int_parameters { get; protected set; }

	// Default UI info structs
	//public ui_bool_info<T0> default_bool_info = new ui_bool_info<T0>(0, "");
	//public ui_float_info<T0> default_float_info = new ui_float_info<T0>(0, 0.01f, 20.0f, "");
	//public ui_int_info<T0> default_int_info = new ui_int_info<T0>(0, 1, 100, "");

	public config_object() {
		ui_bool_parameters = new Dictionary<T0, ui_bool_info<T0>>();
		ui_float_parameters = new Dictionary<T1, ui_float_info<T0>>();
		ui_int_parameters = new Dictionary<T2, ui_int_info<T0>>();

		//System.Enum.GetValues(typeof(T0));
	}

}

	
// UI info structs
public struct ui_bool_info<T0> {
	public bool has_dependency; // Bool just to store whether or not the dependency should be used, since enums are non-nullable.
	public T0 dependency; // Put this field directly under the field for (dependency), and hide it if (dependency) is toggled to false.
	public int relative_order; // Use this to customize the ordering of all options in this category. Values are relative to those within the same dependency, with a default of 0.
	public string description; // Describe the option here, to be used for its tooltip.

	public ui_bool_info(int _relative_order, string _description) {
		this.has_dependency = false;
		this.dependency = default(T0);
		this.relative_order = _relative_order;
		this.description = _description;
	}
	public ui_bool_info(T0 _dependency, int _relative_order, string _description) {
		this.has_dependency = true;
		this.dependency = _dependency;
		this.relative_order = _relative_order;
		this.description = _description;
	}
}

public struct ui_float_info<T0> {
	public bool has_dependency;
	public T0 dependency;
	public int relative_order;
	public float min; // Minimum value, inclusive.
	public float max; // Maximum value, inclusive.
	public string description;

	public ui_float_info(int _relative_order, float _min, float _max, string _description) {
		this.has_dependency = false;
		this.dependency = default(T0);
		this.relative_order = _relative_order;
		this.min = _min;
		this.max = _max;
		this.description = _description;
	}
	public ui_float_info(T0 _dependency, int _relative_order, float _min, float _max, string _description) {
		this.has_dependency = true;
		this.dependency = _dependency;
		this.relative_order = _relative_order;
		this.min = _min;
		this.max = _max;
		this.description = _description;
	}
}

public struct ui_int_info<T0> {
	public bool has_dependency;
	public T0 dependency;
	public int relative_order;
	public int min;
	public int max;
	public string description;

	public ui_int_info(int _relative_order, int _min, int _max, string _description) {
		this.has_dependency = false;
		this.dependency = default(T0);
		this.relative_order = _relative_order;
		this.min = _min;
		this.max = _max;
		this.description = _description;
	}
	public ui_int_info(T0 _dependency, int _relative_order, int _min, int _max, string _description) {
		this.has_dependency = true;
		this.dependency = _dependency;
		this.relative_order = _relative_order;
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
