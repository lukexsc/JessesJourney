using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider2D))]
public class RaycastController : MonoBehaviour
{
	public LayerMask collision_mask;
	[HideInInspector] public const float skin = 0.015f;
	public int hor_ray_count = 3;
	public int ver_ray_count = 3;

	[HideInInspector] public float hor_ray_space;
	[HideInInspector] public float ver_ray_space;

	[HideInInspector] public BoxCollider2D col;
	[HideInInspector] public RaycastOrigins origins;

	public virtual void Start ()
	{
		col = GetComponent<BoxCollider2D>();
		CalculateRaySpacing();
	}

	// Updates the location of the corners of the bounding box
	public void UpdateRaycastOrigins()
	{
		// Shrinks bounding box to give 'skin' for raycast origin
		Bounds bounds = col.bounds;
		bounds.Expand(-2*skin);

		// Set Points
		origins.bottom_left = new Vector2(bounds.min.x, bounds.min.y);
		origins.bottom_right = new Vector2(bounds.max.x, bounds.min.y);
		origins.top_left = new Vector2(bounds.min.x, bounds.max.y);
		origins.top_right = new Vector2(bounds.max.x, bounds.max.y);
	}

	// Calculates the Ray Spacing
	public void CalculateRaySpacing ()
	{
		// Shrinks bounding box to give 'skin' for raycast origin
		Bounds bounds = col.bounds;
		bounds.Expand(-2*skin);

		// Set Minimum of 2 rays
		hor_ray_count = Mathf.Clamp(hor_ray_count, 2, int.MaxValue);
		ver_ray_count = Mathf.Clamp(ver_ray_count, 2, int.MaxValue);

		// Calculate Spacing
		hor_ray_space = bounds.size.y / (hor_ray_count-1);
		ver_ray_space = bounds.size.x / (ver_ray_count-1);
	}

	public struct RaycastOrigins
	{
		public Vector2 top_left, top_right, bottom_left, bottom_right;
	}
}