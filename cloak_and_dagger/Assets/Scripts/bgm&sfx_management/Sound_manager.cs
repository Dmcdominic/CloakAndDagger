using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_manager : MonoBehaviour {

    public string_event_object sfx_trigger;
	public AudioSource audio_source_prefab;

	public String_Audioclip_Dict SFXs;


    // Use this for initialization
    void Awake() {
		DontDestroyOnLoad(gameObject);
		sfx_trigger.e.AddListener(play_by_name);
    }

    void play_by_name(string sfx_name) {
        if (SFXs.ContainsKey(sfx_name) && SFXs[sfx_name] != null) {
			AudioSource source = Instantiate(audio_source_prefab, transform);
            source.playOnAwake = false;
            source.clip = SFXs[sfx_name];
            source.Play();
            Destroy(source.gameObject, source.clip.length + 0.5f);
        }
    }
}

// Should put this global setting to somewhere else

[System.Serializable]
public class String_Audioclip_Dict: SerializableDictionary<string, AudioClip> {}