using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(NetworkLobbyManager))]
public class set_net_obj : MonoBehaviour {

	[SerializeField]
	network_objects net_obj;

	// Use this for initialization
	void Start () {
		net_obj.lobby_manager = GetComponent<NetworkLobbyManager>();
		net_obj.lobby_manager.maxPlayers = 20;
	}


	
	// Update is called once per frame
	void Update () {
		
	}
}
