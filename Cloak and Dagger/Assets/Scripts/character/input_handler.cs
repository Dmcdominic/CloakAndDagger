using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class input_handler : MonoBehaviour {

	[SerializeField]
	input_config config;

	[SerializeField]
	Vec2Var output;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(output)
			output.val = new Vector2(config.horizontal(),config.vertical());	
	}
}
