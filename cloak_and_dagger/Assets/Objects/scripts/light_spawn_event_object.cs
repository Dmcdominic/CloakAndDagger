using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
//[CreateAssetMenu(menuName = "events/light_spawn_event")]
public class light_spawn_event_object : ScriptableObject {
	[System.Serializable]
	public class light_spawn_event : UnityEvent<light_spawn_data> { };

	[SerializeField]
	public light_spawn_event e = new light_spawn_event();

	public void Invoke(light_spawn_data light_Spawn_Data) {
		e.Invoke(light_Spawn_Data);
	}
}
