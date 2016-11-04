using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider2D))]
[RequireComponent (typeof (SpriteRenderer))]
public class Door : MonoBehaviour
{
	public Interactive activator;
	SpriteRenderer rend;
	int start_layer;
	Color start_color;
	bool open;

	void Start ()
	{
		open = false;
		rend = GetComponent<SpriteRenderer>();
		start_layer = gameObject.layer;
		start_color = rend.color;
	}

	void Update ()
	{
		if (!open && activator.active) // open door
		{
			gameObject.layer = 0;
			rend.color = new Color(start_color.r, start_color.g, start_color.b, 0f);
			open = true;
		}
		else if (open && !activator.active) // Close Door
		{
			gameObject.layer = start_layer;
			rend.color = start_color;
			open = false;
		}
	}
}
