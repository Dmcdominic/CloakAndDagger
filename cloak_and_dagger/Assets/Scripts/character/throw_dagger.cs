using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

public class throw_dagger : NetworkBehaviour {



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

	[SerializeField]
	event_object trigger;


	public void throw_func() //too many times have I tried to name a func throw.
	{
		Cmd_throw(_origin.val,_dest.val,cast_buffer);
			
	}

	[Command]
	public void Cmd_throw(Vector2 origin,Vector2 dest,float cast_buffer)
	{
			Vector3 position = origin;
			Vector3 dir = dest - origin;
			Quaternion rotation = Quaternion.Euler(0,0,Mathf.Rad2Deg * Mathf.Atan2(dir.y,dir.x));
			GameObject my_dagger = Instantiate(dagger_prefab,position,rotation);			
			my_dagger.transform.position += my_dagger.transform.right * cast_buffer;
			Rigidbody2D rb = my_dagger.GetComponent<Rigidbody2D>();

			NetworkServer.Spawn(my_dagger);
			
			if(rb)
			{
				rb.AddForce(my_dagger.transform.right * speed,ForceMode2D.Force);
			}
	}

	// Use this for initialization
	void Start () {
		if(isLocalPlayer)
			trigger.e.AddListener(throw_func);
	}
	
	// Update is called once per frame
	void Update () {

	}


	
}
