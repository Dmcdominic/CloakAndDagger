using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Add options here
public enum gameplay_bool_option { heartbeat }
public enum gameplay_float_option { heartbeat_interval, dagger_cooldown }
public enum gameplay_uint_option { uint_test }

[CreateAssetMenu(menuName = "config/gameplay")]
public class gameplay_config : config_object<gameplay_bool_option, gameplay_float_option, gameplay_uint_option> {

	public new GameplayOption_Bool_Dict bool_options = new GameplayOption_Bool_Dict();
	public new GameplayOption_Float_Dict float_options = new GameplayOption_Float_Dict();
	public new GameplayOption_Uint_Dict uint_options = new GameplayOption_Uint_Dict();

}

[System.Serializable]
public class GameplayOption_Bool_Dict : Option_Bool_Dict<gameplay_bool_option> { }
[System.Serializable]
public class GameplayOption_Float_Dict : Option_Float_Dict<gameplay_float_option> { }
[System.Serializable]
public class GameplayOption_Uint_Dict : Option_Uint_Dict<gameplay_uint_option> { }
