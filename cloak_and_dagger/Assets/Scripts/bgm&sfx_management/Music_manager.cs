using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music_manager : MonoBehaviour {

    public AudioSource source;
    public string_event_object bgm_trigger;
    public String_Audioclip_Dict BGMs;

	private string current_track = "";


	// Use this for initialization
	void Awake() {
        DontDestroyOnLoad(gameObject);
        bgm_trigger.e.AddListener(play_by_name);
		SceneManager.sceneLoaded += on_scene_loaded;
    }

	private void on_scene_loaded(Scene arg0, LoadSceneMode arg1) {
		bool in_lobby = arg0.buildIndex < 2;
		if (in_lobby) {
			play_by_name("Title_screen");
		} else {
			play_by_name("Gameplay_track");
		}
	}

	void play_by_name(string bgm_name) {
		if (bgm_name == current_track) {
			return;
		}
        if (BGMs.ContainsKey(bgm_name)) {
			current_track = bgm_name;
			source.Stop();
            source.clip = BGMs[bgm_name];
            if (bgm_name != "Match_end")
                source.loop = true;
            else
                source.loop = false;
            source.Play();
        }
    }
}