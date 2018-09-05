using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class set_aim_location : MonoBehaviour {

	[SerializeField]
	Vec2Var output;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(output)
		{
			Vector3 pos = Input.mousePosition;
			pos.z = 10;
			output.val = Camera.main.ScreenToWorldPoint(pos);
		}			
	}
}
