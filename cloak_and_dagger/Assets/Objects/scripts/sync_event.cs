using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class adhoc_event<T1,T2,T3> : UnityEvent<T1,T2,T3> { }

[CreateAssetMenu(menuName = "events/sync_event")]
public class sync_event : ScriptableObject
{

    public UnityEvent<float,object,int> e = new adhoc_event<float,object,int>();
    public UnityEvent<float, object, int> r = new adhoc_event<float,object,int>();

    public void Invoke(float t,object o,int id,bool reliable = true)
    {
        if (reliable) e.Invoke(t, o, id);
        else r.Invoke(t, o, id);
    }

}

