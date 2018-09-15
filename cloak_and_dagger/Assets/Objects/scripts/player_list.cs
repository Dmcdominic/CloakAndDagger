using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(menuName="player_list")]
public class player_list : ScriptableObject {
	
	[System.Serializable]
	public struct player_info
	{
		[SerializeField]
		public string name;
		[SerializeField]
		public bool ready;

		public int id;
	}

	public List<player_info> roster;

	public void ready_from_id(int id)
	{
		for(int i = 0; i < roster.Count;i++)
		{
			if(roster[i].id == id)
			{
				player_info pi = roster[i];
				pi.ready = true;
				roster[i] = pi;
				Debug.Log("?");
			}
		}
	}

	public void add()
	{
		player_info new_player = new player_info();
		new_player.name = "player" + roster.Count.ToString();
		new_player.ready = false;
		new_player.id = roster.Count;
		roster.Add(new_player);
	}

	public bool all_ready()
	{
		bool r = true;
		foreach(player_info pi in roster)
		{
			if(!pi.ready) r = false;
		}
		return r;
	}
}
