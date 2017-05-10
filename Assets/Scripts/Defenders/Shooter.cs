using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Shooter : MonoBehaviour
{
	public GameObject Projectile, Gun;
	[Range(.5f, 50)]
	public float ReloadTime = 1;

	private Animator _animator;
	private float _nextShotTime;
	private static GameObject _projectileContainer;


	void Awake()
	{
		_animator = GetComponent<Animator>();
		if (!_projectileContainer) _projectileContainer = new GameObject("Projectiles");
	}

	void Update ()
	{
		if (HadTargetInLine())
		{
			_animator.SetBool("IsAttacking", true);
			if (_nextShotTime < Time.time) Fire();
		}
		else
		{
			_animator.SetBool("IsAttacking", false);
		}
	}


	private void Fire()
	{
		var projectile = Instantiate(Projectile, Gun.transform.position, Quaternion.identity) as GameObject;
		projectile.transform.parent = _projectileContainer.transform;

		// reload
		_nextShotTime = Time.time + ReloadTime;
	}

	private bool HadTargetInLine()
	{
		// only cast against Attackers layer
		var layerMask = 1 << 9;

		var range = Projectile.GetComponent<Projectile>().Range + 1;
		var result = Physics2D.Raycast(transform.position + Vector3.right, Vector2.right, range, layerMask);

		return result.collider;
	}
}
