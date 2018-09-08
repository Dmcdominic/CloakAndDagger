using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class lifetime : NetworkBehaviour {

	[SerializeField]
	float duration = 1;

	// Use this for initialization
	void Start () {
		if(isServer)
			StartCoroutine(tick());
	}
	
	// Update is called once per frame
	void Update () {		
	
	}

	IEnumerator tick()
	{
		while(duration >= 0)
		{
			duration -= Time.deltaTime;
			yield return null;
		}
		NetworkServer.Destroy(gameObject);
	}

}
