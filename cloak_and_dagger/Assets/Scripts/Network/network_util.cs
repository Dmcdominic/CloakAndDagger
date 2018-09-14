using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class network_util {

	public static bool is_same_netId(GameObject obj1, GameObject obj2) {
		NetworkIdentity NI1 = obj1.GetComponent<NetworkIdentity>();
		NetworkIdentity NI2 = obj2.GetComponent<NetworkIdentity>();
		return NI1 && NI2 && (NI1.netId == NI2.netId);
	}

}
