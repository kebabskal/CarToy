using System;
using System.Collections;
using UnityEngine;

class HomingOnBump : ShotBehaviour
{
	protected override void Start()
	{
		if (Shot.Source.TryGetComponent(out CarBump carBump))
			carBump.Bumped += OnCarBumped;
	}

	void OnDestroy()
	{
		if (Shot.Source.TryGetComponent(out CarBump carBump))
			carBump.Bumped -= OnCarBumped;
	}

	public void OnCarBumped(GameObject obj)
	{
		if (target)
			return;

		target = obj.transform;
		Shot.StartCoroutine(HomingRoutine());
	}

	IEnumerator HomingRoutine()
	{
		Shot.Lifespan += homingDuration;
		float elapsed = 0;
		float homingSpeed = Shot.Rigidbody.velocity.magnitude + homingSpeedOffset;
		while (target)
		{
			elapsed = Mathf.Clamp01(elapsed + Time.deltaTime);
			var targetDirection = (target.position - transform.position).normalized;
			var shotVelocity = Shot.Rigidbody.velocity;
			Shot.Rigidbody.velocity = Vector3.Lerp(shotVelocity, targetDirection * homingSpeed, elapsed / timeToLock);
			yield return null;
		}
	}

	[SerializeField]
	float homingDuration = 2;

	[SerializeField]
	float timeToLock = .3f;

	Transform target;

	[SerializeField]
	float homingSpeedOffset = 2;
}
