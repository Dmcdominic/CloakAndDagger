using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameplay_config_fields_controller : config_fields_controller<gameplay_bool_option, gameplay_float_option, gameplay_int_option> {
	public override config_category config_Category {
		get { return config_category.gameplay; }
	}

	public new gameplay_config config;

	// Initialization
	protected new void Awake() {
		base.config = config;

		// ========== Populate option UI parameters here ==========
		// NOTE - The order of options here will be reflected in-game in the config menu,
		// except that depedendencies will be placed directly beneath their parent toggle.
		ui_parameters_ordered.Add(gameplay_int_option.int_test, new ui_int_info<gameplay_bool_option>(1, 20, 0, 50, "Test int parameter."));
		ui_parameters_ordered.Add(gameplay_float_option.reflection_time, new ui_float_info<gameplay_bool_option>(0.1f, 5f, 0f, 100f, "Duration of the reflection ability."));
		ui_parameters_ordered.Add(gameplay_bool_option.heartbeat, new ui_bool_info<gameplay_bool_option>("All players are briefly revealed at regular intervals."));
		ui_parameters_ordered.Add(gameplay_float_option.heartbeat_interval, new ui_float_info<gameplay_bool_option>(gameplay_bool_option.heartbeat, 1f, 20f, 0f, 100f, "The interval between each heartbeat reveal, in seconds."));
		ui_parameters_ordered.Add(gameplay_float_option.dagger_cooldown, new ui_float_info<gameplay_bool_option>(0.1f, 20f, 0f, 100f, "The cooldown for throwing a dagger."));

		base.populate_ui_dependents();
		base.Awake();
	}

};