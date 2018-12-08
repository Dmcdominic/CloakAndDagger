using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball_spawn_particle_effects : MonoBehaviour {

	[SerializeField]
	GameObject particle_effects_prefab;


	// Use this for initialization
	void Start () {
		Instantiate(particle_effects_prefab, transform);
	}
}
