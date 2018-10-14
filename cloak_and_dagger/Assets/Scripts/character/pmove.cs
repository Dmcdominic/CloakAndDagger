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
	bool_var is_stun;


	Rigidbody2D rb;

    public player_sync_data psd;


	
	// Update is called once per frame
	void Update () {
		if(!isLocalPlayer)
		{
			return;
		}
		if(is_stun.val)
		{
			return;
		}
		rb.AddForce(input_vec.val * move_speed,ForceMode2D.Force);
        psd.position.val = transform.position;
        psd.velocity.val = rb.velocity;

        transform.position = psd.position.update(transform.position);
        rb.velocity = psd.velocity.update(rb.velocity);

	}


	// Use this for initialization
	void Start () { //can we please get forward declarations
		if(!trans) trans = transform.root;
		rb = trans.GetComponent<Rigidbody2D>();

	}


}
