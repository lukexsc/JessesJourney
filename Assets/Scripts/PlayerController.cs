using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
[RequireComponent (typeof (Animator))]
[RequireComponent (typeof (SpriteRenderer))]
public class PlayerController : Entity
{
	// Movement Variables
	[HideInInspector] public bool paused; // if player actions are paused
	public float move_speed = 6f; // How fast the player moves
	float active_dis = 0.3f; // distance the player needs to be from an object to activate it
	Vector2 velocity; // player's movement
	Controller2D controller; // 2d movement + collision code component

	Animator anim;
	SpriteRenderer rend;

	public override void Start()
	{
		base.Start ();
		controller = GetComponent<Controller2D>();
		anim = GetComponent<Animator>();
		rend = GetComponent<SpriteRenderer>();
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
		}

		SetAnimation((velocity.x != 0f || velocity.y != 0f), (int)controller.facing.x, (int)controller.facing.y);
	}

	// Sets the Player's animator's variables
	void SetAnimation(bool walking, int x_dir, int y_dir)
	{
		anim.SetBool("Walking", walking);
		anim.SetInteger("X Dir", x_dir);
		anim.SetInteger("Y Dir", y_dir);
		if (x_dir < 0 && y_dir == 0) rend.flipX = true; // face left by flipping sprite
		else rend.flipX = false; // otherwise don't flip
	}

	bool DetectHit (RaycastHit2D hit)
	{
		Interactive inter = hit.transform.GetComponent<Interactive>();
		if (inter)
		{
			if (Input.GetKeyDown(KeyCode.E)) inter.Activate();
			return true;
		}
		else return false;
	}
}
