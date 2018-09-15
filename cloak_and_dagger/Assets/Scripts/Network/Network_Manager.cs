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
		//readyup reset
	}

	public override void OnClientSceneChanged(NetworkConnection connection)
	{
		print("hii");
		//base.OnClientSceneChanged(connection);

		if(spawn_on_scene.val)
		{
			ClientScene.Ready(connection);

			if(ClientScene.localPlayers.Count == 0)
			{
				ClientScene.AddPlayer(0);		
			}
			else 
			{
				bool no_player = true;
				foreach(var playerController in ClientScene.localPlayers)
				{
					if(playerController.gameObject != null)
					{
						no_player = false;
					}
				}
				if(no_player)
				{
					ClientScene.AddPlayer(0);
				}				
			}

			spawn_on_scene.val = false;
		}
	}
}
