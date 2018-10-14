using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "variables/event")]
public class event_object : ScriptableObject
{

    public UnityEvent e;

    public void Invoke() { e.Invoke(); }

}


[CreateAssetMenu(menuName = "variables/generic event")]
public class gen_event<T> : ScriptableObject
{
    public UnityEvent<T> e;

    public void Invoke(T arg) { e.Invoke(arg); }
}

[CreateAssetMenu(menuName = "variables/object event")]
public class obj_event : ScriptableObject
{
    public UnityEvent<object> e;

    public void Invoke(object o) { e.Invoke(o); }
}