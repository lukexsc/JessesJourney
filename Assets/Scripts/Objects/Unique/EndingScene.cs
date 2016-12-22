using UnityEngine;
using System.Collections;

public class EndingScene : MonoBehaviour
{
	public Interactive chest;
	public GameObject hidden_area;
	public DialogueLine[] chest_dialogue;
	public DialogueLine[] end_dialogue;

	int scene_stage = 0;
	float timer = 0f;

	void Update ()
	{
		if (scene_stage == 0) // Wait to open chest
		{
			if (chest.active) // open the chest - start scene
			{
				Game.paused = true;
				scene_stage = 1;
			}
		}
		else if (scene_stage == 1) // Open chest and look inside
		{
			Textbox.CreateTextbox(chest_dialogue, 0.005f);
			scene_stage = 2;
		}
		else if (scene_stage == 2) // Reveal hidden area
		{
			if (!Textbox.text_box.active)
			{
				hidden_area.SetActive(true);
				scene_stage = 3;
				timer = 0f;
			}
		}
		else if (scene_stage == 3)
		{
			if (timer >= 1f)
			{
				Textbox.CreateTextbox(end_dialogue, 0.005f);
				scene_stage = 4;
				timer = 0f;
			}
			else timer += Time.deltaTime;
		}
		else if (scene_stage == 4)
		{
			if (!Textbox.text_box.active)
			{
				Fade.instance.LoadScene("Credits");
				Game.spoke_to_bartender = false;
				Game.pushed_statue = false;
				Game.stage = 0;
				Game.puzzle = "";
				scene_stage = 99;
			}
		}
	}
}
