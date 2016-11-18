using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SpriteRenderer))]
public class Bartender : Interactive
{
	public string[] dialogue;
	public AudioClip talk_effect;
	public float delay;
	float delay_counter;
	bool talk;

	public override void Update ()
	{
		if (active)
		{
			Game.paused = true;
			talk = true;
			delay_counter = 0f;
			SoundController.instance.PlayEffect(talk_effect);
		}

		if (talk)
		{
			if (delay_counter >= delay)
			{
				Textbox.CreateTextbox(dialogue, true, 0.01f);
				talk = false;
				delay_counter = 0f;
				Game.spoke_to_bartender = true;
			}
			else delay_counter += Time.deltaTime;
		}

		UpdateActivity();
	}
}
