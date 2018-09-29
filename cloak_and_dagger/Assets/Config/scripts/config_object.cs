using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

// See the "Code Notes" doc in Google Drive for usage details.
// T0, T1, T2 should be enums specific to the config object, thus the generic type restrictions.
[System.Serializable]
public class config_object<T0, T1, T2> : ScriptableObject	where T0 : struct, System.IConvertible
															where T1 : struct, System.IConvertible
															where T2 : struct, System.IConvertible {
	
	// Dicts to freely edit and read the option values.
	public Dictionary<T0, bool> bool_options;
	public Dictionary<T1, float> float_options;
	public Dictionary<T2, int> int_options;
}


// Serializable dictionary declarations
[System.Serializable]
public class Option_Bool_Dict<T> : SerializableDictionary<T, bool> { }
[System.Serializable]
public class Option_Float_Dict<T> : SerializableDictionary<T, float> { }
[System.Serializable]
public class Option_Int_Dict<T> : SerializableDictionary<T, int> { }
