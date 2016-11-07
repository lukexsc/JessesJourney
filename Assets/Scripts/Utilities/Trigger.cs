using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Interactive))]
public class Trigger : RaycastController
{
	public Trigger.TriggerMode mode;
	protected GameObject trigger_object;
	protected Interactive inter;

	public override void Start ()
	{
		base.Start ();
		inter = GetComponent<Interactive>();
	}

	public virtual void Update ()
	{
		UpdateRaycastOrigins();
		float ray_length = col.bounds.size.x;

		bool hit_anything = false; // stores if raycasts hit anything
		for (int i=0;i<hor_ray_count;i++) // cycle through rays
		{
			Vector2 ray_origin = origins.bottom_right;
			ray_origin += Vector2.up * hor_ray_space * i;
			RaycastHit2D hit = Physics2D.Raycast(ray_origin, -1 * Vector2.right, ray_length, collision_mask);

			Debug.DrawRay(ray_origin, -1 * Vector2.right * ray_length, Color.red);

			if (hit) // if raycast hit an activator object
			{
				hit_anything = true;
				if (mode == Trigger.TriggerMode.stay) inter.SetActive(true); // if activate on stay - set active
				else if (mode == Trigger.TriggerMode.enter || mode == Trigger.TriggerMode.enterleave) // if activate on enter
				{
					if (trigger_object != hit.transform.gameObject) inter.SetActive(true); // if the stored trigger object is not the same hit object - actvate
					else inter.SetActive(false); // if not new, set not active
				}
				trigger_object = hit.transform.gameObject; // set trigger object
				break;
			}
		}

		if (!hit_anything) // if raycasts hit nothing
		{
			if (mode == Trigger.TriggerMode.leave || mode == Trigger.TriggerMode.enterleave) // if activate on leave
			{
				if (trigger_object != null) inter.SetActive(true); // if there was an object in the trigger - set active
				else inter.SetActive(false);
			}
			else inter.SetActive(false); // if active on enter or stay - set not active
			trigger_object = null; // reset trigger object
		}
	}

	public enum TriggerMode
	{
		enter,
		leave,
		stay,
		enterleave
	}
}