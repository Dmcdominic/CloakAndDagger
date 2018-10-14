using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum mtc_Type : byte { sync_data, sync_event}

public struct event_data_union
{
    public mtc_Type type;
    public object body;
}


public class splitter : MonoBehaviour {
    [SerializeField]
    obj_event message_in;

    [SerializeField]
    obj_event out_event;

    [SerializeField]
    obj_event out_data;

	// Use this for initialization
	void Start () {
        message_in.e.AddListener(split);
	}
	
    void split(object obj_in)
    {
        event_data_union edu = (event_data_union)obj_in;
        switch (edu.type)
        {
            case mtc_Type.sync_data:
                out_data.Invoke(edu.body);
                break;
            case mtc_Type.sync_event:
                out_event.Invoke(edu.body);
                break;
        }
    }

}
