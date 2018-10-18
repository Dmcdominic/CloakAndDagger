using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class adhoc_event<T1,T2,T3> : UnityEvent<T1,T2,T3> { }

[CreateAssetMenu(menuName = "variables/sync_event")]
public class sync_event : ScriptableObject
{

    public UnityEvent<float,object,int> e = new adhoc_event<float,object,int>();

    public void Invoke(float t,object o,int id) { e.Invoke(t,o,id); }

}

