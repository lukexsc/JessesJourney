using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SpriteRenderer))]
public class Bartender : Interactive
{
	public DialogueLine[] intro;
	public DialogueLine[] basement;
	public DialogueLine[] ruins;
	public DialogueLine[] lever_puzzle;
	public DialogueLine[] button_puzzle;
	public DialogueLine[] path_puzzle;

	public DialogueLine[] post_intro;
	public DialogueLine[] self;
	public DialogueLine[] tavern;
	public DialogueLine[] gossip;

	public float delay;
	float delay_counter;
	bool talk;
	bool talked_to;

	AudioSource source;

	void Start ()
	{
		source = GetComponent<AudioSource>();
	}

	public override void Update ()
	{
		if (active)
		{
			Game.paused = true;
			talk = true;
			delay_counter = 0f;
			//SoundController.instance.PlayEffect(talk_effect);
			source.Play();
		}

		if (talk)
		{
			if (delay_counter >= delay)
			{
				if (Game.stage == 0)
				{
					if (!Game.spoke_to_bartender) Textbox.CreateTextbox(intro, 0.005f);
					else Textbox.CreateTextbox(post_intro, 0.005f);
				}
				else if (Game.stage == 1)
				{
					if (!talked_to) Textbox.CreateTextbox(basement, 0.005f);
					else Textbox.CreateTextbox(self, 0.005f);
				}
				else if (Game.stage == 2)
				{
					if (!talked_to) Textbox.CreateTextbox(ruins, 0.005f);
					else Textbox.CreateTextbox(tavern, 0.005f);
				}
				else if (Game.stage == 3)
				{
					if (!talked_to)
					{
						if (Game.puzzle == "lever") Textbox.CreateTextbox(lever_puzzle, 00.005f);
						else if (Game.puzzle == "button") Textbox.CreateTextbox(button_puzzle, 0.005f);
						else Textbox.CreateTextbox(path_puzzle, 0.01f);
					}
					else Textbox.CreateTextbox(gossip, 0.005f);
				}

				talk = false;
				talked_to = true;
				delay_counter = 0f;
				Game.spoke_to_bartender = true;
			}
			else delay_counter += Time.deltaTime;
		}

		UpdateActivity();
	}
}
