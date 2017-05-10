using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager : MonoBehaviour
{
	private const string MasterVolumeKey = "master_volume";
	private const string UnlockedLvlKey = "unlocked_lvl";
	private const string DifficultyKey = "difficulty";
	private const float DefaultMasterVolume = .8f;
	private const int DefaultDifficulty = 2;


	public static float MasterVolume
	{
		get { return PlayerPrefs.GetFloat(MasterVolumeKey, DefaultMasterVolume); }
		set
		{
			if (value >= 0 && value <= 1) PlayerPrefs.SetFloat(MasterVolumeKey, value);
			else Debug.LogError("Master volume: out of range.");
		}
	}


	public static void UnlockLvl(int lvl)
	{
		if (lvl > -1 && lvl < SceneManager.sceneCountInBuildSettings)
			PlayerPrefs.SetInt(UnlockedLvlKey + lvl, 1);
		else
			Debug.LogError("Unlocked Lvl: lvl does not exist in build order");
	}

	public static bool IsLvlUnlocked(int lvl)
	{
		if (lvl > -1 && lvl < SceneManager.sceneCountInBuildSettings)
			return PlayerPrefs.GetInt(UnlockedLvlKey + lvl) == 1;
		else
		{
			Debug.LogError("Unlocked Lvl: lvl does not exist in build order");
			return false;
		}
		
	}


	public static float Difficulty
	{
		get { return PlayerPrefs.GetFloat(DifficultyKey, DefaultDifficulty); }
		set
		{
			if (value >= 1 && value <= 3) PlayerPrefs.SetFloat(DifficultyKey, value);
			else Debug.LogError("Difficulty: out of range.");
		}
	}
}
