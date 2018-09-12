using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(menuName = "variables/NetworkConnection")]
public class NetworkConnection_var : ScriptableObject {

	[SerializeField]
	bool use_constant = false;

	[SerializeField]
	NetworkConnection constant = new NetworkConnection();

	[SerializeField]
	private NetworkConnection value;

	public NetworkConnection val
	{
		get {return use_constant ? constant : value;}
		set {this.value = value;}
	}
}
