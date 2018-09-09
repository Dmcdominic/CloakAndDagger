using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "variables/event")]
public class event_object : ScriptableObject {

	public UnityEvent e;

	public void Invoke() {e.Invoke();}

}
