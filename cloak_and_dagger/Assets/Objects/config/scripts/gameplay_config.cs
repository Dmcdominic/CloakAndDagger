using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Add options here
public enum gameplay_bool_option { heartbeat }
public enum gameplay_float_option { heartbeat_interval, dagger_cooldown, reflection_time }
public enum gameplay_int_option { int_test }

[CreateAssetMenu(menuName = "config/gameplay")]
public class gameplay_config : config_object<gameplay_bool_option, gameplay_float_option, gameplay_int_option> {

	public new GameplayOption_Bool_Dict bool_options = new GameplayOption_Bool_Dict();
	public new GameplayOption_Float_Dict float_options = new GameplayOption_Float_Dict();
	public new GameplayOption_Int_Dict int_options = new GameplayOption_Int_Dict();

	public gameplay_config() {
		base.bool_options = bool_options;
		base.float_options = float_options;
		base.int_options = int_options;

		// ========== Populate option UI parameters here ==========
		// NOTE - 
		ui_parameters_ordered.Add(gameplay_int_option.int_test, new ui_int_info<gameplay_bool_option>(1, 20, 0, 50, "Test int parameter."));
		ui_parameters_ordered.Add(gameplay_float_option.reflection_time, new ui_float_info<gameplay_bool_option>(0.1f, 5f, 0f, 100f, "Duration of the reflection ability."));
		ui_parameters_ordered.Add(gameplay_bool_option.heartbeat, new ui_bool_info<gameplay_bool_option>("All players are briefly revealed at regular intervals."));
		ui_parameters_ordered.Add(gameplay_float_option.heartbeat_interval, new ui_float_info<gameplay_bool_option>(gameplay_bool_option.heartbeat, 1f, 20f, 0f, 100f, "The interval between each heartbeat reveal, in seconds."));
		ui_parameters_ordered.Add(gameplay_float_option.dagger_cooldown, new ui_float_info<gameplay_bool_option>(0.1f, 20f, 0f, 100f, "The cooldown for throwing a dagger."));

		base.populate_ui_dependents();
	}

}


// Serializable dictionary declarations
[System.Serializable]
public class GameplayOption_Bool_Dict : Option_Bool_Dict<gameplay_bool_option> { }
[System.Serializable]
public class GameplayOption_Float_Dict : Option_Float_Dict<gameplay_float_option> { }
[System.Serializable]
public class GameplayOption_Int_Dict : Option_Int_Dict<gameplay_int_option> { }
