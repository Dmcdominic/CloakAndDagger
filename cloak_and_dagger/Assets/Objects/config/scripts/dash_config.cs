using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "config/dash")]
public class dash_config : ScriptableObject {

	[SerializeField]
	private float _cooldown;
	public float cooldown {
		get { return _cooldown; }
	}

	[SerializeField]
	private float _distance;
	public float distance {
		get { return _distance; }
	}

}
