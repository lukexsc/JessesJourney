using UnityEngine;
using System.Collections;

public class Interactive : MonoBehaviour
{
	public bool active;
	public bool keep_state;

	void Update ()
	{
		//if (active) GetComponent<SpriteRenderer>().color = Color.red;
		//else GetComponent<SpriteRenderer>().color = Color.blue;

		if (active)
		{
			string[] text = {
				"This is a test of the dialgoue system.",
				"This is testing how the text box deals with putting the text to a new line when the sentence is really long and is more than likely a run on sentence."
			};
			Textbox.CreateTextbox(text, true, 0.01f);
		}

		UpdateActivity();
	}

	public void UpdateActivity()
	{
		if (!keep_state) // if it does not keep the state it is in
		{
			if (active) active = false;
		}
	}
}