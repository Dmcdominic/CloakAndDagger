using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class load_scene : MonoBehaviour {

	public map_config map_Config;

	
	public void load_current_map_by_config() {
		load_scene_by_name(map_Config.map);
	}

	public void load_scene_by_name(string name) {
		SceneManager.LoadScene(name);
	}
}
