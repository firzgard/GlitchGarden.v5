using UnityEngine;
using System.Collections;
using Defenders;

public class DefenderSpawner : MonoBehaviour
{

	private StarManager _starManager;
	private GameObject _previewer;

	void Awake()
	{
		_starManager = FindObjectOfType<StarManager>();
	}

	void Update()
	{
		// Destroy previewer if there is no defender selected & still exists a previewer
		if (!DefenderSelectButton.SelectedDefender && _previewer) Destroy(_previewer);
	}

	void OnMouseEnter()
	{
		// Check if there is a selected defender
		if (!DefenderSelectButton.SelectedDefender) return;

		// Inst previewer if not already existed
		if (_previewer) _previewer.SetActive(true);
		else _previewer = Instantiate(DefenderSelectButton.SelectedDefender.DefenderPreview) as GameObject;
	}

	void OnMouseOver()
	{
		// Check if there is a selected defender
		if (!DefenderSelectButton.SelectedDefender) return;

		// Move Previewer to position
		_previewer.transform.position = GetSpawnPosition();
	}

	void OnMouseExit()
	{
		if (_previewer) _previewer.SetActive(false);
	}

	// Place defender on mouse down
	void OnMouseDown()
	{
		// Check if there is a selected defender
		if(!DefenderSelectButton.SelectedDefender) return;

		// get the selected prefab
		var prefab = DefenderSelectButton.SelectedDefender.DefenderPrefab;

		// minus the cost of prefab. If no money => return
		if (!_starManager.UseStar(prefab.GetComponent<Defender>().Cost)) return;

		var defender = Instantiate(prefab, GetSpawnPosition(), Quaternion.identity) as GameObject;
		defender.transform.parent = Defender.DefenderContainer.transform;

		// Cleanup
		DefenderSelectButton.SelectedDefender = null;
	}


	private Vector2 GetSpawnPosition()
	{
		// Get the mouse position
		Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		// Round to game coordinates and return
		return new Vector2(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));
	}
}
