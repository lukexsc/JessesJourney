using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Interactive))]
public class ButtonController : MonoBehaviour
{
	public Lever[] buttons;
	public Lever[] fake_buttons;
	public AudioClip complete_effect;
	public AudioClip fail_effect;

	bool[] button_pressed; // if the buttons have been pressed yet
	int current; // button that needs to be pressed next
	bool beat = false; // if beat the puzzle

	Interactive inter;
	AudioSource source;

	void Start ()
	{
		inter = GetComponent<Interactive>();
		source = GetComponent<AudioSource>();

		current = 0;
		button_pressed = new bool[buttons.Length];
		for (int i=0; i<buttons.Length; i++) button_pressed[i] = false;
	}

	void Update ()
	{
		// if Press any Fake Button - Reset 
		for (int i=0; i<fake_buttons.Length; i++)
		{
			if (fake_buttons[i].GetActive())
			{
				Reset();
				return;
			}
		}

		// Cycle Buttons
		if (!beat)
		{
			for (int i=0; i<buttons.Length; i++)
			{
				if (buttons[i].GetActive() != button_pressed[i]) // If button is pressed
				{
					if (i == current) // if pressed correct next button
					{
						if (current == buttons.Length-1) // if last button pressed - end puzzle
						{
							beat = true;
							//SoundController.instance.PlayEffect(complete_effect);
							source.clip = complete_effect;
							source.Play();
						}
						else // Not end of the puzzle - move to next button
						{
							current++;
						}
					}
					else // if pressed wrong button
					{
						Reset();
					}
				}
				button_pressed[i] = buttons[i].GetActive();
			}
		}
		else
		{
			inter.SetActive(true);
		}
	}

	void Reset()
	{
		current = 0;
		beat = false;
		inter.SetActive(false);

		//SoundController.instance.PlayEffect(fail_effect);
		source.clip = fail_effect;
		source.Play();

		for (int i=0; i<buttons.Length; i++)
		{
			buttons[i].SetActive(false);
			button_pressed[i] = false;
		}

		for (int i=0; i<fake_buttons.Length; i++)
		{
			fake_buttons[i].SetActive(false);
		}
	}
}
