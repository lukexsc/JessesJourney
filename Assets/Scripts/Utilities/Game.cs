using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Game
{
	public static Game current;
	public static PlayerController player;
	public static bool paused;

	public static string GetCurrentSceneName()
	{
		return SceneManager.GetActiveScene().name;
	}
}