using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Torch {
	public GameObject torch_object;
	public Light light;
	public float intensity;

	public Torch(GameObject t, Light l, float i) {
		this.torch_object = t;
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

	public Sprite torch_off_sprite;

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
			
			GameObject t_obj = t.torch_object;

			t_obj.GetComponent<Animator>().enabled = false;
			t_obj.GetComponent<SpriteRenderer>().sprite = torch_off_sprite;
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
			torches[i].torch_object.GetComponent<Animator>().enabled = true;
		}
	}

	// Use this for initialization
	void Start () {
		num_torches = ts.Count;

		// Initializing List of torches and their intensities
		foreach(GameObject t in ts) {
			Light l = (t.GetComponentsInChildren<Light>())[0];
			Torch torch = new Torch(t, l, l.intensity);
			torches.Add(torch);
		}

		InvokeRepeating("runTorches", 0.0f, duration);
	}
	
	// Update is called once per frame
	void Update () {

	}
}