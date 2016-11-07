using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SpriteRenderer))]
[RequireComponent (typeof (Interactive))]
public class Lever : MonoBehaviour
{
	public bool down;
	public bool correct_setting;
	public Sprite up_sprite;
	public Sprite down_sprite;

	SpriteRenderer rend;
	Interactive inter;

	void Awake ()
	{
		inter = GetComponent<Interactive>();
		rend = GetComponent<SpriteRenderer>();
	}

	void Start ()
	{
		SetSprite(down);
	}

	void Update ()
	{
		if (inter.active && !down) Switch(); // If interacted and lever is up - pull down
	}

	// Sets the sprite of the lever
	void SetSprite(bool lever_down)
	{
		if (lever_down) rend.sprite = down_sprite;
		else rend.sprite = up_sprite;
	}

	// Check if lever is supposed to be up or down
	public bool RightSetting()
	{
		return (correct_setting == down);
	}

	// Get if the lever is down or not
	public bool GetActive()
	{
		return down;
	}

	// Switches the position of the lever
	public void Switch()
	{
		down = !down;
		SetSprite(down);
	}
}
