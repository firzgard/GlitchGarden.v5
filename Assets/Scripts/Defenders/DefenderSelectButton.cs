using System.Linq;
using UnityEngine;
using Defenders;
using UnityEngine.UI;

public class DefenderSelectButton : MonoBehaviour
{
	public bool IsAvailable;
	public GameObject DefenderPrefab;
	public GameObject DefenderPreview;
	public Color DisabledColor = new Color(.2f, .2f, .2f);
	public Color NormalColor = new Color(.7f, .7f, .7f);
	public Color FocusedColor = Color.white;
	public static DefenderSelectButton SelectedDefender;

	private StarManager _starManager;
	private DefenderSelectButton _thisBtn;
	private static DefenderSelectButton[] _buttonArray;

	void Awake()
	{
		if (IsAvailable)
		{
			_thisBtn = gameObject.GetComponent<DefenderSelectButton>();
			_buttonArray = FindObjectsOfType<DefenderSelectButton>().Where(b => b.IsAvailable).ToArray();
			_starManager = FindObjectOfType<StarManager>();

			var costText = transform.FindChild("CostText");
			if (costText)
			{
				costText.GetComponent<Text>().text = DefenderPrefab.GetComponent<Defender>().Cost.ToString();
			}
			else
			{
				Debug.LogWarning(name + " has no Cost display.");
			}
		}
		else
		{
			this.enabled = GetComponent<Collider2D>().enabled = false;
			GetComponent<SpriteRenderer>().color = DisabledColor;
		}
	}

	// Update is called once per frame
	void Update () {
		foreach (var btn in _buttonArray)
		{
			if (btn.DefenderPrefab.GetComponent<Defender>().Cost > _starManager.Star)
			{
				btn.GetComponent<SpriteRenderer>().color = btn == SelectedDefender ? FocusedColor : DisabledColor;
				btn.GetComponent<Collider2D>().enabled = false;
			}
			else
			{
				btn.GetComponent<SpriteRenderer>().color = btn == SelectedDefender ? FocusedColor : NormalColor;
				btn.GetComponent<Collider2D>().enabled = true;
			}
		}
	}

	void OnMouseDown()
	{
		SelectedDefender = SelectedDefender == _thisBtn ? null : _thisBtn;
	}
}
