using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Interactive))]
public class LeverController : MonoBehaviour
{
	public Lever[] levers;
	bool[] levers_active;
	Interactive inter;

	void Start ()
	{
		inter = GetComponent<Interactive>();
		levers_active = new bool[levers.Length];
		for (int i=0; i<levers.Length; i++) levers_active[i] = levers[i].GetActive();
	}

	void Update ()
	{
		// Check for activations and Flip Adjacent Switches
		for (int i=0; i<levers.Length; i++)
		{
			if (levers_active[i] != levers[i].GetActive()) // if lever switched
			{
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
			
		if (lever_count >= levers.Length) inter.SetActive(true); // if puzzle completed
		else inter.SetActive(false); // Not done
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
