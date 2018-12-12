using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gg : MonoBehaviour {

    [SerializeField]
    event_object end_game;

    [SerializeField]
    client_var client;

    [SerializeField]
    int_var local_id;

	// Use this for initialization
	void Start () {
        end_game.e.AddListener(() => { if(local_id == 0) client.val.End_Game(); });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
