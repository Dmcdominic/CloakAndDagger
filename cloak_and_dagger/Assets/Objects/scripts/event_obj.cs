using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

[CreateAssetMenu(menuName = "variables/event")]
public class event_object : ScriptableObject
{

    public UnityEvent e = new UnityEvent();

    public void Invoke() { e.Invoke(); }

}



[CreateAssetMenu(menuName = "variables/generic event")]
public class gen_event<T> : ScriptableObject
{
    public UnityEvent<T> e;

    public void Invoke(T arg) { e.Invoke(arg); }
}



public class adhoc_event<T> : UnityEvent<T> { }

[CreateAssetMenu(menuName = "variables/object event")]
public class obj_event : ScriptableObject
{
    public UnityEvent<object> e = new adhoc_event<object>();

    public void Invoke(object o) { e.Invoke(o); }
}