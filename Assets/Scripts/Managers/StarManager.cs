using UnityEngine;
using UnityEngine.UI;

public class StarManager : MonoBehaviour
{
	public int Star = 200;

	private Text _textDisplay;

	void Awake()
	{
		_textDisplay = GetComponent<Text>();
		_textDisplay.text = Star.ToString();
	}

	public void AddStar(int amount)
	{
		Star += amount;
		_textDisplay.text = Star.ToString();
	}

	public bool UseStar(int amount)
	{
		if (amount > Star) return false;

		Star -= amount;
		_textDisplay.text = Star.ToString();
		return true;
	}
}
