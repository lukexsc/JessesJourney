using UnityEngine;
using System.Collections;

public class Interactive : MonoBehaviour
{
	public bool active;
	public bool keep_state;

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
}