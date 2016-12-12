using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
[RequireComponent (typeof (BoxCollider2D))]
[RequireComponent (typeof (SpriteRenderer))]
public class BarDoor : MonoBehaviour
{
	SpriteRenderer rend;
	int start_layer;
	Color start_color;
	protected bool open;

	//AudioSource source;

	void Start ()
	{
		open = false;
		rend = GetComponent<SpriteRenderer>();
		start_layer = gameObject.layer;
		start_color = rend.color;

		//source = GetComponent<AudioSource>();
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
		//source.Play();
	}

	public void CloseDoor()
	{
		gameObject.layer = start_layer;
		rend.color = start_color;
		open = false;
	}
}