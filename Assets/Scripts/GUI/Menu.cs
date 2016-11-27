using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
	public string start_scene;
    public Button first_button;

    void Start()
    {
        first_button.Select();
    }

	public void StartGame()
	{
		if (start_scene == "") start_scene = "Tavern";
		Fade.instance.LoadScene(start_scene);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
