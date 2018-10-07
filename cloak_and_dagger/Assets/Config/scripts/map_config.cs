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
		}
	}
	public event_object map_changed;

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

}


// Serializable dictionary declarations
[System.Serializable]
public class MapOption_Bool_Dict : Option_Bool_Dict<map_bool_option> { }
[System.Serializable]
public class MapOption_Float_Dict : Option_Float_Dict<map_float_option> { }
[System.Serializable]
public class MapOption_Int_Dict : Option_Int_Dict<map_int_option> { }
