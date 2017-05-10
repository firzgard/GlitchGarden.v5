using UnityEngine;
using System.Collections;
using Attackers;

public class Projectile : MonoBehaviour
{
	public float Spd = 1;
	public float Range = 2;
	public float Dmg = 10;

	private float _flownDistance;
	
	// Update is called once per frame
	void Update ()
	{
		if (_flownDistance < Range)
		{
			var nextFrameTravelDistance = Spd * Time.deltaTime;
			_flownDistance += nextFrameTravelDistance;
			transform.Translate(Vector3.right * nextFrameTravelDistance);
		}
		else
		{
			Destroy(gameObject);
		}
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (!col.gameObject.GetComponent<Attacker>()) return;

		var enemyHealth = col.gameObject.GetComponent<Health>();

		if (!(enemyHealth.Hp > 0)) return;

		enemyHealth.TakeDamage(Dmg);
		Destroy(gameObject);
	}
}
