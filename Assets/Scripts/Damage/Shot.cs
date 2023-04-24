using System;
using UnityEngine;

public class Shot : MonoBehaviour {
	public float Lifespan = 2f;
	public Transform HitEffect;
	public Collider Collider;

	Vector3 baseScale;
	float age = 0f;

	public Rigidbody Rigidbody { get; private set; }

	void Awake()
	{
		Rigidbody = GetComponent<Rigidbody>();
	}

	void Start() {
		baseScale = transform.localScale;
		transform.localScale = Vector3.one;
	}

	void Update() {
		age += Time.deltaTime;
		Collider.enabled = age > 0.1f;
		
		transform.localScale = Vector3.Lerp(
			transform.localScale,
			baseScale,
			Time.deltaTime * 25f
		);

		if (age > Lifespan)
		{
			gameObject.SetActive(false);
			Destroy(gameObject);
		}
	}

	void LateUpdate()
	{
		transform.forward = Rigidbody.velocity.normalized;
	}

	void OnTriggerEnter(Collider other) {
		Instantiate(HitEffect, transform.position, transform.rotation);
		Destroy(gameObject);
	}

	public void Initialize(GameObject source)
	{
		Source = source;
	}

	public GameObject Source { get; set; }
}
