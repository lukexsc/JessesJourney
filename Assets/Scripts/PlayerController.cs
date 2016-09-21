using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
[RequireComponent (typeof (SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
	// Movement Variables
	public float move_speed = 6f; // How fast the player moves
	Vector2 velocity; // player's movement
	Controller2D controller; // 2d movement + collision code component

	void Start()
	{
		// Define Components
		controller = GetComponent<Controller2D>();
	}

	void Update ()
	{
		// Get Input
		Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		velocity = move;

		controller.Move(velocity * Time.deltaTime * move_speed); // move the player
	}
}
