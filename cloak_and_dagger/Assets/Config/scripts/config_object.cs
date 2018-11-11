using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public enum config_category { win_con, map, gameplay, readonly_win_con, readonly_map, readonly_gameplay }

// See the "Code Notes" doc in Google Drive for usage details.
// T0, T1, T2 should be enums specific to the config object, thus the generic type restrictions.
[System.Serializable]
public abstract class config_object<T0, T1, T2> : ScriptableObject	where T0 : struct, System.IConvertible
															where T1 : struct, System.IConvertible
															where T2 : struct, System.IConvertible {
	
	// Dicts to freely edit and read the option values.
	public Dictionary<T0, bool> bool_options;
	public Dictionary<T1, float> float_options;
	public Dictionary<T2, int> int_options;

	public abstract void copy_from_obj(config_object<T0, T1, T2> obj);

	public void fill_in_missing_options() {
		foreach (T0 t0 in System.Enum.GetValues(typeof(T0))) {
			if (!bool_options.ContainsKey(t0)) {
				bool_options.Add(t0, false);
			}
		}
		foreach (T1 t1 in System.Enum.GetValues(typeof(T1))) {
			if (!float_options.ContainsKey(t1)) {
				float_options.Add(t1, 1f);
			}
		}
		foreach (T2 t2 in System.Enum.GetValues(typeof(T2))) {
			if (!int_options.ContainsKey(t2)) {
				int_options.Add(t2, 1);
			}
		}
	}
}


// Serializable dictionary declarations
[System.Serializable]
public class Option_Bool_Dict<T> : SerializableDictionary<T, bool> { }
[System.Serializable]
public class Option_Float_Dict<T> : SerializableDictionary<T, float> { }
[System.Serializable]
public class Option_Int_Dict<T> : SerializableDictionary<T, int> { }
