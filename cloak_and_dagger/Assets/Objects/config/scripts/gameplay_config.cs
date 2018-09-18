using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Add options here
public enum gameplay_bool_option { heartbeat }
public enum gameplay_float_option { heartbeat_interval, dagger_cooldown }
public enum gameplay_int_option { int_test }

[CreateAssetMenu(menuName = "config/gameplay")]
public class gameplay_config : config_object<gameplay_bool_option, gameplay_float_option, gameplay_int_option> {

	public new GameplayOption_Bool_Dict bool_options = new GameplayOption_Bool_Dict();
	public new GameplayOption_Float_Dict float_options = new GameplayOption_Float_Dict();
	public new GameplayOption_Int_Dict int_options = new GameplayOption_Int_Dict();

	public gameplay_config() {
		// ========== Populate option UI parameters here ==========
		// bool
		ui_bool_parameters.Add(gameplay_bool_option.heartbeat, new ui_bool_info<gameplay_bool_option>(0, "All players are briefly revealed at regular intervals."));
		// float
		ui_float_parameters.Add(gameplay_float_option.heartbeat_interval, new ui_float_info<gameplay_bool_option>(2, 1f, 20f, "The interval between each heartbeat reveal, in seconds."));
		ui_float_parameters.Add(gameplay_float_option.dagger_cooldown, new ui_float_info<gameplay_bool_option>(3, 0.01f, 20f, "The cooldown for throwing a dagger."));
		// int
		ui_int_parameters.Add(gameplay_int_option.int_test, new ui_int_info<gameplay_bool_option>(4, 1, 20, "Test int parameter."));
	}

}

[System.Serializable]
public class GameplayOption_Bool_Dict : Option_Bool_Dict<gameplay_bool_option> { }
[System.Serializable]
public class GameplayOption_Float_Dict : Option_Float_Dict<gameplay_float_option> { }
[System.Serializable]
public class GameplayOption_Int_Dict : Option_Int_Dict<gameplay_int_option> { }
