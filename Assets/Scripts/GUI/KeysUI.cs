using UnityEngine;
using System.Collections;

public class KeysUI : MonoBehaviour
{
	[HideInInspector] public static KeysUI keys_ui;
	public GameObject[] key_obj;
	public int keys;

	void Start ()
	{
		keys_ui = this;
		ShowKeys();
	}

	// Adds a key to the GUI
	public static void AddKey()
	{
		keys_ui.keys++;
		keys_ui.ShowKeys();
	}

	// Returns the number of keys collected
	public static int GetKeys()
	{
		return keys_ui.keys;
	}

	// Activate and Deactivate keys in GUI based on keys collected
	public void ShowKeys()
	{
		for (int i=0; i<key_obj.Length; i++) // cycle through all keys
		{
			key_obj[i].SetActive((i < keys)); // if have the key
		}
	}
}
