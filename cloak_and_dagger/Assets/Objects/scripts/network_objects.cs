using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[CreateAssetMenu(menuName="variables/network_objects")]
public class network_objects : ScriptableObject {

	[SerializeField]
	public NetworkLobbyManager lobby_manager;
	[SerializeField]
	public NetworkLobbyPlayer lobby_player;
}
