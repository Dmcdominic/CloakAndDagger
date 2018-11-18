using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Add options here
public enum readonly_gameplay_bool_option { }
public enum readonly_gameplay_float_option { dagger_stun_time, dagger_buffer, absolute_dagger_range }
public enum readonly_gameplay_int_option { }

//[CreateAssetMenu(menuName = "config/readonly_gameplay")]
public class readonly_gameplay_config : config_object<readonly_gameplay_bool_option, readonly_gameplay_float_option, readonly_gameplay_int_option> {

	public new ReadonlyGameplayOption_Bool_Dict bool_options = new ReadonlyGameplayOption_Bool_Dict();
	public new ReadonlyGameplayOption_Float_Dict float_options = new ReadonlyGameplayOption_Float_Dict();
	public new ReadonlyGameplayOption_Int_Dict int_options = new ReadonlyGameplayOption_Int_Dict();

	public readonly_gameplay_config() {
		base.bool_options = bool_options;
		base.float_options = float_options;
		base.int_options = int_options;
	}

	public override void copy_from_obj(config_object<readonly_gameplay_bool_option, readonly_gameplay_float_option, readonly_gameplay_int_option> obj) {
		// todo
		throw new System.NotImplementedException();
	}
}

// Serializable dictionary declarations
[System.Serializable]
public class ReadonlyGameplayOption_Bool_Dict : Option_Bool_Dict<readonly_gameplay_bool_option> { }
[System.Serializable]
public class ReadonlyGameplayOption_Float_Dict : Option_Float_Dict<readonly_gameplay_float_option> { }
[System.Serializable]
public class ReadonlyGameplayOption_Int_Dict : Option_Int_Dict<readonly_gameplay_int_option> { }
