using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stun : MonoBehaviour {

	[SerializeField]
	bool_var is_stun;

	[SerializeField]
	float_event_object trigger;


	private float stun_time = 0;


	public void stun_func(float duration)
	{
		stun_time = Mathf.Max(stun_time,duration);
	}

	// Use this for initialization
	void Start () {
		trigger.e.AddListener(stun_func);
	}
	

	public void unstun()
	{
		stun_time = 0;
	}



	// Update is called once per frame
	void Update () {
		if(stun_time > 0) 
		{
			stun_time -= Time.deltaTime;
			is_stun.val = true;
		}
		else 
		{
			is_stun.val = false;
		}

	}
}
