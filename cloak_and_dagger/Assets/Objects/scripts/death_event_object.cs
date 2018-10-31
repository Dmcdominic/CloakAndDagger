using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



[System.Serializable]
//[CreateAssetMenu(menuName = "events/death_event")]
public class death_event_object : ScriptableObject {
    [System.Serializable]
    public class death_event : UnityEvent<death_event_data> { };

    [SerializeField]
    public death_event e = new death_event();

    public void Invoke(death_event_data death_Event_Data) {
        e.Invoke(death_Event_Data);
    }
}
