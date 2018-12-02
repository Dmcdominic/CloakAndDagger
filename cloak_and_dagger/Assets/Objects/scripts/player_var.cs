﻿using System.Collections;
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

    public void init(Dictionary<int,T> ND)
    {
        Debug.Log("initing dict");
        D = ND;
    }

    public T this[int i]
    {
        get
        {
            if (D == null) D = new Dictionary<int, T>();
            if (!D.ContainsKey(i)) { D[i] = default(T); Debug.Log($"Key Not Found. Key  {i}"); }
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

    public bool Contains(int id)
    {
        if (D != null)
            return D.ContainsKey(id);
        else
            D = new Dictionary<int, T>();
        return false;
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
