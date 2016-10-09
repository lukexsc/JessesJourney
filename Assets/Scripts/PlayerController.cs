using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
[RequireComponent (typeof (SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
	// Movement Variables
	[HideInInspector] public bool paused; // if player actions are paused
	public float move_speed = 6f; // How fast the player moves
	float active_dis = 0.3f; // distance the player needs to be from an object to activate it
	Vector2 velocity; // player's movement
	Controller2D controller; // 2d movement + collision code component

	void Start()
	{
		// Define Components
		controller = GetComponent<Controller2D>();
	}

	void Update ()
	{
		if (paused) return; // if paused, skip code
			
		// Get Input
		Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		velocity = move;

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
					DetectHit(hit);
					break;
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
					DetectHit(hit);
					break;
				}
			}
		}

		// TEMP Scene Transition Test
		//
		if (Input.GetKeyDown(KeyCode.L)) Fade.instance.LoadScene("TEST2");
		//
		//
	}

	void DetectHit (RaycastHit2D hit)
	{
		Interactive inter = hit.transform.GetComponent<Interactive>();
		if (inter)
		{
			if (Input.GetKeyDown(KeyCode.E)) inter.active = !inter.active;
		}
	}
}
