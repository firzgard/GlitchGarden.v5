using UnityEngine;

namespace Defenders
{
	public class Defender : MonoBehaviour
	{
		public static GameObject DefenderContainer;
		[Range(1, 2000)]
		public int Cost = 100;

		void Awake()
		{
			if (!DefenderContainer) DefenderContainer = new GameObject("Defenders");
		}
	}
}
