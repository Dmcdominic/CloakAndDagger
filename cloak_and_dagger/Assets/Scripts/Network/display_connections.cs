using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;


public class display_connections : MonoBehaviour {

	[SerializeField]
	event_object refresh;
	[SerializeField]
	string_var name_filter;
	[SerializeField]
	int_var requestDomain;

	// Use this for initialization
	void Start () {
		refresh.e.AddListener(list_connections);
	}
	

	void list_connections()
	{
		int eloScore = 0;
		bool filter_private = false;
		int start = 0;
		int size = 4;
		NetworkManager.singleton.matchMaker.ListMatches(start,size,name_filter.val,filter_private,eloScore,requestDomain.val,on_list);
	}

	void on_list(bool success, string extendedInfo,List<MatchInfoSnapshot> matches)
	{
		foreach(MatchInfoSnapshot snap in matches)
		{
			make_row(snap.name,snap.currentSize,snap.maxSize,snap.isPrivate,snap.networkId);
		}
	}

	void make_row(string name,int cur_players,int max_players,bool locked,UnityEngine.Networking.Types.NetworkID connectID)
	{
		
	}

	// Update is called once per frame
	void Update () {
		
	}



}
