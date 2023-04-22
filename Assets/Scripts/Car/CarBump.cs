using System;
using UnityEngine;

public class CarBump : CarBehaviour {
	public float ForceMultiplier = 10f;
	void OnCollisionEnter(Collision collision) {
		var otherRigidBody = collision.rigidbody;
		if (otherRigidBody == null)
			return;
		
		otherRigidBody.AddForce(-collision.relativeVelocity * ForceMultiplier, ForceMode.Impulse);
		Car.Rigidbody.AddForce(collision.relativeVelocity * ForceMultiplier, ForceMode.Impulse);
		
	}
}
