﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {

	// Settings
	public static float soundFXTriggerDelay = 0.4f;

	// Editor fields
	public Slider MasterVolumeSlider;
	public Slider MusicVolumeSlider;
	public Slider SFXVolumeSlider;

	public Toggle FullscreenToggle;
	//public Toggle CameraShakeToggle;

	// Properties
	private float soundFXTriggerTimer = 0;


	// Initialize listeners
	void Start() {
		FullscreenToggle.onValueChanged.AddListener (delegate {OnFullscreenToggle ();} );
		//CameraShakeToggle.onValueChanged.AddListener (delegate {OnCameraShakeToggle ();} );

		MasterVolumeSlider.onValueChanged.AddListener (delegate {OnMasterVolumeChange ();} );
		MusicVolumeSlider.onValueChanged.AddListener (delegate {OnMusicVolumeChange ();} );
		SFXVolumeSlider.onValueChanged.AddListener (delegate {OnSFXVolumeChange ();} );
	}

	private void Update() {
		if (soundFXTriggerTimer > 0) {
			soundFXTriggerTimer -= Time.unscaledDeltaTime;
		}
	}

	// Set each toggle and slider to their current settings values
	void OnEnable() {
		FullscreenToggle.isOn = SettingsManager.gameSettings.fullscreen;
		//CameraShakeToggle.isOn = SettingsManager.gameSettings.cameraShake;

		MasterVolumeSlider.value = SettingsManager.gameSettings.masterVolume;
		MusicVolumeSlider.value = SettingsManager.gameSettings.localMusicVolume;
		SFXVolumeSlider.value = SettingsManager.gameSettings.localSFXVolume;
	}

	// Update fullscreen setting
	public void OnFullscreenToggle() {
		SettingsManager.changeFullscreen (FullscreenToggle.isOn);
	}

	// Update camera shake setting
	//public void OnCameraShakeToggle() {
	//	SettingsManager.changeCameraShake(CameraShakeToggle.isOn);
	//}

	// Update volume settings
	public void OnMasterVolumeChange() {
		SettingsManager.changeMasterVolume(MasterVolumeSlider.value);
	}

	public void OnMusicVolumeChange() {
		SettingsManager.changeMusicVolume(MusicVolumeSlider.value);
	}

	public void OnSFXVolumeChange() {
		SettingsManager.changeSFXVolume(SFXVolumeSlider.value);
		if (soundFXTriggerTimer <= 0) {
			MusicManager.play_by_name("keycard_pickup");
			soundFXTriggerTimer = soundFXTriggerDelay;
		}
	}
}
