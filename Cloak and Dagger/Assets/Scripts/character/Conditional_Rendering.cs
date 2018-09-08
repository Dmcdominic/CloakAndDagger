using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Conditional_Rendering : NetworkBehaviour {

	[SerializeField]
	Material local_mat;

	[SerializeField]
	Material non_local_mat;

	private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer>();
		if(isLocalPlayer)
		{
			sr.material = local_mat;
		}
		else
		{
			sr.material = non_local_mat;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
