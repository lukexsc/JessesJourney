using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Transition : Trigger
{
	public string room;
	public Vector3 loc;

	public override void Update ()
	{
		base.Update ();

		if (inter.active)
		{
			Fade.instance.LoadScene(room, loc, false);
		}
	}
}
