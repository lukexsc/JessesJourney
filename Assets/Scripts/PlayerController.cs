using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Controller2D))]

public class PlayerController : Entity
{
	// Sprites
	public Sprite[] sprites;

	// Movement Variables
	[HideInInspector] public bool paused; // if player actions are paused
	public float move_speed = 6f; // How fast the player moves
	float active_dis = 0.3f; // distance the player needs to be from an object to activate it
	Vector2 velocity; // player's movement
	Controller2D controller; // 2d movement + collision code component

	public override void Start()
	{
		base.Start ();
		controller = GetComponent<Controller2D>();
		SetSpriteFacing("down");
	}

	void Update ()
	{
		if (paused) return; // if paused, skip code

		SetDrawOrder(); // Reset Draw Order
			
		// Get Input
		velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		controller.Move(velocity * Time.deltaTime * move_speed); // move the player

		// Detect Objects the player is facing
		if (controller.facing.y != 0f) // facing vertical
		{
			for (int i=0;i<controller.ver_ray_count;i++) // go through rays
			{
				Vector2 ray_origin;
				if (controller.facing.y > 0f) ray_origin = controller.origins.top_left;
				else ray_origin = controller.origins.bottom_left;

				ray_origin += Vector2.right * controller.ver_ray_space * i;
				RaycastHit2D hit = Physics2D.Raycast(ray_origin, Vector2.up * controller.facing.y, active_dis, controller.collision_mask);

				Debug.DrawRay(ray_origin, Vector2.up * controller.facing.y * active_dis, Color.yellow);

				if (hit) // if raycast hit a collidable object
				{
					bool inter = DetectHit(hit);
					if (inter) break;
				}
			}

			if (controller.facing.y > 0f) SetSpriteFacing("up");
			else SetSpriteFacing("down");
		}
		else // facing horizontally
		{
			for (int i=0;i<controller.hor_ray_count;i++)
			{
				Vector2 ray_origin;
				if (controller.facing.x > 0f) ray_origin = controller.origins.bottom_right;
				else ray_origin = controller.origins.bottom_left;

				ray_origin += Vector2.up * controller.hor_ray_space * i;
				RaycastHit2D hit = Physics2D.Raycast(ray_origin, Vector2.right * controller.facing.x, active_dis, controller.collision_mask);

				Debug.DrawRay(ray_origin, Vector2.right * controller.facing.x * active_dis, Color.yellow);

				if (hit) // if raycast hit a collidable object
				{
					bool inter = DetectHit(hit);
					if (inter) break;
				}
			}

			if (controller.facing.x > 0f) SetSpriteFacing("right");
			else SetSpriteFacing("left");
		}
	}

	void SetSpriteFacing(string dir)
	{
		if (dir.ToLower() == "up") render.sprite = sprites[1]; // 1 - up
		else if (dir.ToLower() == "right") render.sprite = sprites[2]; // 2 - right
		else if (dir.ToLower() == "left") render.sprite = sprites[3];// 3 - left
		else render.sprite = sprites[0]; // 0 - down
	}

	bool DetectHit (RaycastHit2D hit)
	{
		Interactive inter = hit.transform.GetComponent<Interactive>();
		if (inter)
		{
			if (Input.GetKeyDown(KeyCode.E)) inter.active = !inter.active;
			return true;
		}
		else return false;
	}
}
