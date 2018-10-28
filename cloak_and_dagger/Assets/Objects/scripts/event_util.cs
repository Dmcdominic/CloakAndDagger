using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;


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
