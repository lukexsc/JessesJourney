using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Textbox : MonoBehaviour
{
	[HideInInspector] public bool active;
	[HideInInspector] public static Textbox text_box;
	public GameObject box;
	public Text text;
	public bool stop_player;

	PlayerController player;
	string[] dialogue;
	int current_line;

	void Start ()
	{
		text_box = this;
		player = FindObjectOfType<PlayerController>();
	}

	void Update ()
	{
		if (active)
		{				
			EnableTextbox();
			if (current_line < dialogue.Length) text.text = dialogue[current_line];
			else DisableTextbox();

			if (Input.GetKeyDown(KeyCode.Space)) current_line++;
		}
		else DisableTextbox();
	}

	public void EnableTextbox ()
	{
		box.SetActive(true);
		if (stop_player) player.paused = true;
	}

	public void DisableTextbox ()
	{
		box.SetActive(false);
		player.paused = false;
	}

	// Reactivate the textbox and change the text
	public static void CreateTextbox(string[] lines, bool pause_player)
	{
		text_box.active = true;
		text_box.current_line = 0;
		text_box.dialogue = lines;
		text_box.stop_player = pause_player;
	}
}
