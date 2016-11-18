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

	public static string GetCurrentSceneName()
	{
		return SceneManager.GetActiveScene().name;
	}
}