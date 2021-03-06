﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Add options here
public enum gameplay_bool_option { heartbeat, initial_reveal, dagger_collaterals, daggers_destroy_daggers, daggers_destroy_fireballs, fireballs_destroy_daggers, fireballs_destroy_fireballs, daggers_pierce_walls, fireballs_pierce_walls, fireball_collaterals, fragile_reflect }
public enum gameplay_float_option { heartbeat_interval, dagger_cooldown, blink_cooldown, blink_range, reflect_time, respawn_delay, dagger_speed, dagger_light_radius, player_movespeed, bump_reveal_time, reflect_cooldown,
	fireball_cooldown, fireball_speed, fireball_light_range, torch_cooldown, torch_duration, torch_light_range, trap_cooldown, trap_waiting_duration, trap_hold_duration, trap_light_range, torch_placement_range }

public enum gameplay_int_option { }

//[CreateAssetMenu(menuName = "config/gameplay")]
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

	public override void copy_from_obj(config_object<gameplay_bool_option, gameplay_float_option, gameplay_int_option> obj) {
		gameplay_config casted_obj = (gameplay_config)obj;
		bool_options.CopyFrom(casted_obj.bool_options);
		float_options.CopyFrom(casted_obj.float_options);
		int_options.CopyFrom(casted_obj.int_options);

		fill_in_missing_options();
	}
}


// Serializable dictionary declarations
[System.Serializable]
public class GameplayOption_Bool_Dict : Option_Bool_Dict<gameplay_bool_option> { }
[System.Serializable]
public class GameplayOption_Float_Dict : Option_Float_Dict<gameplay_float_option> { }
[System.Serializable]
public class GameplayOption_Int_Dict : Option_Int_Dict<gameplay_int_option> { }
