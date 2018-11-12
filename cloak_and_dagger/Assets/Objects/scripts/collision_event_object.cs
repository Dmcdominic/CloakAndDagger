using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
[CreateAssetMenu(menuName = "events/collision")]
public class collision_event_object : ScriptableObject {

	[System.Serializable]
	public class collision_event : UnityEvent<GameObject, Collision2D> { };

	[SerializeField]
	public collision_event e = new collision_event();

	public void Invoke(GameObject gameObject, Collision2D collision) {e.Invoke(gameObject, collision);}

}
