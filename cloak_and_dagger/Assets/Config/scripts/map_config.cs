using System.Collections;
using System.Collections.Generic;
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
			_map = value;
			map_changed.Invoke();
			send_config_sync.Invoke();
		}
	}
	public all_maps_list all_map_infos;
	public map_info current_map_info {
		get {
			return all_map_infos.map_infos[map];
		}
	}
	public event_object map_changed;
	public event_object send_config_sync;

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
	}

	public void copy_from_syncable(map_syncable_config syncable) {
		map = syncable.map;
		bool_options.CopyFrom(syncable.bool_options);
		float_options.CopyFrom(syncable.float_options);
		int_options.CopyFrom(syncable.int_options);
	}
}


[System.Serializable]
public struct map_syncable_config {
	public string map;
	public MapOption_Bool_Dict bool_options;
	public MapOption_Float_Dict float_options;
	public MapOption_Int_Dict int_options;
	public map_syncable_config(map_config config) {
		map = config.map;
		bool_options = config.bool_options;
		float_options = config.float_options;
		int_options = config.int_options;
	}
}


// Serializable dictionary declarations
[System.Serializable]
public class MapOption_Bool_Dict : Option_Bool_Dict<map_bool_option> { }
[System.Serializable]
public class MapOption_Float_Dict : Option_Float_Dict<map_float_option> { }
[System.Serializable]
public class MapOption_Int_Dict : Option_Int_Dict<map_int_option> { }
