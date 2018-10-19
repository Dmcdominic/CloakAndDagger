using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(network_id))]
public class sync_behaviour<T> : MonoBehaviour {

    [SerializeField]
    public sync_event in_event;

    [SerializeField]
    public sync_event out_event;

    [SerializeField]
    public int_var local_id;

    [SerializeField]
    float_var t0;

    public bool is_local
    {
        get { return gameObject_id.val == local_id.val; }
    }
    public IValue<int> gameObject_id;

    // Use this for initialization
    public virtual void Start() {
        in_event.e.AddListener(receive_state);
        gameObject_id = GetComponent<network_id>();
    }

    void receive_state(float t, object o, int id)
    {
        if (t > Time.time - t0.val) print($"you got a message from the future! from: {t}, now: {Time.time} ");
        if (id == local_id.val) print($"you got a message you shouldn't have {id}");
        print($"t = {t}");
        if (id == gameObject_id.val)
            rectify(t + t0.val, (T)o);
    }

    public virtual void rectify(float t, T state)
    {
        //override this to rectify differences
    }

    public void send_state(T state)
    { //Call this to send changes
        out_event.Invoke(Time.time - t0.val, (object)state, gameObject_id.val);
    }

    public void send_state_unreliable(T state)
    {
        out_event.Invoke(Time.time, (object)state, gameObject_id.val,reliable: false);
    }

}
