using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Add options here
public enum readonly_camera_bool_option { camera_shake }
public enum readonly_camera_float_option { shake_duration,
	dagger_you_throw_shake, fireball_you_throw_shake, dagger_other_throw_shake, fireball_other_throw_shake,
	dagger_you_kill_shake, fireball_you_kill_shake, dagger_you_die_shake, fireball_you_die_shake, dagger_other_die_shake, fireball_other_die_shake,
	dagger_hit_object_shake, fireball_hit_object_shake,
	trap_catches_you, trap_catches_other }
public enum readonly_camera_int_option { }

//[CreateAssetMenu(menuName = "config/readonly_camera")]
public class readonly_camera_config : config_object<readonly_camera_bool_option, readonly_camera_float_option, readonly_camera_int_option> {

	public new ReadonlyCameraOption_Bool_Dict bool_options = new ReadonlyCameraOption_Bool_Dict();
	public new ReadonlyCameraOption_Float_Dict float_options = new ReadonlyCameraOption_Float_Dict();
	public new ReadonlyCameraOption_Int_Dict int_options = new ReadonlyCameraOption_Int_Dict();

	public readonly_camera_config() {
		base.bool_options = bool_options;
		base.float_options = float_options;
		base.int_options = int_options;
	}

	public override void copy_from_obj(config_object<readonly_camera_bool_option, readonly_camera_float_option, readonly_camera_int_option> obj) {
		// todo
		throw new System.NotImplementedException();
	}
}

// Serializable dictionary declarations
[System.Serializable]
public class ReadonlyCameraOption_Bool_Dict : Option_Bool_Dict<readonly_camera_bool_option> { }
[System.Serializable]
public class ReadonlyCameraOption_Float_Dict : Option_Float_Dict<readonly_camera_float_option> { }
[System.Serializable]
public class ReadonlyCameraOption_Int_Dict : Option_Int_Dict<readonly_camera_int_option> { }
