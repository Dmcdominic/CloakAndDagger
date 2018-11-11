using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_manager : MonoBehaviour
{

    public AudioSource efxSource;
    public AudioSource bgmSource;
    public static Sound_manager instance = null;

    public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;

    // Use this for initialization
    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    /* If there is only 1 sound effect (e.g. sfx) for a certain action, use
     * Sound_manager.instance.play_single(sfx); if there are several sound
     * effects for a single action (eg. sfx1, sfx2, sfx3), use
     * Sound_manager.instance.randomize_sfx(sfx1, sfx2, sfx3). */

    public void play_single(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void randomize_sfx(params AudioClip[] clips)
    {
        int index = Random.Range(0, clips.Length);
        float pitch = Random.Range(lowPitchRange, highPitchRange);
        efxSource.clip = clips[index];
        efxSource.pitch = pitch;
        efxSource.Play();
    }
}