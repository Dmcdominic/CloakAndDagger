using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "config/dagger")]
public class dagger_config : ScriptableObject {

	[SerializeField]
	private float _cooldown;
	public float cooldown {
		get { return _cooldown; }
	}

	[SerializeField]
	private float _stun_time;
	public float stun_time {
		get { return _stun_time; }
	}

}
