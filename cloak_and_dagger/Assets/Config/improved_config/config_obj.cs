using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*	
 *	config_obj:
 *	An SO (scriptable object) to store open ended config values.
 *	Useful for both player customization and active development.
 *	Created by Dominic Calkosz.
 */
[System.Serializable]
[CreateAssetMenu(menuName = "config/config_obj")]
public class config_obj : ScriptableObject {

	// String-value option dictionaries
	public Option_Bool_Dict bool_options;
	public Option_Float_Dict float_options;
	public Option_Int_Dict int_options;
	public Option_String_Dict string_options;

}


// An enum to identify the different datatypes that are used as config option values.
public enum config_value_type { Bool, Float, Int, String }


// Serializable dictionary declarations
[System.Serializable]
public class Option_Bool_Dict : SerializableDictionary<string, bool> { }
[System.Serializable]
public class Option_Float_Dict : SerializableDictionary<string, float> { }
[System.Serializable]
public class Option_Int_Dict : SerializableDictionary<string, int> { }
[System.Serializable]
public class Option_String_Dict : SerializableDictionary<string, string> { }
