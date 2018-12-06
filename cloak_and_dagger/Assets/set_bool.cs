using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class set_bool : MonoBehaviour {

    [SerializeField]
    bool_var my_bool;

    [SerializeField]
    bool value;
	// Use this for initialization
	void Start () {
        my_bool.val = value;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
