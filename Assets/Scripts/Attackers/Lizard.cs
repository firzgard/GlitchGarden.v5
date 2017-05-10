using Defenders;
using UnityEngine;

namespace Attackers
{
	public class Lizard : MonoBehaviour {

		private Animator _animator;
		private Attacker _attacker;

		void Awake()
		{
			_animator = GetComponent<Animator>();
			_attacker = GetComponent<Attacker>();
		}

		void OnTriggerEnter2D(Collider2D col)
		{
			var target = col.gameObject;
			if (target.GetComponent<Defender>())
			{
				_attacker.Attack(target);
			}
		}
	}
}
