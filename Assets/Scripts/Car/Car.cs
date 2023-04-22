using System;
using UnityEngine;

[RequireComponent(typeof(CarAnimator))]
public class Car : MonoBehaviour {
	[Header("Physics")]
	public float DampFactor = 1000f;
	
	
	[Header("Driving")]
	public float TurnSmooth = 5f;
	public float AccelerationForce = 10f;
	public LayerMask GroundMask;

	// Physics properties
	public bool IsGrounded { get; private set; }
	public bool IsGroundedWithGrace => Time.time - lastGrounded < 0.25f;
	public Vector3 Velocity => Rigidbody.velocity;
	
	// Input
	public Vector3 InputDirection { get; set; } = Vector3.forward;
	public float InputAcceleration { get; set; } = 0f;
	public bool InputJump { get; set; } = false;
	
	// Components
	public Rigidbody Rigidbody { get; private set; }

	BoxCollider boxCollider;
	float lastGrounded = -999f;

	void OnEnable() {
		Rigidbody = GetComponent<Rigidbody>();
		boxCollider = GetComponent<BoxCollider>();
	}


	Collider[] groundResults = new Collider[32];
	void CheckGrounded() {
		IsGrounded = false;

		var groundColliderCount = Physics.OverlapBoxNonAlloc(
			transform.position + Vector3.up * (boxCollider.size.y * 0.5f - 0.25f),
			boxCollider.size,
			groundResults,
			transform.rotation,
			GroundMask,
			QueryTriggerInteraction.Ignore
		);

		for (int i = 0; i < groundColliderCount; i++) {
			var groundCollider = groundResults[i];
			if (groundCollider.gameObject != gameObject) {
				IsGrounded = true;
				lastGrounded = Time.time;
				break;
			}
		}
	}

	void LateUpdate() {
		// Update IsGrounded state
		CheckGrounded();

		// Damp velocity along the XZ plane, ignore Vertical for gravity to behave
		if (IsGrounded) {
			var xzVelocity = Velocity.SetY(0f);
			var newVelocity = VectorUtils.Damp(xzVelocity, 60f/DampFactor, Time.deltaTime);
			newVelocity.y = Velocity.y;
			SetVelocity(newVelocity);
		}
		
		// Steering
		transform.forward = Vector3.Slerp(transform.forward, InputDirection.SetY(0f), Time.deltaTime * TurnSmooth);
		
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