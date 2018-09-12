using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.SceneManagement;


public class display_connections : MonoBehaviour {

	[SerializeField]
	event_object refresh;
	[SerializeField]
	string_var name_filter;
	[SerializeField]
	int_var requestDomain;
	[SerializeField]
	connection_list_object connection_obj;
	[SerializeField]
	int_var port;
	[SerializeField]
	event_object host_event;
	[SerializeField]
	string_var host_name;
	[SerializeField]
	string_var host_password;
	[SerializeField]
	int_var lobby_id;
	[SerializeField]
	event_object ready_up;

	// Use this for initialization
	void Start () {
		refresh.e.AddListener(list_connections);
		host_event.e.AddListener(host);
		ready_up.e.AddListener(on_ready);
		NetworkManager.singleton.StartMatchMaker();
		list_connections();
	}
	

	void on_ready()
	{
		
	}


	void list_connections()
	{
		int eloScore = 0;
		bool filter_private = false;
		int start = 0;
		int size = 20;
		NetworkManager.singleton.matchMaker.ListMatches(start,size,name_filter.val,filter_private,eloScore,requestDomain.val,on_list);
	}

	void on_list(bool success, string extendedInfo,List<MatchInfoSnapshot> matches)
	{
		connection_obj.rows = new List<connection_list_object.row>();
		foreach(MatchInfoSnapshot snap in matches)
		{
			make_row(snap.name,snap.currentSize,snap.maxSize,snap.isPrivate,snap.networkId);
		}
	}

	void make_row(string name,int cur_players,int max_players,bool locked,UnityEngine.Networking.Types.NetworkID connectID)
	{
		connection_list_object.row r = new connection_list_object.row();
		r.cur_players = cur_players;
		r.name = name;
		r.max_players = max_players;
		r.locked = locked;
		r.connectID = (ulong)connectID;
		connection_obj.rows.Add(r);
	}

	void host()
	{
		print(NetworkManager.singleton.matchMaker);
		NetworkManager.singleton.matchMaker.CreateMatch(host_name.val,20,true,host_password.val,"","",0,0,host_callback);
	}

	void host_callback(bool success,string extendedInfo,MatchInfo matchInfo)
	{
		if(success)
		{
			NetworkServer.Listen(matchInfo,port.val);
			SceneManager.LoadScene(lobby_id.val);
		}
	}
}
