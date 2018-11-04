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

	public vec2_list _spawn_points;

	private int pos = -1;

	public List<Vector2> spawn_points {
		get { return _spawn_points.val; }
		set { this._spawn_points.val = value; }
	}

	public Vector2 next_spawn_point {
		get { return _spawn_points.next; }
	}

}
