using UnityEngine;
using System.Collections;

public class Interactive : MonoBehaviour
{
	public bool active = false;
	public bool keep_state;
	public bool ignore_player; // ignore the player attempts to directly activate

	public virtual void Update ()
	{
		UpdateActivity();
	}

	public void UpdateActivity()
	{
		if (!keep_state) // if it does not keep the state it is in
		{
			if (active) active = false;
		}
	}

	public virtual void Activate()
	{
		active = !active;
	}

	public virtual void SetActive(bool val)
	{
		active = val;
	}

	public virtual void PlayerActivate()
	{
		if (!ignore_player) Activate();
	}
}