using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
	public Slider VolumeSlider, DifficultySlider;
	public LevelManager LvlManager;
	public float DefaultMasterVolume = .8f;
	public int DefaultDifficulty = 2;

	void Start()
	{
		VolumeSlider.value = PlayerPrefsManager.MasterVolume;
		DifficultySlider.value = PlayerPrefsManager.Difficulty;
	}
	
	void Update ()
	{
		SoundManager.SetMasterVolume(VolumeSlider.value);
	}

	public void SaveAndExit()
	{
		PlayerPrefsManager.MasterVolume = VolumeSlider.value;
		PlayerPrefsManager.Difficulty = DifficultySlider.value;

		LvlManager.LoadLevel("01a Start Menu");
	}

	public void ResetToDefaults()
	{
		VolumeSlider.value = DefaultMasterVolume;
		DifficultySlider.value = DefaultDifficulty;
	}
}
