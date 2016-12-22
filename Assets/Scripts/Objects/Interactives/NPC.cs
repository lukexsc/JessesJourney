using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SpriteRenderer))]
public class NPC : Interactive
{
	public DialogueLine[] dialogue;
	//public AudioClip talk_effect;
	public float delay;
	float delay_counter;
	bool talk;

	AudioSource source;

	public virtual void Start ()
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
				Textbox.CreateTextbox(dialogue, 0.005f);
				talk = false;
				delay_counter = 0f;
			}
			else delay_counter += Time.deltaTime;
		}

		UpdateActivity();
	}
}
