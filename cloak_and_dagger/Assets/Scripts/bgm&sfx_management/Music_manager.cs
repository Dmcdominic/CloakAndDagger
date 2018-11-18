using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_manager : MonoBehaviour {

    public AudioSource source;
    public string_event_object bgm_trigger;
    public String_Audioclip_Dict BGMs;

	// Use this for initialization
	void Awake() {
        bgm_trigger.e.AddListener(play_by_name);
        DontDestroyOnLoad(gameObject);
    }

    void play_by_name(string bgm_name) {
        if (BGMs.ContainsKey(bgm_name)) {
            source.Stop();
            source.clip = BGMs[bgm_name];
            source.Play();
        }
    }
}