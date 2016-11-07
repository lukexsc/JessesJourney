using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SpriteRenderer))]
public class NPC : Interactive
{
	public string[] dialogue;
		
	public override void Update ()
	{
		if (active) Textbox.CreateTextbox(dialogue, true, 0.01f);
		UpdateActivity();
	}
}
