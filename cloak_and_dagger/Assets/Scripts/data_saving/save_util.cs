using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public static class save_util {

	// Path settings
	public static string generalPath() {
		return Application.persistentDataPath;
	}

	// Save a data object to the persistent data path at file_name
	public static void save_to<T>(string subpath, string file_name, T data) {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file;

		string path = get_full_path(subpath, file_name);
		if (!File.Exists(path)) {
			file = File.Create(path);
		} else {
			file = File.Open(path, FileMode.Open);
		}

		bf.Serialize(file, data);
		file.Close();
	}

	// Load a data object from the persistent data path at file_name
	public static object load_from<T>(string subpath, string file_name) {
		object data = null;
		string path = get_full_path(subpath, file_name);
		if (File.Exists(path)) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(path, FileMode.Open);

			data = bf.Deserialize(file);
			file.Close();
		} else {
			Debug.LogError("No file: " + path + " found.");
			return null;
		}

		if (data is T) {
			return (T)data;
		} else {
			Debug.LogError("Object saved at path: " + path + " is not of the expected type.");
			return null;
		}
	}

	// Check if a certain file exists in the persistent data path
	public static bool file_exists(string subpath, string file_name) {
		string path = get_full_path(subpath, file_name);
		return (File.Exists(path));
	}

	// Generates the full path from persistent data path, subpath, and file_name (which gets sanitized)
	private static string get_full_path(string subpath, string file_name) {
		return generalPath() + subpath + sanitize_file_name(file_name);
	}

	// Util function to make sure that a file name is valid.
	private static string sanitize_file_name(string name) {
		var invalids = System.IO.Path.GetInvalidFileNameChars();
		return String.Join("_", name.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd('.');
	}

}
