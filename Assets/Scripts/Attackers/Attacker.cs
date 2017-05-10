using System;
using UnityEngine;

namespace Attackers
{
	public class Attacker : MonoBehaviour
	{
		[Range(-10, 10)] public float CurrentWalkSpeed = 1;
		[Range(0, 500)] public float Dmg = 20;
		[Range(.5f, 50)] public float ReloadTime = 5;

		private Animator _animator;
		private Health _targetHealth;
		private float _nextAttackTime;

		void Awake()
		{
			_animator = GetComponent<Animator>();
		}

		// Update is called once per frame
		void Update () {
			// Move forward
			transform.Translate(Vector3.left * CurrentWalkSpeed * Time.deltaTime);

			// Check to stop attack upon enemy's death
			if (_targetHealth && _targetHealth.GetComponent<Collider2D>())
			{
				if (_nextAttackTime < Time.time)
				{
					_targetHealth.TakeDamage(Dmg);
					_nextAttackTime = Time.time + ReloadTime;
				}
			}
			else
			{
				_animator.SetBool("IsAttacking", false);
				_targetHealth = null;
			}
		}

		// Take target's HP
		public void StrikeCurrentTarget()
		{
			if (_targetHealth || _targetHealth.Hp > 0)
			{
				
			}
		}

		// Lock the target
		public void Attack(GameObject target)
		{
			_animator.SetBool("IsAttacking", true);
			_targetHealth = target.GetComponent<Health>();
		}
	}
}
