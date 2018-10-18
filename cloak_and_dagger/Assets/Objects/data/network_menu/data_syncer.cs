using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[System.Serializable]
public struct data_sync_message 
{
    public Dictionary<int, int> int_vars;
    public Dictionary<int, float> float_vars;
    public Dictionary<int, Vector2> vec2_vars;
    public Dictionary<int, string> string_vars;
}

[System.Serializable]
public struct sync_state
{
    public Dictionary<int, sync_var<int>> int_vars;
    public Dictionary<int, sync_var<float>> float_vars;
    public Dictionary<int, sync_var<Vector2>> vec2_vars;
    public Dictionary<int, sync_var<string>> string_vars;
}

public class data_syncer : MonoBehaviour
{ //obsolete

    sync_state ss;

    [SerializeField]
    obj_event set_ss;

    [SerializeField]
    obj_event out_multicast; //send a message_package out here

    [SerializeField]
    obj_event in_data;

    [SerializeField]
    event_object done_initializing;

    float sync_rate = 20;

    // Use this for initialization
    void Start () {
        done_initializing.e.AddListener(() => StartCoroutine(send_message()));
        set_ss.e.AddListener(init);
        in_data.e.AddListener(update_data);
	}

    void init(object in_state)
    {
        ss = (sync_state)in_state;
    }

    void update_data(object msg)
    {
        data_sync_message dsm = (data_sync_message)msg;
        foreach(int id in dsm.int_vars.Keys)
        {
            ss.int_vars[id].val = dsm.int_vars[id];
            ss.int_vars[id].dirty = false;
            ss.int_vars[id].news_to_me = true;
        }
        foreach (int id in dsm.float_vars.Keys)
        {
            ss.float_vars[id].val = dsm.float_vars[id];
            ss.float_vars[id].dirty = false;
            ss.float_vars[id].news_to_me = true;
        }
        foreach (int id in dsm.vec2_vars.Keys)
        {
            ss.vec2_vars[id].val = dsm.vec2_vars[id];
            ss.vec2_vars[id].dirty = false;
            ss.vec2_vars[id].news_to_me = true;
        }
        foreach (int id in dsm.string_vars.Keys)
        {
            ss.string_vars[id].val = dsm.string_vars[id];
            ss.string_vars[id].dirty = false;
            ss.string_vars[id].news_to_me = true;
        }

    }


    IEnumerator send_message()
    {
        data_sync_message msg = new data_sync_message();
        Message_package msg_p = new Message_package();
        mtc_data edu = new mtc_data();
        msg_p.type = Custom_msg_type.MTC;
        //edu.type = mtc_Type.sync_data;
        while(true)
        {
            yield return new WaitForSeconds(1/sync_rate);
            msg = new data_sync_message();
            msg.int_vars = ss.int_vars.Where(p => p.Value.dirty).ToDictionary(p => p.Key, p => p.Value.val);
            msg.float_vars = ss.float_vars.Where(p => p.Value.dirty).ToDictionary(p => p.Key, p => p.Value.val);
            msg.vec2_vars = ss.vec2_vars.Where(p => p.Value.dirty).ToDictionary(p => p.Key, p => p.Value.val);
            msg.string_vars = ss.string_vars.Where(p => p.Value.dirty).ToDictionary(p => p.Key, p => p.Value.val);
            edu.body = msg;
            msg_p.message = edu;
            out_multicast.Invoke(msg_p);
            foreach(int id in ss.int_vars.Keys)
            {
                ss.int_vars[id].dirty = false;
            }
            foreach (int id in ss.float_vars.Keys)
            {
                ss.float_vars[id].dirty = false;
            }
            foreach (int id in ss.vec2_vars.Keys)
            {
                ss.vec2_vars[id].dirty = false;
            }
            foreach (int id in ss.string_vars.Keys)
            {
                ss.string_vars[id].dirty = false;
            }

        }
    }


}
