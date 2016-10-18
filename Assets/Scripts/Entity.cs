using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SpriteRenderer))]
public class Entity : MonoBehaviour
{
	public float order_offset = 0f;
	protected SpriteRenderer render;

	public virtual void Start ()
	{
		render = GetComponent<SpriteRenderer>();
		SetDrawOrder();
	}

	public void SetDrawOrder()
	{
		render.sortingOrder = (int)((transform.position.y + order_offset) * 100 * -1);
	}
}
