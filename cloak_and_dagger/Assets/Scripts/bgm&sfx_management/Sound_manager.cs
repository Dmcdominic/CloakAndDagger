﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_manager : MonoBehaviour {

    public string_event_object sfx_trigger;
    public String_Audioclip_Dict SFXs;

    // Use this for initialization
    void Awake() {
        sfx_trigger.e.AddListener(play_by_name);
        DontDestroyOnLoad(gameObject);
    }

    void play_by_name(string sfx_name) {
        if (SFXs.ContainsKey(sfx_name)) {
            AudioSource source = new AudioSource();
            source.playOnAwake = false;
            source.clip = SFXs[sfx_name];
            source.Play();
            Destroy(source);
        }
    }
}

// Should put this global setting to somewhere else

[System.Serializable]
public class String_Audioclip_Dict: SerializableDictionary<string, AudioClip> {}