using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Interactive))]
public class LeverController : MonoBehaviour
{
	public Lever[] levers;
	public Interactive reset;
	public AudioClip complete_effect;

	bool[] levers_base;
	bool[] levers_active;
	bool baseline; // if no lever has been pulled
	bool beat = false;
	Interactive inter;

	void Start ()
	{
		baseline = true;
		inter = GetComponent<Interactive>();
		levers_active = new bool[levers.Length];
		levers_base = new bool[levers.Length];
		for (int i=0; i<levers.Length; i++) // set base activation for levers
		{
			levers_active[i] = levers[i].GetActive();
			levers_base[i] = levers[i].GetActive();
		}
	}

	void Update ()
	{
		// Reset Puzzle
		if (reset.active && !baseline)
		{
			baseline = true;
			for (int i=0; i<levers.Length; i++)
			{
				levers[i].SetActive(levers_base[i]);
				SetLever(i);
			}
			return;
		}

		// Check for activations and Flip Adjacent Switches
		for (int i=0; i<levers.Length; i++)
		{
			if (levers_active[i] != levers[i].GetActive()) // if lever switched
			{
				baseline = false;
				SetLever(i);
				if (i > 0) SwitchLever(i-1); // not on left end
				if (i < levers.Length-1) SwitchLever(i+1); // not on right end
				break;
			}
		}

		// Check if Complete
		int lever_count = 0;
		for (int i=0; i<levers.Length; i++)
		{
			if (levers[i].RightSetting()) lever_count++; // if lever correct
			else break;
		}
			
		if (lever_count >= levers.Length) // if puzzle completed
		{
			if (!beat)
			{
				beat = true;
				SoundController.instance.PlayEffect(complete_effect);
			}
			inter.SetActive(true);
		}
		else
		{
			beat = false;
			inter.SetActive(false); // Not done
		}
	}

	void SetLever(int i)
	{
		levers_active[i] = levers[i].GetActive();
	}

	void SwitchLever(int i)
	{
		levers[i].Switch();
		SetLever(i);
	}
}
