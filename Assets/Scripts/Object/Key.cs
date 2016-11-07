using UnityEngine;
using System.Collections;

public class Key : Trigger
{
	public override void Update ()
	{
		base.Update ();

		if (inter.active)
		{
			KeysUI.AddKey();
			Destroy(gameObject);
		}
	}
}
