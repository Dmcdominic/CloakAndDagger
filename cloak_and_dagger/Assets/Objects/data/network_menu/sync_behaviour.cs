using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(IValue<int>))]
public class sync_behaviour<T> : MonoBehaviour {

    [SerializeField]
    public sync_event in_event;

    [SerializeField]
    public sync_event out_event;

    [SerializeField]
    public int_var local_id;

    public IValue<int> gameObject_id;

	// Use this for initialization
	public virtual void Start () {
        in_event.e.AddListener((t,o,id) => receive_state(t,o,id));
        gameObject_id = (IValue<int>)GetComponent(typeof(IValue<int>));
	}

    void receive_state(float t, object o, int id) 
    {
        if (t > Time.time) print($"you got a message from the future! from: {t}, now: {Time.time} ");
        if (id == local_id.val) print($"you got a message you shouldn't have {id}");
        if (id == gameObject_id.val)
            rectify(t, (T)o);
    }

    public virtual void rectify(float t, T state)
    {
        //override this to rectify differences
    }

    public void send_state(T state)
    { //Call this to send changes
        out_event.Invoke(Time.time,(object)state,gameObject_id.val);
    }

    public bool is_local()
    {
        return (local_id.val == gameObject_id.val);
    }
}
