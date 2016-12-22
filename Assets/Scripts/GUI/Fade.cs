using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
	public static Fade instance;
	public float transition_time = 1.0f;
	public Image fade_img;

	bool fading = false;
	float time = 0f;

	void Awake()
	{
		if (instance == null)
		{
			DontDestroyOnLoad(transform.gameObject);
			instance = this;
		}
		else Destroy(gameObject);
	}

	//This gets called from anywhere you need to load a new scene
	public void LoadScene(string level)
	{
		if (!fading) StartCoroutine(EndScene(level));
	}
	public void LoadScene(string level, Vector3 player_loc, bool facing_right)
	{
		if (!fading) StartCoroutine(EndScene(level, player_loc, facing_right));
	}

	// Fades Into the Scene
	IEnumerator StartScene()
	{
		time = 1.0f;
		yield return null;

		while (time >= 0.0f)
		{
			fade_img.color = new Color(fade_img.color.r, fade_img.color.g, fade_img.color.b, time);
			time -= Time.fixedDeltaTime * (1.0f / transition_time);//Time.deltaTime * (1.0f / transition_time);
			yield return null;
		}

		fade_img.gameObject.SetActive(false);
		Game.paused = false;
		fading = false;
	}
	IEnumerator StartScene(Vector3 player_loc2)
	{
		time = 1.0f;
		yield return null;

		while (time >= 0.0f)
		{
			if (time == 1) Game.player.transform.position = player_loc2;
			fade_img.color = new Color(fade_img.color.r, fade_img.color.g, fade_img.color.b, time);
			time -= Time.fixedDeltaTime * (1.0f / transition_time); //Time.deltaTime * (1.0f / transition_time);
			yield return null;
		}

		fade_img.gameObject.SetActive(false);
		Game.paused = false;
		fading = false;
	}

	// Fades Out of the Scene
	IEnumerator EndScene(string nextScene)
	{
		fading = true;
		Game.paused = true;
		fade_img.gameObject.SetActive(true);
		time = 0.0f;
		yield return null;

		while (time <= 1.0f)
		{
			fade_img.color = new Color(fade_img.color.r, fade_img.color.g, fade_img.color.b, time);
			time +=  Time.fixedDeltaTime * (1.0f / transition_time);//Time.deltaTime * (1.0f / transition_time);
			yield return null;
		}

		SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
		StartCoroutine(StartScene());
	}
	IEnumerator EndScene(string nextScene, Vector3 player_loc, bool facing_right) // Overload - set player location
	{
		fading = true;
		Game.paused = true;
		fade_img.gameObject.SetActive(true);
		time = 0.0f;
		yield return null;

		while (time <= 1.0f)
		{
			fade_img.color = new Color(fade_img.color.r, fade_img.color.g, fade_img.color.b, time);
			time += Time.fixedDeltaTime * (1.0f / transition_time);//Time.deltaTime * (1.0f / transition_time);
			yield return null;
		}

		SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
		StartCoroutine(StartScene(player_loc));
	}
}
