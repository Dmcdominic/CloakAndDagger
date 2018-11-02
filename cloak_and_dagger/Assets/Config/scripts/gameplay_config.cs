﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Add options here
public enum gameplay_bool_option { heartbeat, initial_reveal, dagger_collaterals, daggers_destroy_daggers, daggers_pierce_walls }
public enum gameplay_float_option { heartbeat_interval, dagger_cooldown, dash_cooldown, dash_distance, reflection_time, respawn_delay, dagger_speed, dagger_light_radius, player_movespeed }
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
	}

	public void copy_from_syncable(gameplay_syncable_config syncable) {
		bool_options.CopyFrom(syncable.bool_options.ToDictionary(x => x.Key, x => x.Value));
		float_options.CopyFrom(syncable.float_options.ToDictionary(x => x.Key, x => x.Value));
		int_options.CopyFrom(syncable.int_options.ToDictionary(x => x.Key, x => x.Value));
	}
}


[System.Serializable]
public struct gameplay_syncable_config {
	public List<KeyValuePair<gameplay_bool_option, bool>> bool_options;
	public List<KeyValuePair<gameplay_float_option, float>> float_options;
	public List<KeyValuePair<gameplay_int_option, int>> int_options;
	public gameplay_syncable_config(gameplay_config config) {
		bool_options = config.bool_options.ToList();
		float_options = config.float_options.ToList();
		int_options = config.int_options.ToList();
	}
}


// Serializable dictionary declarations
[System.Serializable]
public class GameplayOption_Bool_Dict : Option_Bool_Dict<gameplay_bool_option> { }
[System.Serializable]
public class GameplayOption_Float_Dict : Option_Float_Dict<gameplay_float_option> { }
[System.Serializable]
public class GameplayOption_Int_Dict : Option_Int_Dict<gameplay_int_option> { }
