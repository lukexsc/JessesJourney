using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{
	[HideInInspector] public bool active;
	[HideInInspector] public static TextInput text_input;
	[HideInInspector] public bool correct;
	public InputField input_field;
	public GameObject input_obj;
	public bool stop_player;

	string password;

	void Start ()
	{
		text_input = this;
		DisableTextInput();
	}

	public void EnableTextInput ()
	{
		input_obj.SetActive(true);
		if (stop_player) Game.paused = true;
		active = true;
		input_field.ActivateInputField();
	}

	public static void DisableTextInput ()
	{
		text_input.input_obj.SetActive(false);
		Game.paused = false;
		text_input.active = false;
		text_input.input_field.text = "";
	}

	public static void SetTextInput(string pass_word, bool pause_player)
	{
		text_input.EnableTextInput();
		text_input.password = pass_word;
		text_input.stop_player = pause_player;
	}

	public void CheckInput()
	{
		correct = (input_field.text == password);
		if (!correct)
		{
			input_field.text = "";
			input_field.ActivateInputField();
		}
	}
}
