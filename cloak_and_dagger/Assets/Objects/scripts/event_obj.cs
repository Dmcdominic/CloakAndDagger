using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

[CreateAssetMenu(menuName = "events/unit")]
public class event_object : ScriptableObject
{

    private UnityEvent _e;

    public UnityEvent e
    {
        get { if (_e == null) _e = new UnityEvent();  return _e; }
    }

    public void Invoke() { e.Invoke(); }

}



public class gen_event<T> : ScriptableObject
{

    private UnityEvent<T> _e;

    public UnityEvent<T> e
    {
        get { if (_e == null) _e = new adhoc_event<T>(); return _e; }
    }

    public void Invoke(T arg) { e.Invoke(arg); }

}



public class adhoc_event<T> : UnityEvent<T> { }

