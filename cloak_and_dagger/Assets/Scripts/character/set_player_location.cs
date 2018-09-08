using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class set_player_location : MonoBehaviour {

	[SerializeField]
	Vec2Var output;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(output)
		{
			output.val = transform.position;
		}			
	}
}
