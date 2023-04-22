using System;
using UnityEngine;

[RequireComponent(typeof(CarAnimator))]
public class Car : MonoBehaviour {
	[Header("Physics")]
	public float DampFactor = 1000f;
	public float Gravity = 70f;
	
	[Header("Driving")]
	public float TurnSmooth = 5f;
	public float AccelerationForce = 10f;
	
	// Physics properties
	public bool IsGrounded { get; private set; }
	public Vector3 Velocity => Rigidbody.velocity;
	
	// Input
	public Vector3 InputDirection { get; set; } = Vector3.forward;
	public float InputAcceleration { get; set; } = 0f;
	public bool InputJump { get; set; } = false;
	
	// Components
	public Rigidbody Rigidbody { get; private set; }
		

	void OnEnable() {
		Rigidbody = GetComponent<Rigidbody>();
	}

	void LateUpdate() {
		// Update IsGrounded state
		IsGrounded = transform.position.y <= 0.1f;
		
		// Apply Gravity to velocity
		if (!IsGrounded) {
			AddVelocity(Vector3.down * Gravity);
		}
		
		// Damp velocity along the XZ plane, ignore Vertical for gravity to work
		if (IsGrounded) {
			SetVelocity(Velocity.SetY(0f));
			SetVelocity(VectorUtils.Damp(Velocity, 60f/DampFactor, Time.deltaTime));
		}
		
		// Steering
		transform.forward = Vector3.Slerp(transform.forward, InputDirection, Time.deltaTime * TurnSmooth);
		
		// Accelerate on ground
		if (IsGrounded)
			AddVelocity(transform.forward * (InputAcceleration * AccelerationForce));

		// Move car
		transform.position = new Vector3(transform.position.x, Mathf.Max(0f, transform.position.y), transform.position.z);
	}

	public void AddVelocity(Vector3 force) {
		Rigidbody.AddForce(force, ForceMode.Acceleration);
	}

	public void SetVelocity(Vector3 force) {
		Rigidbody.velocity = force;
	}
}