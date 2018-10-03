using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "config/win_con_info")]
public class win_con_info : ScriptableObject {

	public List<winCon_bool_option> compatible_bool_options;
	public List<winCon_float_option> compatible_float_options;
	public List<winCon_int_option> compatible_int_options;

}
