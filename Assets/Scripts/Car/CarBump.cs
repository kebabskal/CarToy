using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class CarBump : CarBehaviour {
	public float ForceMultiplier = 10f;
	public Transform Effect;
	
	void OnCollisionEnter(Collision collision) {
		var otherRigidBody = collision.rigidbody;
		if (otherRigidBody == null)
			return;
		
		otherRigidBody.AddForce(-collision.relativeVelocity * ForceMultiplier, ForceMode.Impulse);
		Car.Rigidbody.AddForce(collision.relativeVelocity * ForceMultiplier, ForceMode.Impulse);

		var delta = Car.transform.position - otherRigidBody.transform.position;
		Instantiate(Effect, Car.transform.position, quaternion.LookRotation(delta.normalized, Vector3.up));

		Bumped?.Invoke(otherRigidBody.gameObject);
	}

	public event Action<GameObject> Bumped;
}
