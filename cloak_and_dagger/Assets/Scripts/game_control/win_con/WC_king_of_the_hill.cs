using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WC_king_of_the_hill : win_condition_controller {

    [SerializeField]
    player_float time_in;

    [SerializeField]
    int_float_event time_score;

    [SerializeField]
    event_object activate_hills;

    public override win_condition win_Condition
    {
        get
        {
            return win_condition.king_of_the_hill;
        }
    }

    public override bool free_for_all_compatible { get { return true; } } 

    protected override void init()
    {
        
    }

    protected override void on_game_start()
    {
        time_in.init(player_stats_dict.Values.Select(v => v.playerID).ToDictionary(v => (int)v, v => 0f));
        time_score.e.AddListener((i,f) => 
        {
			print("checking player: " + (byte)i);
			print("it says player is on team: " + player_stats_dict[(byte)i].teamID);
			team_stats t = team_stats_dict[player_stats_dict[(byte)i].teamID];
            t.time_in_hill += f;
            if (t.time_in_hill > WCAP.win_Con_Config.float_options[winCon_float_option.time_to_win])
                end_game_general(new List<byte>(new byte[] { t.teamID })); 
        });
        activate_hills.Invoke();
    }

    protected override void on_player_killed(death_event_data death_data)
    {
        
    }

    protected override void on_timeout()
    {
        end_game_general(
            new List<byte>(
                new byte[]
                {
                   team_stats_dict.Values.Aggregate((p1,p2) => p1.time_in_hill > p2.time_in_hill ? p1 : p2).teamID
                }));
    }

}
