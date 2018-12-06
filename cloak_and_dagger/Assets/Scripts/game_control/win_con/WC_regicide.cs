using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WC_regicide : win_condition_controller
{

    [SerializeField]
    player_bool is_king;

    byte king = 255;

    public override win_condition win_Condition
    {
        get
        {
            return win_condition.regicide;
        }
    }

    public override bool free_for_all_compatible => true;

    protected override void init()
    {

    }

    protected override void on_game_start()
    {
        StartCoroutine(score_king());
    }

    public void un_set_king(byte team_id)
    {
        foreach (int p_id in player_stats_dict.Values
            .Where(ps => ps.teamID == team_id)
            .Select(ps => ps.playerID)) 
        {
            is_king[p_id] = false;
        }
    }

    public void set_king(byte team_id)
    {
        king = team_id;
        foreach (int p_id in player_stats_dict.Values
            .Where(ps => ps.teamID == team_id)
            .Select(ps => ps.playerID)) //Groupby?
        {
            is_king[p_id] = true;
        }
    }

    protected override void on_player_killed(death_event_data death_data)
    {
        print("hey boy");
        un_set_king(player_stats_dict[death_data.playerID].teamID);
        set_king(player_stats_dict[death_data.killerID].teamID);

    }

    protected override void on_timeout()
    {
        end_game_general(new List<byte>(new byte[] { team_stats_dict.Values.Aggregate((old_max, new_id) => (old_max.time_in_hill > new_id.time_in_hill) ? old_max : new_id).teamID}));
    }

    IEnumerator score_king()
    {
        yield return new WaitUntil(() => king != 255);
        while(true)
        {
            yield return null;
            team_stats_dict[king].time_as_king += Time.deltaTime;
        }
    }
	
    IEnumerator check_winner()
    {
        yield return new WaitUntil(() => team_stats_dict[king].time_as_king > WCAP.win_Con_Config.float_options[winCon_float_option.time_to_win]);
        end_game_general(new List<byte>(new byte[] { king }));
    }
}
