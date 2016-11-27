using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Textbox : MonoBehaviour
{
	[HideInInspector] public bool active;
	[HideInInspector] public bool paused;
	[HideInInspector] public static Textbox text_box;
	public GameObject box_obj;
	public Text text_obj;
	public bool stop_player;

	string[] dialogue;
	int current_line;

	float type_speed = 0.01f;
	float type_count = 0f;
	int type_place = 1; 

	void Start ()
	{
		text_box = this;
		DisableTextbox();
	}

	void Update ()
	{
		if (active && !paused)
		{
			EnableTextbox();
			if (current_line < dialogue.Length) // if writing line
			{
				string line =  dialogue[current_line];
				if (type_place < line.Length)
				{
					line = line.Substring(0, type_place);
					if (type_count >= type_speed)
					{
						type_count = 0f;
						type_place++;
					}
					else type_count += Time.deltaTime;
				}
				text_obj.text = line;//dialogue[current_line];
			}
			else DisableTextbox();

			if (Input.GetKeyDown(KeyCode.E))
			{
				if (type_place < dialogue[current_line].Length-1) 
				{
					type_place = dialogue[current_line].Length-1; // if still typing, set to end
				}
				else
				{
					type_place = 1;
					type_count = 0f;
					current_line++;
				}
			}
		}
	}

	public void EnableTextbox ()
	{
		box_obj.SetActive(true);
		if (stop_player) Game.paused = true;
		active = true;
	}

	public void DisableTextbox ()
	{
		box_obj.SetActive(false);
		Game.paused = false;
		active = false;
	}

	// Reactivate the textbox and change the text
	public static void CreateTextbox(string[] lines, bool pause_player, float speed)
	{
		//text_box.active = true;
		text_box.EnableTextbox();
		text_box.current_line = 0;
		text_box.type_place = 1;
		text_box.type_count = 0f;

		text_box.dialogue = lines;
		text_box.stop_player = pause_player;
		text_box.type_speed = speed;
	}
}
