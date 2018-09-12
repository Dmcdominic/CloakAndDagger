using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class Network_Manager : NetworkManager {

	[SerializeField]
	player_list players;
	[SerializeField]
	NetworkConnection_var client_conn;
	[SerializeField]
	public bool_var spawn_on_scene;

	public void Start()
	{
		players.roster = new List<player_list.player_info>();
		spawn_on_scene.val = false;
	}

	public override void OnClientSceneChanged(NetworkConnection connection)
	{
		print("hii");
		//base.OnClientSceneChanged(connection);

		if(spawn_on_scene.val)
		{
			ClientScene.Ready(connection);

			ClientScene.AddPlayer(0);	
			spawn_on_scene.val = false;
		}
	}
	//volgar the viking
}
