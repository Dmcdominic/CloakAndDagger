using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class custom_network_behavior : NetworkBehaviour {

	//// ================== Server only event calling ==================

	//// Call one of these within your custom_network_behavior in order to invoke an event on all clients IFF the custom_network_behavior isServer is true
	//protected void server_only_event_call(UnityEvent e) {
	//	if (isServer) {
	//		Rpc_trigger_event_for_all_clients(e);
	//	}
	//}
	//protected void server_only_event_call<T>(UnityEvent<T> e, T arg) {
	//	if (isServer) {
	//		Rpc_trigger_event_for_all_clients<T>(e, arg);
	//	}
	//}
	//protected void server_only_event_call<T0, T1>(UnityEvent<T0, T1> e, T0 arg0, T1 arg1) {
	//	if (isServer) {
	//		Rpc_trigger_event_for_all_clients<T0, T1>(e, arg0, arg1);
	//	}
	//}

	//// These are the Rpc's used by the server event methods above to invoke the event on every client
	//[ClientRpc]
	//private void Rpc_trigger_event_for_all_clients(UnityEvent e) {
	//	e.Invoke();
	//}
	//[ClientRpc]
	//private void Rpc_trigger_event_for_all_clients<T>(UnityEvent<T> e, T arg) {
	//	e.Invoke(arg);
	//}
	//[ClientRpc]
	//private void Rpc_trigger_event_for_all_clients<T0, T1>(UnityEvent<T0, T1> e, T0 arg0, T1 arg1) {
	//	e.Invoke(arg0, arg1);
	//}

}
