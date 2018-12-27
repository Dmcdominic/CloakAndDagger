using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class map_loader : MonoBehaviour {

	public string_event_object load_map;
	public int_var maps_starting_build_index;

	//private Dictionary<string, AsyncOperation> loaded_scenes;


	private void Awake() {
		DontDestroyOnLoad(gameObject);
	}

	private void Start() {
		//loaded_scenes = new Dictionary<string, AsyncOperation>();
		for (int build_index = maps_starting_build_index.val; build_index < SceneManager.sceneCountInBuildSettings; build_index++) {
			//AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(build_index, LoadSceneMode.Additive); // Tried with and without additive
			//asyncOperation.allowSceneActivation = false;
			//loaded_scenes[SceneManager.GetSceneByBuildIndex(build_index).name] = asyncOperation;
		}
		if (load_map) {
			load_map.e.AddListener(on_load_scene_event);
		}
	}

	private void on_load_scene_event(string scene_name) {
        //print("Allowing scene activation for scene w/ scene_name: " + scene_name);
        //loaded_scenes[scene_name].allowSceneActivation = true;
        //SceneManager.UnloadSceneAsync
    }
}
