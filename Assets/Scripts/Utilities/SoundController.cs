using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour
{
	[HideInInspector] public static SoundController instance;
	public AudioSource music_source;
	public AudioSource effect_source;

	void Start ()
	{
		instance = this;
	}

	// Play a sound effect
	public void PlayEffect(AudioClip clip)
	{
		effect_source.clip = clip;
		effect_source.Play();
	}

	// Play music clip - on loop
	public void PlayMusic(AudioClip clip)
	{
		music_source.clip = clip;
		music_source.Play();
	}

	public AudioClip GetMusic()
	{
		return music_source.clip;
	}
}