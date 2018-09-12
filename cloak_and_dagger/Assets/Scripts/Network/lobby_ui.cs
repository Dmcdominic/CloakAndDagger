using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NetworkIdentity))]
public class lobby_ui : NetworkBehaviour {

	[SerializeField]
	bool_var ready;
	[SerializeField]
	player_list players;
	NetworkIdentity net_id;
	[SerializeField]
	int_var game_scene;


	public void ready_up()
	{
		players.ready_from_id(net_id);
	}

	void Start()
	{
		net_id = GetComponent<NetworkIdentity>();
		players.add(net_id);
		if(isServer) InvokeRepeating("ready_check",1,.5f);
	}


	void ready_check()
	{
		if(players.all_ready())
		{
			Rpc_start_game();

		}
	}


	[ClientRpc]
	void Rpc_start_game()
	{
		SceneManager.LoadScene(game_scene.val);
	}

}
