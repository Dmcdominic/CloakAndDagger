using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class win_con_ui : MonoBehaviour {


    [SerializeField]
    win_condition_assets_packet WCAP;

    string game_mode = "";


    [SerializeField]
    party_var party;


    [SerializeField]
    GameObject team_ui;
    [SerializeField]
    Text red;
    [SerializeField]
    Text blue;
    [SerializeField]
    GameObject player_text;
    [SerializeField]
    player_int teams_dict;
    [SerializeField]
    int_var local_id;
    [SerializeField]
    GameObject blue_wax;
    [SerializeField]
    GameObject red_wax;

    // Use this for initialization
    void Start() {
        WCAP.trigger_on_game_start.e.AddListener(() => go() );
    }

    void go()
    {
        switch (WCAP.win_Con_Config.win_Condition)
        {
            case win_condition.last_survivor:
                game_mode = "Last Survivor";
                break;
            case win_condition.kill_count:
                game_mode = "Kill Count";
                break;
            case win_condition.assault:
                game_mode = "Assualt";
                break;
            case win_condition.regicide:
                game_mode = "Regicide";
                break;
            case win_condition.king_of_the_hill:
                game_mode = "King of the Hill";
                break;
        }
        if(!WCAP.win_Con_Config.bool_options[winCon_bool_option.free_for_all])
        {
            StartCoroutine(teams());
        } else
        {
            StartCoroutine(individual());
        }
    }

    IEnumerator individual()
    {
        GameObject temp;
        int i;
        for(i = 0; i < WCAP.game_Stats.player_Stats.Count;i++)
        {
            temp = Instantiate(player_text, transform);
            temp.transform.SetSiblingIndex(i);
        }
        for(;i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i));
        }
        while(true)
        {
            for(i = 0; i  < WCAP.game_Stats.player_Stats.Count; i++)
            {
                transform.GetChild(i).GetComponent<Text>().text =
                    i == 0 ? party.val.leader + " ":
                    $"{party.val.members[WCAP.game_Stats.player_Stats[WCAP.game_Stats.player_Stats[(byte)i].teamID].playerID - 1]} ";
                switch (WCAP.win_Con_Config.win_Condition)
                {
                    case win_condition.last_survivor:
                        transform.GetChild(i).GetComponent<Text>().text += 
                            $"{WCAP.game_Stats.team_Stats[WCAP.game_Stats.player_Stats[(byte)i].teamID].lives_remaining} ";
                        transform.GetChild(i).GetComponent<Text>().text += "/" +
                            WCAP.win_Con_Config.int_options[winCon_int_option.lives].ToString();
                        break;
                    case win_condition.kill_count:
                        transform.GetChild(i).GetComponent<Text>().text +=
                             $"{WCAP.game_Stats.team_Stats[WCAP.game_Stats.player_Stats[(byte)i].teamID].kill_count} ";
                        transform.GetChild(i).GetComponent<Text>().text += "/" +
                            WCAP.win_Con_Config.int_options[winCon_int_option.kill_limit].ToString();
                        break;
                    case win_condition.assault:
                        transform.GetChild(i).GetComponent<Text>().text +=
                             $"{WCAP.game_Stats.team_Stats[WCAP.game_Stats.player_Stats[(byte)i].teamID].payload_deliveries} ";
                        transform.GetChild(i).GetComponent<Text>().text += "/" +
                            WCAP.win_Con_Config.int_options[winCon_int_option.payload_delivery_limit].ToString();
                        break;
                    case win_condition.regicide:
                        transform.GetChild(i).GetComponent<Text>().text +=
                             WCAP.game_Stats.team_Stats[WCAP.game_Stats.player_Stats[(byte)i].teamID].time_as_king.ToString("N1");
                        transform.GetChild(i).GetComponent<Text>().text += "/" +
                            WCAP.win_Con_Config.float_options[winCon_float_option.time_to_win].ToString();

                        break;
                    case win_condition.king_of_the_hill:
                        transform.GetChild(i).GetComponent<Text>().text +=
                             WCAP.game_Stats.team_Stats[WCAP.game_Stats.player_Stats[(byte)i].teamID].time_in_hill.ToString("N1");
                        transform.GetChild(i).GetComponent<Text>().text += "/" +
                            WCAP.win_Con_Config.float_options[winCon_float_option.time_to_win].ToString();
                        break;
                }
            }
            yield return null;
        }
        
    }

    IEnumerator teams()
    {
        team_ui.SetActive(true);
        yield return new WaitUntil(() => WCAP.game_Stats.team_Stats.Count > 1);
        blue_wax.SetActive(teams_dict[local_id] == 0);
        red_wax.SetActive(teams_dict[local_id] == 1);


        while (true)
        {
            switch (WCAP.win_Con_Config.win_Condition)
            {
                case win_condition.last_survivor:
                    blue.text = $"{WCAP.game_Stats.team_Stats[0].lives_remaining} ";
                    red.text  = $"{WCAP.game_Stats.team_Stats[1].lives_remaining} ";
                    break;
                case win_condition.kill_count:
                    blue.text = $"{WCAP.game_Stats.team_Stats[0].kill_count} ";
                    red.text  = $"{WCAP.game_Stats.team_Stats[1].kill_count} ";
                    break;
                case win_condition.assault:
                    blue.text = $"{WCAP.game_Stats.team_Stats[0].payload_deliveries} ";
                    red.text  = $"{WCAP.game_Stats.team_Stats[1].payload_deliveries} ";
                    break;
                case win_condition.regicide:
                    blue.text = $"{WCAP.game_Stats.team_Stats[0].time_as_king} ";
                    red.text  = $"{WCAP.game_Stats.team_Stats[1].time_as_king} ";
                    break;
                case win_condition.king_of_the_hill:
                    blue.text = $"{WCAP.game_Stats.team_Stats[0].time_in_hill} ";
                    red.text  = $"{WCAP.game_Stats.team_Stats[1].time_in_hill} ";
                    break;
            }
            yield return null;
        }
    }

    // Update is called once per frame
    void Update () {
		
	
    }
}