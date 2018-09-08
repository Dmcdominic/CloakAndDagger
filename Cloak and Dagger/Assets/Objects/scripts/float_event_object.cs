using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[CreateAssetMenu(menuName = "variables/float_event")]
public class float_event_object : ScriptableObject {
	[SerializeField]
	float constant = 0;

	public class float_event : UnityEvent<float> {}

	public UnityEvent<float> e = new float_event();

	public void Invoke(float d) {e.Invoke(d);}

	public void Invoke() {e.Invoke(constant);}

}

