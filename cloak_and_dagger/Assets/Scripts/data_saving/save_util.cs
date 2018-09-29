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
		return Application.persistentDataPath + "/";
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
	public static bool try_load_from<T>(string subpath, string file_name, out T data) {
		object generic_data = null;

		string directory_path = get_full_path(subpath, "");
		if (!Directory.Exists(directory_path)) {
			Directory.CreateDirectory(directory_path);
		}

		string path = get_full_path(subpath, file_name);
		if (File.Exists(path)) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(path, FileMode.Open);

			generic_data = bf.Deserialize(file);
			file.Close();
		} else {
			Debug.LogError("No file: " + path + " found.");
			data = default(T);
			return false;
		}

		if (generic_data is T) {
			data = (T)generic_data;
			return true;
		} else {
			Debug.LogError("Object saved at path: " + path + " is not of the expected type.");
			data = default(T);
			return false;
		}
	}

	// Get a list of all the files that exist in the root of a particular directory
	public static List<string> get_files_in_dir(string subpath, bool full_file_paths = false) {
		string directory_path = get_full_path(subpath, "");
		Debug.Log("directory_path: " + directory_path);
		if (!Directory.Exists(directory_path)) {
			Directory.CreateDirectory(directory_path);
		}

		string[] file_names = Directory.GetFiles(directory_path);
		foreach (string file in file_names) {
			Debug.Log("File: " + file);
		}

		List<string> files_list = new List<string>(file_names);

		if (!full_file_paths) {
			for (int i=0; i < files_list.Count; i++) {
				files_list[i] = Path.GetFileName(files_list[i]);
			}
		}
		
		return files_list;
	}

	// Check if a certain file exists in the persistent data path
	public static bool file_exists(string subpath, string file_name) {
		string path = get_full_path(subpath, file_name);
		return (File.Exists(path));
	}

	// Generates the full path from persistent data path, subpath, and file_name (which gets sanitized)
	private static string get_full_path(string subpath, string file_name) {
		string directory_path = Path.Combine(generalPath(), subpath);
		return Path.Combine(directory_path, sanitize_file_name(file_name));
	}

	// Util function to make sure that a file name is valid.
	private static string sanitize_file_name(string name) {
		var invalids = System.IO.Path.GetInvalidFileNameChars();
		return String.Join("_", name.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd('.');
	}

}
