using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class respawner : MonoBehaviour {

	[SerializeField]
	event_object respawn_event;

	private NetworkStartPosition[] points;

	private GameObject character;

	// Use this for initialization
	void Start () {
		respawn_event.e.AddListener(respawn);	
		character = NetworkManager.singleton.playerPrefab;
		points = GetComponentsInChildren<NetworkStartPosition>();
	}

	void respawn(){
		GameObject local_character = Instantiate(character,points[Random.Range(0, points.Length)].transform.position,Quaternion.identity);
		NetworkServer.Spawn(local_character);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
