using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class Key : Trigger
{
	AudioSource source;

	void Awake ()
	{
		if (KeysUI.CheckRoom(Game.GetCurrentSceneName())) Destroy(gameObject); 
	}

	public override void Start ()
	{
		base.Start ();
		source = GetComponent<AudioSource>();
	}

	public override void Update ()
	{
		base.Update ();

		// Picked Up
		if (inter.active)
		{
			KeysUI.AddKey();
			source.Play();
			Destroy(gameObject);
		}
	}
}
