using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider2D))]
public class RaycastController : MonoBehaviour
{
	public LayerMask collision_mask;
	const float ray_space = 0.35f;
	[HideInInspector] public const float skin = 0.015f;
	[HideInInspector] public int hor_ray_count;
	[HideInInspector] public int ver_ray_count;

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

		// Set Raycount
		hor_ray_count = Mathf.Max(Mathf.CeilToInt(bounds.size.y / ray_space), 2);
		ver_ray_count = Mathf.Max(Mathf.CeilToInt(bounds.size.x / ray_space), 2);

		// Calculate Spacing
		hor_ray_space = bounds.size.y / (hor_ray_count-1);
		ver_ray_space = bounds.size.x / (ver_ray_count-1);
	}

	public struct RaycastOrigins
	{
		public Vector2 top_left, top_right, bottom_left, bottom_right;
	}
}