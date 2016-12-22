using UnityEngine;
using System.Collections;

public class StageUpdate : MonoBehaviour
{
	public int stage_num;
	public bool overide_higher;
	public string puzzle_room;
	Interactive inter;

	void Start ()
	{
		inter = GetComponent<Interactive>();
	}
	
	void Update ()
	{
		if (inter.active)
		{
			if (overide_higher || Game.stage < stage_num) Game.stage = stage_num;
			if (puzzle_room != "") Game.puzzle = puzzle_room;
		}
	}
}
