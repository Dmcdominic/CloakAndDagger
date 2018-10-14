using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public struct player_sync_data
{
    public sync_var<Vector2> position;
    public sync_var<Vector2> velocity;
}

public class init_game : MonoBehaviour {
    [SerializeField]
    obj_event init_sync_state; //invoke with sync_state struct

    [SerializeField]
    string_var my_name;

    [SerializeField]
    party_var party_info;

    [SerializeField]
    event_object start;

    [SerializeField]
    vec2_list spawn_points;

    [SerializeField]
    GameObject player_prefab;

    [SerializeField]
    scene_var game_scene;


	// Use this for initialization
	void Start () {
        start.e.AddListener(init);
	}
	
    void init()
    {

        SceneManager.LoadScene(game_scene.val.name);


        sync_state ss = new sync_state();
        ss.int_vars = new Dictionary<int, sync_var<int>>();
        ss.float_vars = new Dictionary<int, sync_var<float>>();
        ss.vec2_vars = new Dictionary<int, sync_var<Vector2>>();
        ss.string_vars = new Dictionary<int, sync_var<string>>();

        List<player_sync_data> psds = new List<player_sync_data>();

        player_sync_data leader_psd = new player_sync_data();
        leader_psd.position.variable = new Vec2Var(); //these bad boys stay in memory until end_game
        leader_psd.velocity.variable = new Vec2Var();
        leader_psd.position.val = spawn_points.next;
        leader_psd.velocity.val = Vector2.zero;

        GameObject leader = Instantiate(player_prefab,leader_psd.position.val,Quaternion.identity);
        leader.GetComponent<pmove>().psd = leader_psd;

        psds.Add(leader_psd);
        player_sync_data psd;
        foreach (string member_name in party_info.val.members)
        {
            psd = new player_sync_data();
            psd.position.variable = new Vec2Var();
            psd.velocity.variable = new Vec2Var();
            psd.position.val = spawn_points.next;
            psd.velocity.val = Vector2.zero;
            GameObject player = Instantiate(player_prefab, psd.position.val, Quaternion.identity);
            player.GetComponent<pmove>().psd = psd;
            psds.Add(psd);
        }

        
        for(int i = 0; i < psds.Count; i++)
        {
            ss.vec2_vars[2 * i]     = psds[i].position;
            ss.vec2_vars[2 * i + 1] = psds[i].velocity;
        }

        init_sync_state.Invoke(ss); //right now we only update postion and velocity

    }
}
