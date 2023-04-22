using UnityEngine;

public class CarFollow : CarBehaviour{
	public Transform Follow;

	void LateUpdate() {
		if (Follow == null)
			return;
		var delta = Follow.position - transform.position;
		delta.y = 0f;
		delta.Normalize();
		
		Car.InputDirection = delta;
		Car.InputAcceleration = 1f;
	}
}
