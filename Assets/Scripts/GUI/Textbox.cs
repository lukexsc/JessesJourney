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

	public GameObject name_obj;
	public Text name_text;
	bool show_name;

	DialogueLine[] dialogue;
	[HideInInspector] public int current_line;

	float type_speed = 0.01f;
	float type_count = 0f;
	int type_place = 1;

	// End Sentence Pause
	const float end_pause = 0.2f;

	void Start ()
	{
		text_box = this;
		DisableTextbox();
	}

	void Update ()
	{
		if (active)
		{
			EnableTextbox();

			if (current_line >= dialogue.Length) DisableTextbox();

			// Text
			string line = dialogue[current_line].line;
			if (type_place < line.Length)
			{
				line = line.Substring(0, type_place);
				if (type_count >= type_speed)
				{
					type_count = 0f;
					string end = dialogue[current_line].line.Substring(type_place, 1);
					if (end == "." || end == "!" || end == "?") type_count -= end_pause;
					type_place++;
				}
				else type_count += Time.deltaTime;
			}
			text_obj.text = line;

			// Advance
			if (!paused)
			{
				if (Input.GetButtonDown("Submit"))
				{
					if (type_place < dialogue[current_line].line.Length-1) 
					{
						type_place = dialogue[current_line].line.Length-1; // if still typing, set to end
					}
					else
					{
						type_place = 1;
						type_count = 0f;
						current_line++;
						text_obj.text = "";

						if (current_line < dialogue.Length) SetNameBox();// if next line exists - set name box
					}
				}
			}
		}
	}

	public void SetNameBox()
	{
		if (dialogue[current_line].speaker == "" && show_name) // if there isn't a speaker - disable name box
		{
			name_obj.SetActive(false);
			show_name = false;
		}
		else if (dialogue[current_line].speaker != name_text.text) // If name is different
		{
			name_obj.SetActive(true);
			name_text.text = dialogue[current_line].speaker;
			show_name = true;
		}
	}

	public void EnableTextbox ()
	{
		box_obj.SetActive(true);
		active = true;
	}

	public void DisableTextbox ()
	{
		box_obj.SetActive(false);
		name_obj.SetActive(true);//false);
		active = false;
		current_line = 0;
		Game.paused = false;
	}

	// Reactivate the textbox and change the text
	public static void CreateTextbox(DialogueLine[] lines, float speed)
	{
		// Set Up Text Box
		text_box.active = true;
		text_box.type_place = 1;
		text_box.type_count = 0f;

		text_box.dialogue = lines;
		text_box.type_speed = speed;

		text_box.name_text.text = lines[0].speaker;
		text_box.show_name = true;
		text_box.SetNameBox();
	}
}

[System.Serializable]
public class DialogueLine
{
	public string line;
	public string speaker;

	DialogueLine (string _speaker, string _line)
	{
		speaker = _speaker;
		line = _line;
	}
}