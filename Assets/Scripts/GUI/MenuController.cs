using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{
	public GameObject menu_obj;
	bool active;

	Textbox box;
	TextInput input;

	void Start ()
	{
		menu_obj.SetActive(false);
		active = false;
		box = GetComponent<Textbox>();
		input = GetComponent<TextInput>();
	}

	void Update ()
	{
		if (Input.GetButtonDown("Menu")) // Toggle Menu
        {
			if (!input.active)
			{
				active = !active;
				menu_obj.SetActive(active);
				box.paused = active;
				Game.paused = active;
                if (active) menu_obj.GetComponent<Menu>().first_button.Select(); // select button
			}
		}
	}
}
