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

	public void Start()
	{
		players.roster = new List<player_list.player_info>();
	}

	public override void OnClientConnect(NetworkConnection connection)
	{
		client_conn.val = connection;
	}


}
