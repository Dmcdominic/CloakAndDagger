﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum win_condition { last_survivor, kill_count }

public enum winCon_bool_option {  }
public enum winCon_float_option { time_limit }
public enum winCon_int_option { lives, kill_limit }

//[CreateAssetMenu(menuName = "config/win_con")]
[System.Serializable]
public class win_con_config : config_object<winCon_bool_option, winCon_float_option, winCon_int_option> {

	public win_condition _win_Condition;
	public win_condition win_Condition {
		get { return _win_Condition; }
		set {
			_win_Condition = value;
			win_con_changed.Invoke();
			send_config_sync.Invoke();
		}
	}
	public event_object win_con_changed;
	public event_object send_config_sync;

	public new WinConOption_Bool_Dict bool_options = new WinConOption_Bool_Dict();
	public new WinConOption_Float_Dict float_options = new WinConOption_Float_Dict();
	public new WinConOption_Int_Dict int_options = new WinConOption_Int_Dict();


	public win_con_config() {
		base.bool_options = bool_options;
		base.float_options = float_options;
		base.int_options = int_options;
	}

	public void switch_win_con(int new_win_con) {
		win_Condition = (win_condition)new_win_con;
	}

	public override void copy_from_obj(config_object<winCon_bool_option, winCon_float_option, winCon_int_option> obj) {
		win_con_config casted_obj = (win_con_config)obj;
		win_Condition = casted_obj.win_Condition;
		bool_options.CopyFrom(casted_obj.bool_options);
		float_options.CopyFrom(casted_obj.float_options);
		int_options.CopyFrom(casted_obj.int_options);
	}

	public void copy_from_syncable(win_con_syncable_config syncable) {
		win_Condition = syncable.win_Condition;
		bool_options.CopyFrom(syncable.bool_options.ToDictionary(x => x.Key, x => x.Value));
		float_options.CopyFrom(syncable.float_options.ToDictionary(x => x.Key, x => x.Value));
		int_options.CopyFrom(syncable.int_options.ToDictionary(x => x.Key, x => x.Value));
	}
}


[System.Serializable]
public struct win_con_syncable_config {
	public win_condition win_Condition;
	public List<KeyValuePair<winCon_bool_option, bool>> bool_options;
	public List<KeyValuePair<winCon_float_option, float>> float_options;
	public List<KeyValuePair<winCon_int_option, int>> int_options;
	public win_con_syncable_config(win_con_config config) {
		win_Condition = config.win_Condition;
		bool_options = config.bool_options.ToList();
		float_options = config.float_options.ToList();
		int_options = config.int_options.ToList();
	}
}


// Serializable dictionary declarations
[System.Serializable]
public class WinConOption_Bool_Dict : Option_Bool_Dict<winCon_bool_option> { }
[System.Serializable]
public class WinConOption_Float_Dict : Option_Float_Dict<winCon_float_option> { }
[System.Serializable]
public class WinConOption_Int_Dict : Option_Int_Dict<winCon_int_option> { }
