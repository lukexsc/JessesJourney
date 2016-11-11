using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Transition : Trigger
{
	public string room;
	public Vector3 loc;
	public AudioClip transition_effect;
	public AudioClip room_music;

	public override void Update ()
	{
		base.Update ();

		if (inter.active)
		{
			if (room_music) SoundController.instance.PlayMusic(room_music);
			SoundController.instance.PlayEffect(transition_effect);
			Fade.instance.LoadScene(room, loc, false);
		}
	}
}
