using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class player_var<T> : ScriptableObject, IEnumerable<int>
{
    void Awake()
    {
        D = new Dictionary<int, T>();
    }
    private Dictionary<int, T> D;

    public T this[int i]
    {
        get
        {
            if (D == null) D = new Dictionary<int, T>();
            if (!D.ContainsKey(i)) D[i] = default(T);
            return D[i];
        }
        set
        {
            if (D == null) D = new Dictionary<int, T>();
            D[i] = value;
        }
    }

    public T loc
    {
        get { return this[data.local_id]; }
        set { this[data.local_id] = value; }
    }

    public IEnumerator<int> GetEnumerator()
    {
        if (D == null) D = new Dictionary<int, T>();
        return D.Keys.GetEnumerator();
    }

    public int Count
    {
        
        get { if (D == null) D = new Dictionary<int, T>(); return D.Keys.Count; }
    }

    public T mine(int i)
    {

        if (D == null) D = new Dictionary<int, T>();
        if (!D.ContainsKey(i)) D[i] = CreateInstance<gen_var<T>>();
        return D[i];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
