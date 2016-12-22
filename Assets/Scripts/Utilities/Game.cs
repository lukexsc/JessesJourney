using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Game
{
	public static Game current;
	public static PlayerController player;
	public static bool paused;

	// Game Values
	public static bool spoke_to_bartender = false;
	public static bool pushed_statue = false;
	public static int stage = 0; // 0 = intro | 1 = basement | 2 = ruins | 3 = puzzles | 4 =
	public static string puzzle = "";

	public static string GetCurrentSceneName()
	{
		return SceneManager.GetActiveScene().name;
	}
}