using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider2D))]
[RequireComponent (typeof (SpriteRenderer))]
public class BarDoor : MonoBehaviour
{
	SpriteRenderer rend;
	int start_layer;
	Color start_color;
	protected bool open;

	void Start ()
	{
		open = false;
		rend = GetComponent<SpriteRenderer>();
		start_layer = gameObject.layer;
		start_color = rend.color;

		if (Game.spoke_to_bartender) OpenDoor();
	}

	public virtual void Update ()
	{
		if (!open && Game.spoke_to_bartender && !Textbox.text_box.active) // open door if spoke to bartender
		{
			OpenDoor();
		}
	}

	public void OpenDoor()
	{
		gameObject.layer = 0;
		rend.color = new Color(start_color.r, start_color.g, start_color.b, 0f);
		open = true;
	}

	public void CloseDoor()
	{
		gameObject.layer = start_layer;
		rend.color = start_color;
		open = false;
	}
}