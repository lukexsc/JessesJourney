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
	public bool pop_up;
	//public AudioClip down_effect;

	protected bool pressed;
	protected SpriteRenderer rend;
	protected Interactive inter;

	AudioSource source;

	void Awake ()
	{
		inter = GetComponent<Interactive>();
		rend = GetComponent<SpriteRenderer>();
		source = GetComponent<AudioSource>();
	}

	void Start ()
	{
		SetSprite(down);
	}

	void Update ()
	{
		if (pop_up) SetActive(inter.active);
		else if (inter.active && !down) Switch(); // If interacted and lever is up - pull down
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
		if (!pressed)
		{
			//SoundController.instance.PlayEffect(down_effect);
			source.Play();
			pressed = true;
		}
	}

	public void SetActive(bool val)
	{
		down = val;
		SetSprite(down);
		if (down)
		{
			if (!pressed)
			{
				//SoundController.instance.PlayEffect(down_effect);
				source.Play();
				pressed = true;
			}
		}
		else pressed = false;
	}
}
