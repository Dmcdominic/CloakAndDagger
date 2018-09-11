using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="connection_list_object")]
public class connection_list_object : ScriptableObject {

	[System.Serializable]
	public struct row
	{	
		[SerializeField]
		public string name;

		[SerializeField]
		public int cur_players;

		[SerializeField]
		public int max_players;

		[SerializeField]
		public bool locked;

		
		public ulong connectID;

	}

	[SerializeField]
	public List<row> rows;
	
}
