using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class victory_screen : MonoBehaviour {

    [SerializeField]
    win_condition_assets_packet WCAP;

    [SerializeField]
    Text t;

    [SerializeField]
    Text subtitle;

    [SerializeField]
    int_var local_id;

    [SerializeField]
    party_var party;

	// Use this for initialization
	void OnEnable () {
		if(WCAP.game_Stats.team_Stats[WCAP.game_Stats.player_Stats[(byte)local_id.val].teamID].winner)
        {
            t.text = "Victory";
        } else
        {
            t.text = "Defeat";
        }
        if(!WCAP.win_Con_Config.bool_options[winCon_bool_option.free_for_all])
        {
            if (WCAP.game_Stats.team_Stats.Where(p => p.Value.winner).FirstOrDefault().Key == 0)
            {
                subtitle.text = "Blue Team Won!";
            } else
            {
                subtitle.text = "Red Team Won!";
            }
        } else
        {
            int o = WCAP.game_Stats.team_Stats.Where(p => p.Value.winner).FirstOrDefault().Key;
            if(o == 0)
            {
                subtitle.text = party.val.leader + " Won!";
            } else
            {
                subtitle.text = party.val.members[o - 1] + " Won!";
            }
        }
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
