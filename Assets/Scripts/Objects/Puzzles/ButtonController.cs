using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Interactive))]
public class ButtonController : MonoBehaviour
{
	public Lever[] buttons;
	public Lever fake_button;
	public AudioClip complete_effect;
	public AudioClip fail_effect;

	bool[] button_pressed; // if the buttons have been pressed yet
	int current; // button that needs to be pressed next
	bool beat = false; // if beat the puzzle

	Interactive inter;

	void Start ()
	{
		inter = GetComponent<Interactive>();

		current = 0;
		button_pressed = new bool[buttons.Length];
		for (int i=0; i<buttons.Length; i++) button_pressed[i] = false;
	}

	void Update ()
	{
		// if Press Fake Button - Reset 
		if (fake_button.GetActive())
		{
			Reset();
			return;
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
							SoundController.instance.PlayEffect(complete_effect);
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

		SoundController.instance.PlayEffect(fail_effect);

		for (int i=0; i<buttons.Length; i++)
		{
			buttons[i].SetActive(false);
			button_pressed[i] = false;
		}
		fake_button.SetActive(false);
	}
}
