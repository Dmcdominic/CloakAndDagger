using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "variables/vec2")]
public class Vec2Var : ScriptableObject, IValue<Vector2> {

	[SerializeField]
	bool use_constant = false;

	[SerializeField]
	Vector2 constant = Vector2.zero;

	[SerializeField]
	private Vector2 value;

	public Vector2 val
	{
		get {return use_constant ? constant : value;}
		set {this.value = value;}
	}

}
