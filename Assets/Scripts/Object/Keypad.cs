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
	}

	void Update ()
	{
		if (!inter.active) // If not completed
		{
			if (TextInput.text_input.active) // If TextInput is up
			{
				if (TextInput.text_input.correct) // if correct password
				{
					inter.SetActive(true); // activate self
					TextInput.DisableTextInput(); // turn off text input
				}
				else inter.SetActive(false); // Deactivate self

				// Cancel Text Input
				if (Input.GetButtonDown("Cancel")) 
				{
					TextInput.DisableTextInput(); // turn off text input
					inter.SetActive(false); // Deactivate self
				}
			}
			else
			{
				if (activator.active) // Active Keypad - open keypad
				{
					activator.SetActive(false); // Deactivate activator
					TextInput.SetTextInput(password, true); // Set Up password
				}
			}
		}
	}
}
