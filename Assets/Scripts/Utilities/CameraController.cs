using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public Transform follow;
	public float rate;

	void LateUpdate ()
	{
		if (transform.position.x != follow.position.x || transform.position.y != follow.position.y) // if camera not centered on object
		{
			if (Mathf.Abs(transform.position.x - follow.position.x) < 0.1f
				&& Mathf.Abs(transform.position.y - follow.position.y) < 0.1f) transform.position = new Vector3(follow.position.x, follow.position.y, transform.position.z); // if allmost equivalent - set equal
			else transform.position = new Vector3(Mathf.Lerp(transform.position.x, follow.position.x, rate), Mathf.Lerp(transform.position.y, follow.position.y, rate), transform.position.z);
		}
	}
}
