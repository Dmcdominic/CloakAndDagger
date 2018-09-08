using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "variables/float")]
public class float_var : ScriptableObject {

	[SerializeField]
	bool use_constant = false;

	[SerializeField]
	float constant = 0;

	[SerializeField]
	private float value;

	public float val
	{
		get {return use_constant ? constant : value;}
		set {this.value = value;}
	}
}
