using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{
	[HideInInspector] public bool active; // if the input field is active
	[HideInInspector] public static TextInput text_input; // static reference to this object
	[HideInInspector] public bool correct; // if correct password entered
	public InputField input_field;
	public GameObject input_obj;
	public bool stop_player;
	public AudioClip fail_effect;
	public AudioClip win_effect;

	string password;

	void Start ()
	{
		text_input = this;
		DisableTextInput();
	}

	// Sets up the input field and pauses the player
	public void EnableTextInput ()
	{
		input_obj.SetActive(true); // Activate input field
		if (stop_player) Game.paused = true; // pause player
		active = true;
		input_field.ActivateInputField();
	}

	// Disables the input field and frees the player
	public static void DisableTextInput ()
	{
		Game.paused = false; // unpause
		text_input.input_field.text = ""; // clear input field
		text_input.correct = false; // set as not correct
		text_input.active = false; // set as not active
		text_input.input_obj.SetActive(false); // Deactivate input object
	}

	// Sets up the text input with password
	public static void SetTextInput(string pass_word, bool pause_player)
	{
		text_input.EnableTextInput();
		text_input.password = pass_word;
		text_input.stop_player = pause_player;
	}

	// Checks if the player entered the password
	public void CheckInput()
	{
		correct = (input_field.text == password);
		input_field.text = ""; // clear input field
		input_field.ActivateInputField(); // reactivate field

		// Sound Effect
		if (correct) SoundController.instance.PlayEffect(win_effect);
		else SoundController.instance.PlayEffect(fail_effect);
	}
}
