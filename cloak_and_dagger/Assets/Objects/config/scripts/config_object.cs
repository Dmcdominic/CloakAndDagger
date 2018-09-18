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
	public Dictionary<T2, uint> uint_options;

	// Dicts to indicate a preferred order of toggle/field population within the customization menu/UI.
	// (This only applies if we take the auto-population route.)
	// Should be set within the constructor of the particular config_object, if desired.
	public Dictionary<T0, uint> ui_bool_population_order { get; protected set; }
	public Dictionary<T1, uint> ui_float_population_order { get; protected set; }
	public Dictionary<T2, uint> ui_uint_population_order { get; protected set; }

}

[System.Serializable]
public class Option_Bool_Dict<T> : SerializableDictionary<T, bool> { }
[System.Serializable]
public class Option_Float_Dict<T> : SerializableDictionary<T, float> { }
[System.Serializable]
public class Option_Uint_Dict<T> : SerializableDictionary<T, uint> { }
