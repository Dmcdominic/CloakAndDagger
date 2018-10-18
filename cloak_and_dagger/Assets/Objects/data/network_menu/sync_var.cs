using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class sync_var<T> {


    public bool read_only;

    public bool dirty = false;

    public IValue<T> variable;

    public int id;

    public bool news_to_me = false;

    public sync_var(bool read_only)
    {
        this.read_only = read_only;
    }

    public T update(T cur)
    {
        if(news_to_me)
        {
            news_to_me = false;
            return val;
        }
        else
        {
            return cur;
        }
    }

    public T val
    {
        get { return variable.val; }
        set { if (!read_only) { dirty = true; variable.val = value; }
          //  else Debug.Log($"tried to change {id} sync_var but it was read_only");
        }
    }

}
