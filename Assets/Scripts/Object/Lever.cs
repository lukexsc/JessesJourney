using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SpriteRenderer))]
[RequireComponent (typeof (Interactive))]
public class Lever : MonoBehaviour
{
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
		if (inter.active) rend.sprite = down_sprite;
		else rend.sprite = up_sprite;
	}

	void Update ()
	{
		if (inter.active) rend.sprite = down_sprite;
		else rend.sprite = up_sprite;
	}

	public bool RightSetting()
	{
		return (correct_setting == inter.active);
	}

	public bool GetActive()
	{
		return inter.active;
	}

	public void Switch()
	{
		inter.Activate();
	}
}
