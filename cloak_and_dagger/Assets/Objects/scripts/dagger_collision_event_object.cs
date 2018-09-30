using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
[CreateAssetMenu(menuName = "variables/dagger_collision_event")]
public class dagger_collision_event_object : ScriptableObject {

	[System.Serializable]
	public class dagger_collision_event : UnityEvent<GameObject, dagger_data, GameObject, string> { };

	[SerializeField]
	public dagger_collision_event e = new dagger_collision_event();

	public void Invoke(GameObject dagger, dagger_data dagger_Data,
                       GameObject collided_with, string tag) {
        e.Invoke(dagger, dagger_Data, collided_with, tag);
    }
}
