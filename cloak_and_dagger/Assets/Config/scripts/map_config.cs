﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum map_bool_option { dynamic_lights, hazards }
public enum map_float_option { }
public enum map_int_option { }

//[CreateAssetMenu(menuName = "config/map")]
[System.Serializable]
public class map_config : config_object<map_bool_option, map_float_option, map_int_option> {

	public string _map;
	public string map {
		get { return _map; }
		set {
			if (_map != value) {
				_map = value;
				map_changed.Invoke();
				update_one_config_value.Invoke(-1, _map, (int)config_category.map);
			}
		}
	}
	public all_maps_list all_map_infos;
	public event_object map_changed;
	public config_option_event_object update_one_config_value;
	public win_con_config win_Con_Config;
	public player_int teams;

	public map_info current_map_info {
		get {
			return all_map_infos.map_infos[map];
		}
	}
	public Vector2 next_spawn_point(int player_id) {
		bool ffa = win_Con_Config.bool_options[winCon_bool_option.free_for_all];
		return current_map_info.spawn_points(ffa, teams[player_id]).next;
	}

	public new MapOption_Bool_Dict bool_options = new MapOption_Bool_Dict();
	public new MapOption_Float_Dict float_options = new MapOption_Float_Dict();
	public new MapOption_Int_Dict int_options = new MapOption_Int_Dict();


	public map_config() {
		base.bool_options = bool_options;
		base.float_options = float_options;
		base.int_options = int_options;
	}

	public void switch_map(string new_map) {
		map = new_map;
	}

	public override void copy_from_obj(config_object<map_bool_option, map_float_option, map_int_option> obj) {
		map_config casted_obj = (map_config)obj;
		map = casted_obj.map;
		bool_options = (MapOption_Bool_Dict)casted_obj.bool_options;
		float_options = (MapOption_Float_Dict)casted_obj.float_options;
		int_options = (MapOption_Int_Dict)casted_obj.int_options;

		fill_in_missing_options();
	}
}


// Serializable dictionary declarations
[System.Serializable]
public class MapOption_Bool_Dict : Option_Bool_Dict<map_bool_option> { }
[System.Serializable]
public class MapOption_Float_Dict : Option_Float_Dict<map_float_option> { }
[System.Serializable]
public class MapOption_Int_Dict : Option_Int_Dict<map_int_option> { }
