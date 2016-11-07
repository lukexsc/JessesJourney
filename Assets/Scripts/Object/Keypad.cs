using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Interactive))]
public class Keypad : MonoBehaviour
{
	public string password; // 4 number password
	public Interactive activator; // activate the keypad

	Interactive inter; // interactive in the keypad

	void Start ()
	{
		inter = GetComponent<Interactive>();
		//inter.ignore_player = true;
	}

	void Update ()
	{
		if (TextInput.text_input.active) // If TextInput is up
		{
			if (TextInput.text_input.correct) // if correct password
			{
				inter.SetActive(true);
				TextInput.DisableTextInput();
			}
		}
		else
		{
			if (activator.active) // Active Keypad - open keypad
			{
				activator.SetActive(false);
				inter.SetActive(false);
				TextInput.SetTextInput(password, true);
			}
		}
	}
}
