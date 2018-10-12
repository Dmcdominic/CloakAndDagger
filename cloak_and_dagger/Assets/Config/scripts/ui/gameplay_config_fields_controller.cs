﻿using System.Collections;
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
		// NOTE - The generic enum type argument in each new ui_(type)_info MUST be the (category)_bool_option.
		// See gameplay_config_fields_controller as an example of this.
		ui_parameters_ordered.Add(gameplay_bool_option.initial_reveal, new ui_bool_info<gameplay_bool_option>("All players are revealed briefly before the match begins."));
		ui_parameters_ordered.Add(gameplay_float_option.respawn_delay, new ui_float_info<gameplay_bool_option>(0f, 20f, 0f, 500f, "The delay between death and respawn, assuming the player has any lives remaining."));
		ui_parameters_ordered.Add(gameplay_int_option.int_test, new ui_int_info<gameplay_bool_option>(1, 20, 0, 50, "Test int parameter."));
		ui_parameters_ordered.Add(gameplay_float_option.reflection_time, new ui_float_info<gameplay_bool_option>(0.1f, 5f, 0f, 100f, "Duration of the reflection ability."));
		ui_parameters_ordered.Add(gameplay_bool_option.heartbeat, new ui_bool_info<gameplay_bool_option>("All players are briefly revealed at regular intervals."));
		ui_parameters_ordered.Add(gameplay_float_option.heartbeat_interval, new ui_float_info<gameplay_bool_option>(gameplay_bool_option.heartbeat, 1f, 20f, 0f, 100f, "The interval between each heartbeat reveal, in seconds."));
		ui_parameters_ordered.Add(gameplay_float_option.dash_cooldown, new ui_float_info<gameplay_bool_option>(0.1f, 60f, 0f, 250f, "The cooldown for dashing/blinking/teleporting."));
		ui_parameters_ordered.Add(gameplay_float_option.dash_distance, new ui_float_info<gameplay_bool_option>(0.1f, 20f, 0f, 100f, "The maximum distance you can dash/blink/teleport."));
		ui_parameters_ordered.Add(gameplay_float_option.dagger_cooldown, new ui_float_info<gameplay_bool_option>(0.1f, 30f, 0f, 250f, "The cooldown for throwing a dagger."));
		ui_parameters_ordered.Add(gameplay_float_option.dagger_speed, new ui_float_info<gameplay_bool_option>(1f, 40f, 0.1f, 250f, "The speed of a dagger after it is thrown."));
		ui_parameters_ordered.Add(gameplay_float_option.dagger_light_radius, new ui_float_info<gameplay_bool_option>(50f, 100f, 1f, 179f, "The size of the area which is made visible around any dagger."));
		ui_parameters_ordered.Add(gameplay_bool_option.dagger_collaterals, new ui_bool_info<gameplay_bool_option>("Daggers can pierce through multiple players."));

		base.populate_ui_dependents();
		base.Awake();
	}

};