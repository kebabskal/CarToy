using System;
using UnityEngine;

public class CarInput : CarBehaviour {
	public Controls Controls { get; private set; }
	
	public override void OnEnable() {
		base.OnEnable();
		Controls = new Controls();
		Controls.Enable();
	}

	void Update() {
		// Read input
		var directionV2 = Controls.Car.Direction.ReadValue<Vector2>();
		var direction = new Vector3(directionV2.x, 0, directionV2.y);
		
		// Limit the length so diagonals aren't longer than cardinals
		direction = direction.LimitLength(1f);
		
		// Update Input values 
		if (direction.magnitude > 0.1f) Car.InputDirection = direction.normalized;
		Car.InputAcceleration = direction.magnitude;
		Car.InputJump = Controls.Car.Jump.IsPressed();
		Car.InputShoot = Controls.Car.Shoot.IsPressed();
		
	}
}
