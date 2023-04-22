using UnityEngine;
using UnityEngine.Serialization;

public class CarAnimator : CarBehaviour {
	[Header("Components")]
	public Transform ScaleRoot;
	public Transform WiggleRoot;
	public ParticleSystem[] GroundParticleSystems;
	public Transform[] FrontWheels;
	
	[Header("Values")]
	public float WiggleAmount = 10f;
	public float WiggleSmooth = 10f;
	public float ScaleSmooth = 10f;
	public float WheelTurnSmooth = 10f;

	[Header("Scaling")]
	public Vector3 JumpScale = new Vector3(0.6f, 1.5f, 0.6f);
	public Vector3 JumpRotation = new Vector3(45f, 0f, 0f);
	public Vector3 LandScale = new Vector3(1.5f, 0.6f, 1.5f);
	
	Vector3 desiredScale = Vector3.one;
	Vector3 desiredRotationEuler = Vector3.zero;
	bool wasGrounded = false;
	
	void LateUpdate() {
		// When on ground lerp Scale towards Vector3.one
		if (Car.IsGrounded)
			desiredScale = Vector3.one;
		
		// Lerp scaling and rotation
		ScaleRoot.localScale = Vector3.Lerp(ScaleRoot.localScale, desiredScale, Time.deltaTime * ScaleSmooth);
		ScaleRoot.localRotation = Quaternion.Slerp(ScaleRoot.localRotation, Quaternion.Euler(desiredRotationEuler), Time.deltaTime * ScaleSmooth);

		// Update wiggle
		WiggleRoot.localRotation = Quaternion.Slerp(WiggleRoot.localRotation, Quaternion.identity, Time.deltaTime * WiggleSmooth);
		var localVelocity = transform.InverseTransformVector(Car.Velocity);
		
		WiggleRoot.localEulerAngles += new Vector3(localVelocity.z, 0f, localVelocity.x) * (WiggleAmount * Time.deltaTime);
		
		// Update Front Wheels Rotation
		var defaultDirection = transform.forward;
		
		var angle = Vector3.SignedAngle(transform.forward, Car.InputDirection, Vector3.up);
		if (defaultDirection.magnitude > 0.1f) 
			defaultDirection = Quaternion.Euler(0f, angle * 1.25f, 0f) * defaultDirection;
		
		foreach (var wheel in FrontWheels)
			wheel.forward = Vector3.Slerp(wheel.forward, defaultDirection, Time.deltaTime * WheelTurnSmooth);
		
		// Update particle systems
		foreach (var particles in GroundParticleSystems) {
			var emission = particles.emission;
			emission.enabled = Car.IsGrounded;
		}
		
		// "Events"
		if (!wasGrounded && Car.IsGrounded)
			OnLanded();
	
		if (wasGrounded && !Car.IsGrounded)
			OnJumped();

		// Remember grounded state
		wasGrounded = Car.IsGrounded;
	}

	void OnLanded() {
		ScaleRoot.localScale = LandScale;
		desiredRotationEuler = Vector3.zero;
	}	
	
	void OnJumped() {
		desiredScale = JumpScale;
		ScaleRoot.localScale = Vector3.Lerp(ScaleRoot.localScale, desiredScale, 0.5f);
		desiredRotationEuler = JumpRotation;
	}
}
