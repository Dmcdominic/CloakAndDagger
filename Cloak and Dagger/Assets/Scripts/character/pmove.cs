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

	private bool stunned;
	private Coroutine last_stun;

	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		if(!trans) trans = transform.root;
		rb = trans.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!isLocalPlayer)
		{
			return;
		}
		rb.AddForce(input_vec.val * move_speed,ForceMode2D.Force);

	}

	public void stun(float duration)
	{
		StopCoroutine(last_stun);
		last_stun = StartCoroutine(stun_loop(duration));
	}

	public void unstun()
	{
		StopCoroutine(last_stun);
		stunned = false;
	}

	IEnumerator stun_loop(float duration)
	{
		stunned = true;
		while(duration > 0)
		{
			duration -= Time.deltaTime;
			yield return null;
		}
		stunned = false;
	}
}
