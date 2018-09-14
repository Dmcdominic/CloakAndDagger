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
	[SerializeField]
	bool_var spawn_on_scene;


	public void ready_up()
	{
		Cmd_ready();
	}

	[Command]
	public void Cmd_ready()
	{
		players.ready_from_id(0);
	}




	void Start()
	{
		net_id = GetComponent<NetworkIdentity>();
		players.add();
		if(isServer) InvokeRepeating("ready_check",1,.5f);
		spawn_on_scene.val = true;
	}


	void ready_check()
	{
		if(isServer && players.all_ready())
		{
			NetworkManager.singleton.ServerChangeScene("SampleScene");
 			//REPLACE ME
		}
	}



}
