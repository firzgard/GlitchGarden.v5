using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {

	public List<AudioClip> SceneBgms;

	private AudioSource _primaryAudioSource;
	private GameObject _2DsfxSpawnPosition;
	private static SoundManager _instance = null;
	private static float _masterVolume;

	void Awake()
	{
		if (_instance && _instance != this)
		{
			Destroy(gameObject);
			print("Duplicate music player self-destructing!");
		}
		else
		{
			_instance = this;
			_instance._primaryAudioSource = this.GetComponent<AudioSource>();
			_masterVolume = PlayerPrefsManager.MasterVolume;
			_instance._primaryAudioSource.volume = _masterVolume;
			_instance._primaryAudioSource.spatialBlend = 0;

			GameObject.DontDestroyOnLoad(gameObject);
		}
	}

	void Start()
	{
		PlayLvlBgm(SceneManager.GetActiveScene().buildIndex);
	}

	void OnLevelWasLoaded(int lvl)
	{
		PlayLvlBgm(lvl);
	}



	private void PlayLvlBgm(int lvl)
	{
		if (SceneBgms [lvl])
		{
			_instance._primaryAudioSource.clip = SceneBgms[lvl];
			_instance._primaryAudioSource.Play();
		}
		else
		{
			_instance._primaryAudioSource.Stop();
		}
	}

	public static void PlayCustomBgm(AudioClip clip, float delay = 0)
	{
		if (delay > 0) _instance.StartCoroutine(DelayPlay(_instance._primaryAudioSource, delay, clip));
		else _instance._primaryAudioSource.Play();
	}

	public static AudioSource PlayOneShot2D(AudioClip clip, float delay = 0, float volume = 1, float pitch = 1, float pan = 0)
	{
		// Create new spawn position if not already existed
		if (!_instance._2DsfxSpawnPosition) _instance._2DsfxSpawnPosition = new GameObject("2DsfxSpawnPosition");

		var newASource = _instance._2DsfxSpawnPosition.gameObject.AddComponent<AudioSource>();
		newASource.clip = clip;
		newASource.volume = _masterVolume * volume;
		newASource.spatialBlend = 0;
		newASource.pitch = pitch;
		newASource.panStereo = pan;

		if (delay > 0)
		{
			_instance.StartCoroutine(DelayPlay(newASource, delay));
			Destroy(newASource, delay + clip.length + .1f);
		}
		else
		{
			newASource.Play();
			Destroy(newASource, clip.length + .1f);
		}

		return newASource;
	}

	public static void SetMasterVolume(float volume)
	{
		_masterVolume = volume;
		_instance._primaryAudioSource.volume = _masterVolume;
	}



	public static IEnumerator DelayPlay(AudioSource source, float delay, AudioClip clip = null)
	{
		yield return new WaitForSeconds(delay);

		if (clip) source.clip = clip;

		source.Play();
	}

	public static void StopBgm()
	{
		_instance._primaryAudioSource.Stop();
	}
}
