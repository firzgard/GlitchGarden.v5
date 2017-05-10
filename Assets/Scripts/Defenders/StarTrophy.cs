using UnityEngine;

namespace Defenders
{
	public class StarTrophy : MonoBehaviour
	{
		[Range(1, 1000)]
		public int StarEarnedEachTime = 20;

		[Range(1, 50)]
		public float StarBirthInterval = 5;

		private StarManager _starCounter;
		private Animator _animator;
		private float _nextStarBirthTime;

		void Awake()
		{
			_starCounter = FindObjectOfType<StarManager>();
			_animator = GetComponent<Animator>();
			_nextStarBirthTime = Time.time + StarBirthInterval;
		}

		void Update()
		{
			if (Time.time > _nextStarBirthTime)
			{
				_starCounter.AddStar(StarEarnedEachTime);
				_animator.SetTrigger("GrowthTrigger");
				_nextStarBirthTime += StarBirthInterval;
			}
		}
	}
}
