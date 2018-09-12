using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lobby_ui : MonoBehaviour {

	[SerializeField]
	network_objects net_obj;


	public void ready_up(bool b)
	{
		
		if(net_obj.lobby_player)
		{
			if(b)
			{
				net_obj.lobby_player.SendReadyToBeginMessage();
			}
			else 
			{
				net_obj.lobby_player.SendNotReadyToBeginMessage();
			}
		}
	}

	void Start()
	{
		net_obj.lobby_player = Instantiate(net_obj.lobby_manager.lobbyPlayerPrefab);
	}


}
