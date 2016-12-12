using UnityEngine;
using System.Collections;

public class Key : Trigger
{
	public AudioClip collect_clip;

	public override void Start ()
	{
		base.Start ();
		if (KeysUI.CheckRoom(Game.GetCurrentSceneName())) Destroy(gameObject); 
	}

	public override void Update ()
	{
		base.Update ();

		// Picked Up
		if (inter.active)
		{
			KeysUI.AddKey();
			SoundController.instance.PlayEffect(collect_clip);
			Destroy(gameObject);
		}
	}
}
