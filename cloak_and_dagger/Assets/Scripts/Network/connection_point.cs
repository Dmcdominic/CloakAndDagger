using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class connection_point : MonoBehaviour {

	int index;
	[SerializeField]
	event_object refresh_event;
	[SerializeField]
	connection_list_object conn_list;
	[SerializeField]
	int_var requestDomain;
	[SerializeField]
	int_var lobby_scene;
	[SerializeField]
	network_objects net_obj;

	public string password {get;set;}

	connection_list_object.row row;

	// Use this for initialization
	void Start () {
		refresh_event.e.AddListener(refresh);
		index = transform.GetSiblingIndex();
		refresh();

	}
	
	void refresh()
	{
		if(index >= conn_list.rows.Count)
		{
			foreach(Transform trans in transform)
				trans.gameObject.SetActive(false);
		}
		else 
		{
			foreach(Transform trans in transform)
				trans.gameObject.SetActive(true);

			row = conn_list.rows[index];
			transform.GetChild(0).GetComponent<Text>().text = row.name;
			transform.GetChild(1).GetComponent<Text>().text = row.cur_players.ToString() + "/" + row.max_players.ToString();
			transform.GetChild(2).GetComponent<ui_match_lock_button>().is_locked = row.locked;
			transform.GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
			transform.GetChild(3).GetComponent<Button>().onClick.AddListener(join);
		}
	}

	void join()
	{
		refresh();
		if(password == null) password = "";
		print((UnityEngine.Networking.Types.NetworkID)row.connectID);

		NetworkManager.singleton.matchMaker.JoinMatch((UnityEngine.Networking.Types.NetworkID)row.connectID,password,"","",0,requestDomain.val,NetworkManager.singleton.OnMatchJoined);
	}




}
