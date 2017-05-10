using UnityEngine;
using System.Collections;
using System.Linq;

public class Health : MonoBehaviour
{
	public float Hp = 10;
	public float GetHitAnimationFlashInterval = .1f;
	public float GetHitAnimationDuration = 1f;

	private Animator _animator;
	private SpriteRenderer[] _spriteRenderers;
	private IEnumerator _getHitAnimatingCoroutine;

	void Awake()
	{
		_animator = GetComponent<Animator>();
		_spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
	}

	// Coroutine for flasing animation
	private IEnumerator AnimateGettingHit()
	{
		var remainingTime = GetHitAnimationDuration;

		while (remainingTime > 0)
		{
			remainingTime -= GetHitAnimationFlashInterval;
			foreach (var inst in _spriteRenderers)
			{
				inst.enabled = !inst.enabled;
			}

			yield return new WaitForSeconds(GetHitAnimationFlashInterval);
		}
		foreach (var inst in _spriteRenderers)
		{
			inst.enabled = true;
		}
	}

	public void TakeDamage(float dmg)
	{
		Hp -= dmg;

		// If not dead, start flashing animation, else, start dying animation
		if (Hp > 0)
		{
			// Start flashing animation with scripting
			if(_getHitAnimatingCoroutine != null) StopCoroutine(_getHitAnimatingCoroutine);
			_getHitAnimatingCoroutine = AnimateGettingHit();
			StartCoroutine(_getHitAnimatingCoroutine);
			foreach (var inst in _spriteRenderers)
			{
				inst.enabled = true;
			}
		}
		else
		{
			StopAllCoroutines();
			Die();
		}
	}

	public void Die()
	{
		_animator.SetTrigger("DieTrigger");
		Destroy(GetComponent<Rigidbody2D>());
		Destroy(GetComponent<Collider2D>());
	}

	public void DestroyObject()
	{
		Destroy(gameObject);
	}
}
