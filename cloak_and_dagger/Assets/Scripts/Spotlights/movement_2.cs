using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement_2 : MonoBehaviour {

	private Vector3 startposition;
	private float xstart;
	public bool left;
	public float speed;
	private float l;

	// Use this for initialization
	private void Start () {
		l = left ? -1.0f : 1.0f;
		xstart = -30.0f * l;
	}
	
	// Update is called once per frame
	private void Update () {
		transform.position = new Vector3(l * ((Time.time*speed)%90) + xstart, transform.position.y, transform.position.z);
	}
}
