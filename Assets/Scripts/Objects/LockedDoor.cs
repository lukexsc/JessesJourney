using UnityEngine;
using System.Collections;

public class LockedDoor : Door
{
	public int needed_keys;

	public override void Update ()
	{
		if (!open && activator.active) // open locked door
		{
			if (KeysUI.keys_ui.keys >= needed_keys) OpenDoor();
		}
	}
}
