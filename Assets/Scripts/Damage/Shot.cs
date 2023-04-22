using System;
using UnityEngine;

public class Shot : MonoBehaviour {
	public float Lifespan = 2f;
	public Transform HitEffect;
	public Collider Collider;

	Vector3 baseScale;
	float age = 0f;

	void Start() {
		Destroy(gameObject, Lifespan);
		baseScale = transform.localScale;
		transform.localScale = Vector3.one;
	}

	void Update() {
		age += Time.time;
		Collider.enabled = age > 0.1f;
		
		transform.localScale = Vector3.Lerp(
			transform.localScale,
			baseScale,
			Time.deltaTime * 25f
		);
	}

	void OnTriggerEnter(Collider other) {
		Instantiate(HitEffect, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}
