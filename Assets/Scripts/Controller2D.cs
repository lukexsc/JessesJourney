using UnityEngine;
using System.Collections;

public class Controller2D : RaycastController
{
	float max_climb_slope = 80f;
	float max_descend_slope = 75f;

	public CollisionInfo collisions;

	// Move Object
	public void Move (Vector2 velocity)
	{
		collisions.Reset();
		UpdateRaycastOrigins();
		collisions.velocity_old = velocity;

		if (velocity.y < 0) DescendSlopes(ref velocity);
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
				// Slopes
				float slope = Vector2.Angle(hit.normal, Vector2.up);
				if (i ==0 && slope <= max_climb_slope) 
				{
					if (collisions.descend_slope) // correct descending to climbing transition
					{
						collisions.descend_slope = false;
						velocity = collisions.velocity_old;
					}
					float dis_to_slope_start = 0;
					if (collisions.slope != collisions.slope_old) // if moving to new slope - move up to it
					{
						dis_to_slope_start = hit.distance - skin;
						velocity.x -= dis_to_slope_start * dir_x;
					}
					ClimbSlope(ref velocity, slope);
					velocity.x += dis_to_slope_start * dir_x;
				}

				if (!collisions.climb_slope || slope > max_climb_slope) // if not on slope or too steep
				{
					velocity.x = (hit.distance-skin) * dir_x; // move until it meets the obstacle
					ray_length = hit.distance; // Set as max distance

					// colide with obstacle while on slope
					if (collisions.climb_slope) velocity.y = Mathf.Tan(collisions.slope * Mathf.Deg2Rad) * Mathf.Abs(velocity.x);

					// Set Collision Sides
					collisions.left = (dir_x == -1);
					collisions.right = (dir_x == 1);
				}
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

				if (collisions.climb_slope) velocity.x = velocity.y / Mathf.Tan(collisions.slope * Mathf.Deg2Rad) * Mathf.Sign(velocity.x);

				// Set Collision Sides
				collisions.below = (dir_y == -1);
				collisions.above = (dir_y == 1);
			}
		}

		if (collisions.climb_slope) // don't get stuck changing between slopes
		{
			float dir_x = Mathf.Sign(velocity.x);
			ray_length = Mathf.Abs(velocity.x) + skin;
			Vector2 ray_origin = ((dir_x == -1)?origins.bottom_left:origins.bottom_right) + Vector2.up * velocity.y;
			RaycastHit2D hit = Physics2D.Raycast(ray_origin, Vector2.right * dir_x, ray_length, collision_mask);

			if (hit)
			{
				float slope = Vector2.Angle(hit.normal, Vector2.up);
				if (slope != collisions.slope)
				{
					velocity.x = (hit.distance - skin) * dir_x;
					collisions.slope = slope;
				}
			}
		}
	}

	// Climb up slopes
	void ClimbSlope(ref Vector2 velocity, float slope)
	{
		float move_dis = Mathf.Abs(velocity.x);
		float climb_vy =  Mathf.Sin(slope*Mathf.Deg2Rad) * move_dis;

		if (velocity.y <= climb_vy) // not jumping
		{
			velocity.y = climb_vy;
			velocity.x = Mathf.Cos(slope*Mathf.Deg2Rad) * move_dis * Mathf.Sign(velocity.x);
			collisions.below = true;
			collisions.climb_slope = true;
			collisions.slope = slope;
		}
	}

	// Descend Slopes
	void DescendSlopes(ref Vector2 velocity)
	{
		float dir_x = Mathf.Sign(velocity.x);
		Vector2 ray_origin = (dir_x == -1)?origins.bottom_right:origins.bottom_left;
		RaycastHit2D hit = Physics2D.Raycast(ray_origin, -Vector2.up, Mathf.Infinity, collision_mask);

		Debug.DrawRay(ray_origin, -Vector2.up * 2f, Color.yellow);

		if (hit)
		{
			float slope = Vector2.Angle(hit.normal, Vector2.up);
			if (slope != 0 && slope <= max_descend_slope)
			{
				if (Mathf.Sign(hit.normal.x) == dir_x) // if slope in same direction as movement
				{
					if (hit.distance - skin <= Mathf.Tan(slope * Mathf.Deg2Rad) * Mathf.Abs(velocity.x)) // if close to slope
					{
						float move_dis = Mathf.Abs(velocity.x);
						float descend_vy =  Mathf.Sin(slope*Mathf.Deg2Rad) * move_dis;
						velocity.x = Mathf.Cos(slope*Mathf.Deg2Rad) * move_dis * Mathf.Sign(velocity.x);
						velocity.y -= descend_vy;

						collisions.slope = slope;
						collisions.descend_slope = true;
						collisions.below = true;
					}
				}
			}
		}
	}

	public struct CollisionInfo
	{
		public bool above, below;
		public bool left, right;

		public bool climb_slope, descend_slope;
		public float slope, slope_old;

		public Vector2 velocity_old;

		public void Reset ()
		{
			above = below = left = right = climb_slope = descend_slope = false;
			slope_old = slope;
			slope = 0;
		}
	}
}