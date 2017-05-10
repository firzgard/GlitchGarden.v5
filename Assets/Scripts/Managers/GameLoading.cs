using UnityEngine;
using System.Collections;

public class GameLoading : MonoBehaviour
{
	public AudioClip GameLoadingSfx;
	public float GameLoadingSfxVolume = .3f;

	// Use this for initialization
	void Start ()
	{
		SoundManager.PlayOneShot2D(GameLoadingSfx, volume: GameLoadingSfxVolume);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
