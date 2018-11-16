using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName="game_stats")]
public class game_stats : ScriptableObject {

	public Dictionary<byte, player_stats> player_Stats;
	public Dictionary<byte, team_stats> team_Stats;

}
