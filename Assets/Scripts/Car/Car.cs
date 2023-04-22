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
	public Vector3 Velocity => velocity;
	
	// Input
	public Vector3 InputDirection { get; set; } = Vector3.forward;
	public float InputAcceleration { get; set; } = 0f;
	public bool InputJump { get; set; } = false;
		
	Vector3 velocity;

	void LateUpdate() {
		// Update IsGrounded state
		IsGrounded = transform.position.y <= 0.00001f;
		
		// Apply Gravity to velocity
		if (!IsGrounded)
			velocity.y += -Gravity * Time.deltaTime;
		
		// Damp velocity along the XZ plane, ignore Vertical for gravity to work
		if (IsGrounded) {
			velocity.y = 0;
			velocity = VectorUtils.Damp(velocity, 60f/DampFactor, Time.deltaTime);
		}
		
		// Steering
		transform.forward = Vector3.Slerp(transform.forward, InputDirection, Time.deltaTime * TurnSmooth);
		
		// Accelerate on ground
		if (IsGrounded)
			velocity += transform.forward * (InputAcceleration * AccelerationForce * Time.deltaTime);

		// Move car
		transform.position += velocity * Time.deltaTime;
		transform.position = new Vector3(transform.position.x, Mathf.Max(0f, transform.position.y), transform.position.z);
	}

	public void AddVelocity(Vector3 force) {
		velocity += force;
	}

	public void SetVelocity(Vector3 force) {
		velocity = force;
	}
}