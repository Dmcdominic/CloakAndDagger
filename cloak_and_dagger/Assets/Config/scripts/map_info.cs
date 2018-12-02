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

	[SerializeField]
	vec2_list ffa_spawn_points;

	[SerializeField]
	vec2_list blue_spawn_points;

	[SerializeField]
	vec2_list red_spawn_points;

    public void init_spawnpoints() {
		ffa_spawn_points.set_counter(0);
		blue_spawn_points.set_counter(0);
		red_spawn_points.set_counter(0);
	}

	public vec2_list spawn_points(bool free_for_all, int team_id) {
		if (free_for_all) {
			return ffa_spawn_points;
		} else if (team_id == 0) {
			return blue_spawn_points;
		} else {
			return red_spawn_points;
		}
	}

}
