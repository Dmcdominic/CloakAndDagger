using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class respawner : NetworkBehaviour {

	[SerializeField]
	event_object respawn_event;
	[SerializeField]
	NetworkConnection_var conn;

	private NetworkIdentity net_id;
	private NetworkStartPosition[] points;

	private GameObject character;

	// Use this for initialization
	void Start () {
		respawn_event.e.AddListener(respawn);	
		character = NetworkManager.singleton.playerPrefab;
		points = GetComponentsInChildren<NetworkStartPosition>();
		//if(isClient) init_spawn();
	}

	void init_spawn()
	{
		ClientScene.Ready(ClientScene.readyConnection);
		ClientScene.AddPlayer(ClientScene.readyConnection,1);
	}


	void respawn(){
		ClientScene.AddPlayer(0);
		//GameObject local_character = Instantiate(character,points[Random.Range(0, points.Length)].transform.position,Quaternion.identity);
		//NetworkServer.Spawn(local_character);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
