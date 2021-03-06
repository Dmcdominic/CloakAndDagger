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

		// Player movement
		ui_parameters_ordered.Add(gameplay_float_option.player_movespeed, new ui_float_info<gameplay_bool_option>(4f, 10f, -50f, 50f, "The movespeed of all players."));

		// Spawning
		ui_parameters_ordered.Add(gameplay_bool_option.initial_reveal, new ui_bool_info<gameplay_bool_option>("All players are revealed briefly before the match begins."));
		ui_parameters_ordered.Add(gameplay_float_option.respawn_delay, new ui_float_info<gameplay_bool_option>(0f, 20f, 0f, 500f, "The delay between death and respawn, assuming the player has any lives remaining."));

		// Heartbeat
		ui_parameters_ordered.Add(gameplay_bool_option.heartbeat, new ui_bool_info<gameplay_bool_option>("All players are briefly revealed at regular intervals."));
		ui_parameters_ordered.Add(gameplay_float_option.heartbeat_interval, new ui_float_info<gameplay_bool_option>(gameplay_bool_option.heartbeat, 1f, 20f, 0f, 100f, "The interval between each heartbeat reveal, in seconds."));

		// Daggers
		ui_parameters_ordered.Add(gameplay_float_option.dagger_cooldown, new ui_float_info<gameplay_bool_option>(4f, 30f, 0f, 250f, "The cooldown for throwing a dagger."));
		ui_parameters_ordered.Add(gameplay_float_option.dagger_speed, new ui_float_info<gameplay_bool_option>(1f, 40f, 0f, 250f, "The speed of a dagger after it is thrown."));
		ui_parameters_ordered.Add(gameplay_float_option.dagger_light_radius, new ui_float_info<gameplay_bool_option>(10f, 90f, 1f, 179f, "The size of the area which is made visible around a dagger."));
		ui_parameters_ordered.Add(gameplay_bool_option.dagger_collaterals, new ui_bool_info<gameplay_bool_option>("Daggers can pierce through multiple players."));
		ui_parameters_ordered.Add(gameplay_bool_option.daggers_pierce_walls, new ui_bool_info<gameplay_bool_option>("Daggers can pass through walls."));

		// Dagger/fireball collisions
		ui_parameters_ordered.Add(gameplay_bool_option.daggers_destroy_daggers, new ui_bool_info<gameplay_bool_option>("Daggers will destroy each other when they collide."));
		ui_parameters_ordered.Add(gameplay_bool_option.daggers_destroy_fireballs, new ui_bool_info<gameplay_bool_option>("Daggers will destroy fireballs when they collide."));
		ui_parameters_ordered.Add(gameplay_bool_option.fireballs_destroy_daggers, new ui_bool_info<gameplay_bool_option>("Fireballs will destroy daggers when they collide."));
		ui_parameters_ordered.Add(gameplay_bool_option.fireballs_destroy_fireballs, new ui_bool_info<gameplay_bool_option>("Fireballs will destroy each other when they collide."));

		// Fireballs
		ui_parameters_ordered.Add(gameplay_float_option.fireball_cooldown, new ui_float_info<gameplay_bool_option>(3f, 60f, 0f, 250f, "The cooldown for throwing a fireball."));
		ui_parameters_ordered.Add(gameplay_float_option.fireball_speed, new ui_float_info<gameplay_bool_option>(1f, 20f, 0f, 250f, "The speed of a fireball after it is thrown."));
		ui_parameters_ordered.Add(gameplay_float_option.fireball_light_range, new ui_float_info<gameplay_bool_option>(3f, 12f, 1f, 50f, "The range of the light emitted by a fireball."));
		ui_parameters_ordered.Add(gameplay_bool_option.fireball_collaterals, new ui_bool_info<gameplay_bool_option>("Fireballs can pierce through multiple players."));
		ui_parameters_ordered.Add(gameplay_bool_option.fireballs_pierce_walls, new ui_bool_info<gameplay_bool_option>("Fireballs can pass through walls."));

		// Blink
		ui_parameters_ordered.Add(gameplay_float_option.blink_range, new ui_float_info<gameplay_bool_option>(2f, 20f, 0f, 100f, "Maximum range of the blink ability, a short range teleportation (to your mouse position)."));
		ui_parameters_ordered.Add(gameplay_float_option.blink_cooldown, new ui_float_info<gameplay_bool_option>(0.1f, 60f, 0f, 250f, "Cooldown of the blink ability."));

		// Reflect
		ui_parameters_ordered.Add(gameplay_float_option.reflect_time, new ui_float_info<gameplay_bool_option>(0.1f, 3f, 0f, 100f, "Duration of the reflect ability. While active, any daggers that would collide with the player will instead be reflected in the opposite direction."));
		ui_parameters_ordered.Add(gameplay_float_option.reflect_cooldown, new ui_float_info<gameplay_bool_option>(2f, 30f, 0f, 100f, "Cooldown of the reflect ability."));
		ui_parameters_ordered.Add(gameplay_bool_option.fragile_reflect, new ui_bool_info<gameplay_bool_option>("The reflect ability can only reflect one dagger each time it is used."));

		// Torch
		ui_parameters_ordered.Add(gameplay_float_option.torch_cooldown, new ui_float_info<gameplay_bool_option>(10f, 60f, 0f, 250f, "The cooldown for placing a torch (at your mouse position), which illuminates an area to all players."));
		ui_parameters_ordered.Add(gameplay_float_option.torch_duration, new ui_float_info<gameplay_bool_option>(10f, 60f, 0f, 250f, "The amount of time that a placed torch will stay lit."));
		ui_parameters_ordered.Add(gameplay_float_option.torch_placement_range, new ui_float_info<gameplay_bool_option>(2f, 20f, 0f, 100f, "The maximum range from which you can place a torch."));
		ui_parameters_ordered.Add(gameplay_float_option.torch_light_range, new ui_float_info<gameplay_bool_option>(3f, 12f, 1f, 50f, "The range of the light emitted by a torch."));

		// Trap
		ui_parameters_ordered.Add(gameplay_float_option.trap_cooldown, new ui_float_info<gameplay_bool_option>(10f, 60f, 0f, 250f, "The cooldown for placing a trap (underneath you), which will catch and illuminate the first player that walks into it."));
		ui_parameters_ordered.Add(gameplay_float_option.trap_waiting_duration, new ui_float_info<gameplay_bool_option>(10f, 60f, 0f, 250f, "The amount of time that a placed trap will remain on the map before expiring. Set to 0 for unlimited duration."));
		ui_parameters_ordered.Add(gameplay_float_option.trap_hold_duration, new ui_float_info<gameplay_bool_option>(1f, 10f, 0f, 100f, "The amount of time that a trap will hold a player in place before releasing them."));
		ui_parameters_ordered.Add(gameplay_float_option.trap_light_range, new ui_float_info<gameplay_bool_option>(3f, 12f, 1f, 50f, "The range of the light emitted by a trap while holding a player in place."));

		// Other
		ui_parameters_ordered.Add(gameplay_float_option.bump_reveal_time, new ui_float_info<gameplay_bool_option>(0.5f, 5f, 0f, 100f, "The duration for which each player will be revealed to the other if they touch."));


		base.populate_ui_dependents();
		base.Awake();
	}

	public override int get_encoded_enum_bool_opt(gameplay_bool_option option) {
		return 0 + 3 * ((int)option);
	}
	public override int get_encoded_enum_float_opt(gameplay_float_option option) {
		return 1 + 3 * ((int)option);
	}
	public override int get_encoded_enum_int_opt(gameplay_int_option option) {
		return 2 + 3 * ((int)option);
	}

};