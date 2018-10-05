using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public enum GameState { Playing, Paused, Inactive, Transitioning };

public class GM : MonoBehaviour {

	// Properties
	public List<int> worldLevelTotals;

	// References
	[HideInInspector]
	private GameState gameState;


	// Singleton management
	private static GM _instance;
	public static GM Instance {
		get {
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType<GM>();
				if (_instance == null) {
					Debug.LogError("No GM found in the scene");
				}
			}
			return _instance;
		}
	}

	private void Awake() {
		if (_instance != null && _instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			_instance = this;
		}

		if (transform.parent == null) {
			DontDestroyOnLoad(this);
		}

		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	// Project management utility
	void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		
	}

	public static void changeToMainMenu(bool withTransition = true) {
		if (withTransition) {
			loadSceneWithTransition(1);
		} else {
			SceneManager.LoadScene(1);
		}
	}

	// Load scene with transition
	private static void loadSceneWithTransition(int sceneIndex) {
		FadeOverlayManager.Instance.fadeToBlack(sceneIndex);
	}

}
