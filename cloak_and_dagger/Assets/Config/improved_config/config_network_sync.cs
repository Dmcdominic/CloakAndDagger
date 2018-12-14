using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*	
 *	config_network_sync:
 *	An interface to sync config_obj SO's over the network.
 *	Created by Dominic Calkosz.
 */
public class config_network_sync : MonoBehaviour {

	public config_obj config_to_sync;

	// When this is invoked, the local config will be sent to,
	// and override, that of all other connected clients.
	public event_object sync_config_event;

	// The network events used to send and receive the config.
	public sync_event out_event;
	public sync_event in_event;


	// Initialization
	private void Awake() {
		if (in_event) {
			in_event.e.AddListener(sync_incoming_config);
		}
		if (sync_config_event) {
			sync_config_event.e.AddListener(send_full_config);
		}
	}

	// This method accepts a single incoming config key-value pair,
	// and updates the config SO accordingly.
	private void sync_incoming_config(float t, object obj, int i) {
		int value_type_index = i;
		config_value_type value_type = (config_value_type)value_type_index;

		config_syncable_entry entry = (config_syncable_entry)obj;

		switch(value_type) {
			case config_value_type.Bool:
				config_to_sync.bool_options[entry.key] = (bool)entry.value;
				break;
			case config_value_type.Float:
				config_to_sync.float_options[entry.key] = (float)entry.value;
				break;
			case config_value_type.Int:
				config_to_sync.int_options[entry.key] = (int)entry.value;
				break;
			case config_value_type.String:
				config_to_sync.string_options[entry.key] = (string)entry.value;
				break;
			default:
				Debug.LogError("Unknown config value type received from incoming config sync message");
				break;
		}
	}

	// This method will send all config values over the network to be synced.
	private void send_full_config() {
		// Sync bool options
		foreach (string key in config_to_sync.bool_options.Keys) {
			config_syncable_entry new_entry = new config_syncable_entry(key, config_to_sync.bool_options[key]);
			out_event.Invoke(0, new_entry, (int)config_value_type.Bool);
		}

		// Sync float options
		foreach (string key in config_to_sync.float_options.Keys) {
			config_syncable_entry new_entry = new config_syncable_entry(key, config_to_sync.float_options[key]);
			out_event.Invoke(0, new_entry, (int)config_value_type.Float);
		}

		// Sync int options
		foreach (string key in config_to_sync.int_options.Keys) {
			config_syncable_entry new_entry = new config_syncable_entry(key, config_to_sync.int_options[key]);
			out_event.Invoke(0, new_entry, (int)config_value_type.Int);
		}

		// Sync string options
		foreach (string key in config_to_sync.string_options.Keys) {
			config_syncable_entry new_entry = new config_syncable_entry(key, config_to_sync.string_options[key]);
			out_event.Invoke(0, new_entry, (int)config_value_type.String);
		}
	}
}

// This is a serializable struct used to store and send the key-value pair for every config option.
[System.Serializable]
public struct config_syncable_entry {
	public string key;
	public object value;
	public config_syncable_entry(string _key, object _value) {
		key = _key;
		value = _value;
	}
}
