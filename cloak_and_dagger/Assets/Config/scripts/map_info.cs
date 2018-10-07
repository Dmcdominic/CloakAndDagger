using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "config/map_info")]
public class map_info : ScriptableObject {

	public string map_name;
	public Sprite thumbnail;

	public List<win_condition> compatible_win_cons;

	public List<map_bool_option> compatible_bool_options;
	public List<map_float_option> compatible_float_options;
	public List<map_int_option> compatible_int_options;

}
