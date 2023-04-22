using UnityEngine;

public class CarJump : CarBehaviour {
	public float ForceInitial = 20f;
	public float ForceContinuous = 20f;
	
	bool jumpWasReleased = true;

	void Update() {
		if (!Car.InputJump)
			jumpWasReleased = true;
		
		if (Car.IsGrounded && Car.InputJump && jumpWasReleased) {
			Car.SetVelocity(Car.Velocity.SetY(ForceInitial));
			jumpWasReleased = false;
			transform.position += Vector3.up * 0.1f;
		}

		if (!Car.IsGrounded && Car.InputJump && !jumpWasReleased)
			Car.AddVelocity(Vector3.up * (ForceContinuous));
	}
}