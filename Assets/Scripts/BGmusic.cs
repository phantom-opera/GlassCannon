using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGmusic : MonoBehaviour
{

	public static BGmusic instance;
	[SerializeField]AudioSource bossMusic;
	[SerializeField] AudioSource mainMenuMusic;
	private void Awake()
	{
		if(instance != null)
		{
			Destroy(gameObject);
		}

		else
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
	}

	private void Update()
	{
		if (SceneManager.GetActiveScene().name == "Main Menu" || SceneManager.GetActiveScene().name == "Coliseum")
		{
			if (!mainMenuMusic.isPlaying)
			{
				BGmusic.instance.mainMenuMusic.Play();
			}
		}
		else if(SceneManager.GetActiveScene().name != "Main Menu")
		{
			BGmusic.instance.mainMenuMusic.Stop();
		}

		if (SceneManager.GetActiveScene().name == "SampleScene" || SceneManager.GetActiveScene().name == "PinkVaseStage")
		{
			if (!bossMusic.isPlaying)
			{
				BGmusic.instance.bossMusic.Play();
			}
		}

		else if(SceneManager.GetActiveScene().name != "SampleScene" || SceneManager.GetActiveScene().name != "PinkVaseStage")
		{
			BGmusic.instance.bossMusic.Stop();
		}
	}
}
