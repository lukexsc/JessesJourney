using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Entity))]
[RequireComponent (typeof (SpriteRenderer))]
[RequireComponent (typeof (Controller2D))]
public class PushBlock : Interactive
{
	public float dis;
	public float speed;

	Vector3 target;
	Vector2 dir;
	Controller2D controller;
	Entity entity;

	AudioSource source;

	void Start ()
	{
		entity = GetComponent<Entity>();
		controller = GetComponent<Controller2D>();
		target = transform.position;

		source = GetComponent<AudioSource>();
	}

	public override void Update ()
	{
		if (Game.paused) return;
			
		if (target == transform.position) // if not moving
		{
			if (active) // if Pushed
			{
				//SoundController.instance.PlayEffect(push_effect);
				source.Play();
				dir = Game.player.controller.facing;
				if (dir.y != 0f) target = transform.position + new Vector3(0f, dir.y * dis); // Vertical Move
				else target = transform.position + new Vector3(dir.x * dis, 0f); // Horizontal Move
			}
		}
		else // Move
		{
			entity.SetDrawOrder(); // Reset Draw Order
			Vector2 velocity = dir * speed * Time.deltaTime;
			Vector3 move_goal = transform.position + (Vector3)velocity;
			controller.Move(velocity);

			if (transform.position != move_goal) target = transform.position;
			if (Mathf.Abs(transform.position.x - target.x) <= 0.05f && Mathf.Abs(transform.position.y - target.y) <= 0.05f) // if close to target - set to target
			{
				transform.position = target;
				dir = Vector2.zero;
			}
		}
		UpdateActivity();
	}
}
