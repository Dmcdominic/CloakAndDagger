using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class pmove : NetworkBehaviour {

	[SerializeField]
	Vec2Var input_vec;

	[SerializeField]
	Transform trans;

	[SerializeField]
	float move_speed = 100;

	[SerializeField]
	float_event_object stun_trigger;

	private float stun_time = 0;

	Rigidbody2D rb;



	
	
	// Update is called once per frame
	void Update () {
		if(!isLocalPlayer)
		{
			return;
		}
		if(stun_time > 0)
		{
			return;
		}
		rb.AddForce(input_vec.val * move_speed,ForceMode2D.Force);

	}

	public void stun(float duration)
	{
		stun_time = Mathf.Max(stun_time,duration);
	}

	// Use this for initialization
	void Start () { //can we please get forward declarations
		if(!trans) trans = transform.root;
		rb = trans.GetComponent<Rigidbody2D>();
		StartCoroutine(stun_loop());
		stun_trigger.e.AddListener(stun);
	}

	public void unstun()
	{
		stun_time = 0;
	}

	IEnumerator stun_loop()
	{
		
		while(true)
		{
			if(stun_time > 0) stun_time -= Time.deltaTime;
			yield return null;
		}
		
	}
}
