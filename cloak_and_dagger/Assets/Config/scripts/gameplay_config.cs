using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Add options here
public enum gameplay_bool_option { heartbeat, dagger_collaterals }
public enum gameplay_float_option { heartbeat_interval, dagger_cooldown, dash_cooldown, dash_distance, reflection_time }
public enum gameplay_int_option { int_test }

[CreateAssetMenu(menuName = "config/gameplay")]
[System.Serializable]
public class gameplay_config : config_object<gameplay_bool_option, gameplay_float_option, gameplay_int_option> {

	public new GameplayOption_Bool_Dict bool_options = new GameplayOption_Bool_Dict();
	public new GameplayOption_Float_Dict float_options = new GameplayOption_Float_Dict();
	public new GameplayOption_Int_Dict int_options = new GameplayOption_Int_Dict();

	public gameplay_config() {
		base.bool_options = bool_options;
		base.float_options = float_options;
		base.int_options = int_options;
	}

}


// Serializable dictionary declarations
[System.Serializable]
public class GameplayOption_Bool_Dict : Option_Bool_Dict<gameplay_bool_option> { }
[System.Serializable]
public class GameplayOption_Float_Dict : Option_Float_Dict<gameplay_float_option> { }
[System.Serializable]
public class GameplayOption_Int_Dict : Option_Int_Dict<gameplay_int_option> { }
