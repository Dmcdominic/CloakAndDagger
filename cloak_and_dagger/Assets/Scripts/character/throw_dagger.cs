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
	float_event_object trigger;

	[SerializeField]
	dagger_config dagger_Config;


	public void throw_func(float cooldown) //too many times have I tried to name a func throw.
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
		my_dagger.GetComponent<dagger_data_carrier>().dagger_Data = create_dagger_data();
		Rigidbody2D rb = my_dagger.GetComponent<Rigidbody2D>();
			

		NetworkServer.Spawn(my_dagger);
			
		if(rb)
		{
			//change this to a set velocity, forces don't apply instantly over the network
			rb.velocity = my_dagger.transform.right * speed;
		}
	}

	// Use this for initialization
	void Start () {
		if(isLocalPlayer)
			trigger.e.AddListener(throw_func);
	}

	// Edit the properties of the dagger here before throwing it
	private dagger_data create_dagger_data() {
		return new dagger_data(dagger_Config.collaterals);
	}
	
}
