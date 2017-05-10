using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	[Range(0, float.MaxValue)]
	public float AutoLoadNextLvlTime = 3;

	void Start()
	{
		if (Mathf.Approximately(AutoLoadNextLvlTime, 0)) Debug.Log("Level auto loading disabled");
		else Invoke("LoadNextLevel", AutoLoadNextLvlTime);
	}

	public void LoadLevel(string sceneName){
		Debug.Log ("New Level loaded: " + sceneName);
		SceneManager.LoadScene(sceneName);
	}

	public void LoadLevel(string sceneName, float time)
	{
		StartCoroutine(LoadLevelAfter(sceneName, time));
	}

	public IEnumerator LoadLevelAfter(string sceneName, float time)
	{
		yield return new WaitForSeconds(time);

		Debug.Log("New Level loaded: " + sceneName);
		SceneManager.LoadScene(sceneName);
	}

	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

	public void LoadNextLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
