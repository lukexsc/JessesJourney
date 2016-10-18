using UnityEngine;
using System.Collections;

public class Controller2D : RaycastController
{
	CollisionInfo collisions;
	[HideInInspector] public Vector2 facing; // direction facing

	public override void Start ()
	{
		base.Start ();
		facing = new Vector2(0f, -1f); // start facing down
	}

	// Move Object
	public void Move (Vector2 velocity)
	{
		collisions.Reset();
		UpdateRaycastOrigins();
		collisions.velocity_old = velocity;
		SetFacing(velocity); // Set the colliders facing

		if (velocity.x != 0f) HorizontalCollisions(ref velocity);
		if (velocity.y != 0f) VerticalCollisions(ref velocity);

		transform.Translate(velocity);
	}

	// Calculate Horizontal Collisions
	void HorizontalCollisions (ref Vector2 velocity)
	{
		float dir_x = Mathf.Sign(velocity.x);
		float ray_length = Mathf.Abs(velocity.x) + skin;

		for (int i=0;i<hor_ray_count;i++)
		{
			Vector2 ray_origin = (dir_x == -1)?origins.bottom_left:origins.bottom_right;
			ray_origin += Vector2.up * hor_ray_space * i;
			RaycastHit2D hit = Physics2D.Raycast(ray_origin, Vector2.right * dir_x, ray_length, collision_mask);

			Debug.DrawRay(ray_origin, Vector2.right * dir_x, Color.red);

			if (hit) // if raycast hit a collidable object
			{
				velocity.x = (hit.distance-skin) * dir_x; // move until it meets the obstacle
				ray_length = hit.distance; // Set as max distance

				// Set Collision Sides
				collisions.left = (dir_x == -1);
				collisions.right = (dir_x == 1);
			}
		}
	}

	// Calculate Vertical Collisions
	void VerticalCollisions (ref Vector2 velocity)
	{
		float dir_y = Mathf.Sign(velocity.y);
		float ray_length = Mathf.Abs(velocity.y) + skin;

		for (int i=0;i<ver_ray_count;i++)
		{
			Vector2 ray_origin = (dir_y == -1)?origins.bottom_left:origins.top_left;
			ray_origin += Vector2.right * (ver_ray_space * i + velocity.x);
			RaycastHit2D hit = Physics2D.Raycast(ray_origin, Vector2.up * dir_y, ray_length, collision_mask);

			Debug.DrawRay(ray_origin, Vector2.up * dir_y, Color.red);

			if (hit) // if raycast hit a collidable object
			{
				velocity.y = (hit.distance-skin) * dir_y; // move until it meets the obstacle
				ray_length = hit.distance; // Set as max distance

				// Set Collision Sides
				collisions.below = (dir_y == -1);
				collisions.above = (dir_y == 1);
			}
		}
	}

	// Set the direction the collider is facing
	void SetFacing(Vector2 velocity)
	{
		if (velocity.x != 0f || velocity.y != 0f) // if moving in a direction
		{
			if (velocity.x != 0f) facing.x = Mathf.Sign(velocity.x);
			else facing.x = 0f;

			if (velocity.y != 0f) facing.y = Mathf.Sign(velocity.y);
			else facing.y = 0f;
		}
	}

	public struct CollisionInfo
	{
		public bool above, below;
		public bool left, right;

		public Vector2 velocity_old;

		public void Reset ()
		{
			above = below = left = right = false;
		}
	}
}