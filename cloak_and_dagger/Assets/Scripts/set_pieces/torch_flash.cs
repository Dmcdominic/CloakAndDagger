using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Torch {
	public Light light;
	public float intensity;

	public Torch(Light l, float i) {
		this.light = l;
		this.intensity = i;
	}
}

public class torch_flash : MonoBehaviour {

	System.Random rnd = new System.Random();

	static float DIM_TIME = 3.0f;

	public List<GameObject> ts = new List<GameObject>();
	private int num_torches;
	private List<Torch> torches = new List<Torch>();

	public float duration = 10.0f;

	public int num_on = 0;
	private List<int> on_torches = new List<int>();

	void selectTorches() {
		on_torches = new List<int>();
		int tot = num_torches;
		int need = num_on;

		// Simulates randomly selecting num_on torches from torches
		for(int i=0; i<num_torches; i++) {
			int prob = rnd.Next(1,tot+1);
			Debug.Log(prob);
			if(prob <= need) {
				need--;
				on_torches.Add(i);
			}
			tot--;
		}
	}

	void turnOffTorch(Torch t) {
		if(t.light.enabled) {
			t.light.enabled = false;
		}
	}

	void clearTorches() {
		foreach(Torch t in torches) {
			turnOffTorch(t);
		}
	}

	void runTorches() {
		clearTorches();

		selectTorches();
		
		foreach(int i in on_torches) {
			torches[i].light.enabled = true;
		}
	}

	// Use this for initialization
	void Start () {
		num_torches = ts.Count;

		// Initializing List of torches and their intensities
		foreach(GameObject t in ts) {
			Light l = (t.GetComponentsInChildren<Light>())[0];
			Torch torch = new Torch(l, l.intensity);
			torches.Add(torch);
		}

		InvokeRepeating("runTorches", 0.0f, duration);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
