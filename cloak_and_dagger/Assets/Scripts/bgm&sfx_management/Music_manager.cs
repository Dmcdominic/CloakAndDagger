using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_manager : MonoBehaviour {

    public AudioSource source;
    public string_event_object bgm_trigger;
    public String_Audioclip_Dict BGMs;

	private string current_track = "";


	// Use this for initialization
	void Awake() {
        DontDestroyOnLoad(gameObject);
        bgm_trigger.e.AddListener(play_by_name);
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