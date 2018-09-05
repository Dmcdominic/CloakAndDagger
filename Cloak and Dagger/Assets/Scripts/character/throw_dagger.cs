using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class throw_dagger : NetworkBehaviour {

	[SerializeField]
	input_config config;

	[SerializeField]
	Vec2Var _origin;

	[SerializeField]
	Vec2Var _dest;

	[SerializeField]
	float speed = 100;

	[SerializeField]
	float cast_buffer = 1;

	[SerializeField]
	GameObject dagger_prefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(config.dagger)
		{
			Vector3 position = _origin.val;
			Vector3 dir = _dest.val - _origin.val;
			Quaternion rotation = Quaternion.Euler(0,0,Mathf.Rad2Deg * Mathf.Atan2(dir.y,dir.x));
			GameObject my_dagger = Instantiate(dagger_prefab,position,rotation);
			my_dagger.transform.position += my_dagger.transform.right * cast_buffer;
			Rigidbody2D rb = my_dagger.GetComponent<Rigidbody2D>();
			if(rb)
			{
				rb.AddForce(my_dagger.transform.right * speed,ForceMode2D.Force);
			}
			NetworkServer.Spawn(my_dagger);

		}
	}
}
